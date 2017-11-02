using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CodeActions;
using Microsoft.CodeAnalysis.CodeFixes;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Code.Analyzers.Common
{
    public abstract class BaseCodeFixProvider : CodeFixProvider
    {
        protected void RegisterCodeFixForDiagnostic<T>(CodeFixContext context, SyntaxNode root, string id, string title, Func<Document, Diagnostic, T, CancellationToken, Task<Document>> codeFixer) where T: SyntaxNode
        {
            foreach (Diagnostic diagnostic in context.Diagnostics.Where(d => d.Id == id))
            {
                var diagnosticSpan = diagnostic.Location.SourceSpan;

                // Find the type declaration identified by the diagnostic.
                SyntaxNode node = root.FindToken(diagnosticSpan.Start).Parent;
                while (!(node is T))
                    node = node.Parent;

                // Register a code action that will invoke the fix.
                context.RegisterCodeFix(
                    CodeAction.Create(
                        title,
                        createChangedDocument: c => codeFixer(context.Document, diagnostic, (T)node, c),
                        equivalenceKey: title),
                    diagnostic);
            }
        }

        protected Task<Document> ChangeMethodName(Document document, MethodDeclarationSyntax methodDeclaration, string newName, CancellationToken cancellationToken)
        {
            SyntaxToken methodNameToken = methodDeclaration.ChildTokens().First(t => t.Kind() == SyntaxKind.IdentifierToken);
            SyntaxToken newNameToken = SyntaxFactory.Identifier(newName);
            var newMethod = methodDeclaration.ReplaceToken(methodNameToken, newNameToken);

            return ReplaceOriginalNode(document, methodDeclaration, newMethod, cancellationToken);
        }

        protected Task<Document> AddAttributeListAsync<T>(Document document, T node, Func<T, SyntaxTriviaList, SyntaxList<AttributeListSyntax>> attrListCreator, CancellationToken cancellationToken) where T : SyntaxNode
        {
            return AddAttributeListAsync(document, node, null, attrListCreator, cancellationToken);
        }

        protected async Task<Document> AddAttributeListAsync<T>(Document document, T node, string[] requiredUsings, Func<T, SyntaxTriviaList, SyntaxList<AttributeListSyntax>> attrListCreator, CancellationToken cancellationToken) where T: SyntaxNode
        {
            Document newDocument = await AddAttributeToDocument(document, node, attrListCreator, cancellationToken).ConfigureAwait(false);
            newDocument = await AddMissingUsings(newDocument, requiredUsings, cancellationToken).ConfigureAwait(false);

            return newDocument;
        }

        protected async Task<Document> AddMissingUsings(Document document, string[] usings, CancellationToken cancellationToken)
        {
            var compilationUnit = await document.GetSyntaxRootAsync(cancellationToken).ConfigureAwait(false) as CompilationUnitSyntax;

            IEnumerable<string> currentUsings = compilationUnit.Usings.Select(u => u.Name.ToString());
            
            UsingDirectiveSyntax[] newUsings = (usings ?? new string[] { }).Except(currentUsings).Select(CreateUsing).ToArray();
            compilationUnit = compilationUnit.AddUsings(newUsings);

            return document.WithSyntaxRoot(compilationUnit);
        }

        private static UsingDirectiveSyntax CreateUsing(string @namespace)
        {
            return SyntaxFactory.UsingDirective(SyntaxFactory.ParseName(@namespace));
        }

        private static async Task<Document> AddAttributeToDocument<T>(Document document, T node, Func<T, SyntaxTriviaList, SyntaxList<AttributeListSyntax>> attrListCreator, CancellationToken cancellationToken) where T : SyntaxNode
        {
            SyntaxTriviaList leadingTrivia = GetLeadingTriviaWithoutRedundantEndOfLine(node);            

            var attrList = attrListCreator(node, leadingTrivia);

            var adaptor = new AttributeManipulationAdaptor(node);
            SyntaxNode newMethod = adaptor.WithAttributeLists(attrList);

            return await ReplaceOriginalNode(document, node, newMethod, cancellationToken).ConfigureAwait(false);
        }

        private static SyntaxTriviaList GetLeadingTriviaWithoutRedundantEndOfLine<T>(T node) where T : SyntaxNode
        {
            var firstToken = node.GetFirstToken();
            var leadingTrivia = node.GetLeadingTrivia();

            if (firstToken.Parent is AttributeListSyntax && firstToken.Parent.HasTrailingTrivia)
            {
                var trivia = firstToken.Parent.GetTrailingTrivia();
                if (trivia.Any(t => t.IsKind(SyntaxKind.EndOfLineTrivia)))
                    leadingTrivia = leadingTrivia.RemoveAt(0);
            }

            return leadingTrivia;
        }

        protected static async Task<Document> ReplaceOriginalNode<T>(Document document, T originalNode, SyntaxNode newNode, CancellationToken cancellationToken) where T : SyntaxNode
        {
            // Replace the old local declaration with the new local declaration.
            var oldRoot = await document.GetSyntaxRootAsync(cancellationToken).ConfigureAwait(false);
            var newRoot = oldRoot.ReplaceNode(originalNode, newNode);
            
            // Return document with transformed tree.
            return document.WithSyntaxRoot(newRoot);
        }

        protected Task<Document> InsertBeforeNode<T>(Document document, T originalNode, SyntaxNode newNode, IEnumerable<string> requiredUsings, CancellationToken cancellationToken) where T : SyntaxNode
        {
            return InsertBeforeNode(document, originalNode, new SyntaxNode[] { newNode }, requiredUsings, cancellationToken);
        }

        protected async Task<Document> InsertBeforeNode<T>(Document document, T originalNode, IEnumerable<SyntaxNode> newNodes, IEnumerable<string> requiredUsings, CancellationToken cancellationToken) where T : SyntaxNode
        {
            var oldRoot = await document.GetSyntaxRootAsync(cancellationToken).ConfigureAwait(false);
            var newRoot = oldRoot.InsertNodesBefore(originalNode, newNodes);

            Document newDocument =  document.WithSyntaxRoot(newRoot);
            return await AddMissingUsings(newDocument, requiredUsings.ToArray(), cancellationToken).ConfigureAwait(false);
        }

        protected static string GetDeclaringClassName(SyntaxNode childNode)
        {
            SyntaxNode currentNode = childNode.Parent;
            while (!(currentNode is TypeDeclarationSyntax))
                currentNode = currentNode.Parent;

            string className = ((TypeDeclarationSyntax)currentNode).Identifier.Text;
            return className;
        }

        protected Task<Document> AddDefaultAttributeProperty(Document document, AttributeSyntax declaration, string propertyName, CancellationToken cancellationToken)
        {
            return AddDefaultAttributeProperty(document, declaration, propertyName, string.Empty, cancellationToken);
        }

        protected Task<Document> AddDefaultAttributeProperty(Document document, AttributeSyntax declaration, string propertyName, string defaultValue, CancellationToken cancellationToken)
        {
            ExpressionSyntax expression = SyntaxFactory.ParseExpression($"{propertyName} = \"{defaultValue}\"");
            AttributeArgumentSyntax attribute = SyntaxFactory.AttributeArgument(expression);
            AttributeSyntax newNode = declaration.AddArgumentListArguments(attribute);

            return ReplaceOriginalNode(document, declaration, newNode, cancellationToken);
        }
    }
}

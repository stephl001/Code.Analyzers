using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CodeFixes;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Orckestra.UnitTestAnalyzers.UnitTest
{
    public partial class UnitTestSyntaxAnalyzerCodeFixProvider
    {
        public sealed override async Task RegisterCodeFixesAsync(CodeFixContext context)
        {
            var root = await context.Document.GetSyntaxRootAsync(context.CancellationToken).ConfigureAwait(false);

            RegisterCodeFixForDiagnostic<MethodDeclarationSyntax>(context, root, UnitTestSyntaxAnalyzer.TestCategoryDiagnosticId, "TestCategory Analyzer", AddTestCategoryAsync);
            RegisterCodeFixForDiagnostic<MethodDeclarationSyntax>(context, root, UnitTestSyntaxAnalyzer.AsyncVoidDiagnosticId, "Async void Analyzer", ReplaceVoidForTaskAsync);
            RegisterCodeFixForDiagnostic<MethodDeclarationSyntax>(context, root, UnitTestSyntaxAnalyzer.AsyncTaskDiagnosticId, "Async Task Analyzer", AddAsyncTestCategoryAsync);
            RegisterCodeFixForDiagnostic<MethodDeclarationSyntax>(context, root, UnitTestSyntaxAnalyzer.ClientIntegrationTestDiagnosticId, "Client Integration Analyzer", AddDataSourceAsync);
        }

        private async Task<Document> ReplaceVoidForTaskAsync(Document document, Diagnostic diagnostic, MethodDeclarationSyntax methodDeclaration, CancellationToken cancellationToken)
        {
            SyntaxNode voidIdentifier = methodDeclaration.ReturnType;

            IdentifierNameSyntax taskIdentifier = SyntaxFactory.IdentifierName("Task");
            taskIdentifier = taskIdentifier.WithLeadingTrivia(voidIdentifier.GetLeadingTrivia());
            taskIdentifier = taskIdentifier.WithTrailingTrivia(voidIdentifier.GetTrailingTrivia());

            var newMethod = methodDeclaration.ReplaceNode(voidIdentifier, taskIdentifier);

            // Replace the old local declaration with the new local declaration.
            var oldRoot = await (document.GetSyntaxRootAsync(cancellationToken)).ConfigureAwait(false);
            var newRoot = oldRoot.ReplaceNode(methodDeclaration, newMethod);

            // Return document with transformed tree.
            return document.WithSyntaxRoot(newRoot);
        }

        private Task<Document> AddDataSourceAsync(Document document, Diagnostic diagnostic, MethodDeclarationSyntax declaration, CancellationToken cancellationToken)
        {
            return AddAttributeListAsync(document, declaration, CreateDataSourceAttributeList, cancellationToken);
        }

        private Task<Document> AddTestCategoryAsync(Document document, Diagnostic diagnostic, MethodDeclarationSyntax declaration, CancellationToken cancellationToken)
        {
            return AddAttributeListAsync(document, declaration, CreateTestCategoryAttributeLists, cancellationToken);
        }

        private Task<Document> AddAsyncTestCategoryAsync(Document document, Diagnostic diagnostic, MethodDeclarationSyntax declaration, CancellationToken cancellationToken)
        {
            return AddAttributeListAsync(document, declaration, CreateAsyncAttributeList, cancellationToken);
        }

        private SyntaxList<AttributeListSyntax> CreateAsyncAttributeList(MethodDeclarationSyntax declaration, SyntaxTriviaList trivia)
        {
            SyntaxList<AttributeListSyntax> list = declaration.AttributeLists;

            var attrList = SyntaxFactory.AttributeList().AddAttributes(CreateAsyncTestCategoryAttribute());
            list = list.Add(attrList.WithLeadingTrivia(trivia));

            return list;
        }

        private static SyntaxList<AttributeListSyntax> CreateDataSourceAttributeList(MethodDeclarationSyntax declaration, SyntaxTriviaList trivia)
        {
            SyntaxList<AttributeListSyntax> list = declaration.AttributeLists;
            
            var attrList = SyntaxFactory.AttributeList().AddAttributes(CreateDataSourceAttribute());
            list = list.Add(attrList.WithLeadingTrivia(trivia));

            return list;
        }
        
        private static AttributeSyntax CreateAsyncTestCategoryAttribute()
        {
            var attr = SyntaxFactory.Attribute(SyntaxFactory.IdentifierName("TestCategory"));
            var token = SyntaxFactory.ParseToken("\"Async\"");
            var literal = SyntaxFactory.LiteralExpression(SyntaxKind.StringLiteralExpression, token);
            attr = attr.AddArgumentListArguments(SyntaxFactory.AttributeArgument(literal));

            return attr;
        }

        private static AttributeSyntax CreateDataSourceAttribute()
        {
            var attr = SyntaxFactory.Attribute(SyntaxFactory.IdentifierName("DataSource"));
            var identifier = SyntaxFactory.IdentifierName("AllFormats");
            attr = attr.AddArgumentListArguments(SyntaxFactory.AttributeArgument(identifier));

            return attr;
        }

        private static SyntaxList<AttributeListSyntax> CreateTestCategoryAttributeLists(MethodDeclarationSyntax declaration, SyntaxTriviaList trivia)
        {
            SyntaxList<AttributeListSyntax> list = declaration.AttributeLists;
            var validNodes = list.Where(attr => !attr.DescendantTokens().Any(token => (token.ValueText == "Unit" || token.ValueText == "Integration"))).ToArray();

            SyntaxList<AttributeListSyntax> newList = new SyntaxList<AttributeListSyntax>();
            newList = newList.AddRange(validNodes);

            var unitAttrList = SyntaxFactory.AttributeList().AddAttributes(CreateTestCategoryAttribute("Unit"));
            newList = newList.Add(unitAttrList.WithLeadingTrivia(trivia));

            return newList;
        }

        private static AttributeSyntax CreateTestCategoryAttribute(string arg)
        {
            var attr = SyntaxFactory.Attribute(SyntaxFactory.IdentifierName("TestCategory"));
            var token = SyntaxFactory.ParseToken("\"Unit\"");
            var literal = SyntaxFactory.LiteralExpression(SyntaxKind.StringLiteralExpression, token);
            attr = attr.AddArgumentListArguments(SyntaxFactory.AttributeArgument(literal));

            return attr;
        }
    }
}
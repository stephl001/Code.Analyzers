using System.Threading;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CodeFixes;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Orckestra.Analyzers.Common;
using System.Linq;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System;
using Orckestra.Analyzers.Common.Extensions;

namespace Orckestra.Analyzers.General
{
    public partial class GeneralSyntaxAnalyzerCodeFixProvider : BaseCodeFixProvider
    {
        delegate Task<Document> HandleMethodDeclarationCodeFix(Document document, Diagnostic diagnostic, MethodDeclarationSyntax methodExpression, CancellationToken cancellationToken);
        private static readonly IDictionary<string, HandleMethodDeclarationCodeFix> _handlers = new Dictionary<string, HandleMethodDeclarationCodeFix>()
        {
            {"NonGeneric", RefactorNonGenericAwaitMethodAsync},
            {"Generic", RefactorGenericAwaitMethodAsync}
        };

        public sealed override async Task RegisterCodeFixesAsync(CodeFixContext context)
        {
            var root = await context.Document.GetSyntaxRootAsync(context.CancellationToken).ConfigureAwait(false);

            RegisterCodeFixForDiagnostic<InvocationExpressionSyntax>(context, root, GeneralSyntaxAnalyzer.ThreadSleepInAsyncDiagnosticId, "ThreadSleep Analyzer", ReplaceThreadSleepAsync);
            RegisterCodeFixForDiagnostic<AwaitExpressionSyntax>(context, root, GeneralSyntaxAnalyzer.ConfigureAwaitDiagnosticId, "ConfigureAwait Analyzer", AddConfigureAwaitAsync);
            RegisterCodeFixForDiagnostic<MethodDeclarationSyntax>(context, root, GeneralSyntaxAnalyzer.ReturnSingleAwaitDiagnosticId, "ReturnSingleAwait Analyzer", RefactorAsyncMethodAsync);
        }

        private Task<Document> RefactorAsyncMethodAsync(Document document, Diagnostic diagnostic, MethodDeclarationSyntax methodExpression, CancellationToken cancellationToken)
        {
            return _handlers[diagnostic.Properties["Fixer"]](document, diagnostic, methodExpression, cancellationToken);
        }

        private static Task<Document> RefactorAwaitMethodAsync(Document document, MethodDeclarationSyntax methodExpression, CancellationToken cancellationToken)
        {
            return RefactorAwaitMethodAsync(document, methodExpression, e => e, cancellationToken);
        }

        private static Task<Document> RefactorAwaitMethodAsync(Document document, MethodDeclarationSyntax methodExpression, Func<ExpressionSyntax,SyntaxNode> newAwaitNodeTransform, CancellationToken cancellationToken)
        {
            MethodDeclarationSyntax currentMethodExpression = methodExpression;
            AwaitExpressionSyntax awaitExpression = GetTopLevelAwaitExpression(currentMethodExpression);
            while (awaitExpression != null)
            {
                SyntaxNode transformedNode = TransformAwaitExpression(awaitExpression, newAwaitNodeTransform);

                if (transformedNode is StatementSyntax)
                    currentMethodExpression = currentMethodExpression.ReplaceNode(GetParentStatement(awaitExpression), transformedNode);
                else
                    currentMethodExpression = currentMethodExpression.ReplaceNode(awaitExpression, transformedNode);

                awaitExpression = GetTopLevelAwaitExpression(currentMethodExpression);
            }

            SyntaxToken asyncToken = currentMethodExpression.ChildTokens().First(t => t.IsKind(SyntaxKind.AsyncKeyword));
            MethodDeclarationSyntax noAsyncMethod = currentMethodExpression.ReplaceToken(asyncToken, SyntaxFactory.Token(SyntaxKind.None));

            return ReplaceOriginalNode(document, methodExpression, noAsyncMethod, cancellationToken);
        }

        private static Regex _configureAwaitPattern = new Regex(@"\.ConfigureAwait(WithCulture)?\s*\(\s*(true|false)\s*\)", RegexOptions.CultureInvariant);

        private static SyntaxNode TransformAwaitExpression(AwaitExpressionSyntax awaitExpression, Func<ExpressionSyntax, SyntaxNode> newAwaitNodeTransform)
        {
            string expression = awaitExpression.Expression.ToString();
            expression = ReplaceLastOccurence(_configureAwaitPattern, expression, string.Empty);            

            ExpressionSyntax newExpression = SyntaxFactory.ParseExpression(expression);
            SyntaxNode transformedNode = newAwaitNodeTransform(newExpression);
            transformedNode = transformedNode.WithTriviaFrom(awaitExpression);
            return transformedNode;
        }

        private static string ReplaceLastOccurence(Regex _configureAwaitPattern, string expression, string empty)
        {
            MatchCollection matches =_configureAwaitPattern.Matches(expression);
            if (matches.Count >= 1)
            {
                Match m = matches[matches.Count - 1];
                string part1 = expression.Substring(0, m.Index);
                string part2 = expression.Substring(m.Index + m.Length);

                expression = string.Concat(part1, part2);
            }

            return expression;
        }

        private static Task<Document> RefactorNonGenericAwaitMethodAsync(Document document, Diagnostic diagnostic, MethodDeclarationSyntax methodExpression, CancellationToken cancellationToken)
        {
            return RefactorAwaitMethodAsync(document, methodExpression, SyntaxFactory.ReturnStatement, cancellationToken);
        }

        private static Task<Document> RefactorGenericAwaitMethodAsync(Document document, Diagnostic diagnostic, MethodDeclarationSyntax methodExpression, CancellationToken cancellationToken)
        {
            return RefactorAwaitMethodAsync(document, methodExpression, cancellationToken);
        }

        private static AwaitExpressionSyntax GetTopLevelAwaitExpression(MethodDeclarationSyntax methodExpression)
        {
            return GetTopLevelAwaitExpressions(methodExpression).FirstOrDefault();
        }

        private static IEnumerable<AwaitExpressionSyntax> GetTopLevelAwaitExpressions(MethodDeclarationSyntax methodSyntax)
        {
            return methodSyntax.GetTopLevelExpressions<AwaitExpressionSyntax>(n => n.IsKind(SyntaxKind.AwaitExpression));
        }

        private static StatementSyntax GetParentStatement(SyntaxNode expression)
        {
            while (!(expression is StatementSyntax))
                expression = expression.Parent;

            return (StatementSyntax)expression;
        }

        private Task<Document> ReplaceThreadSleepAsync(Document document, Diagnostic diagnostic, InvocationExpressionSyntax sleepExpression, CancellationToken cancellationToken)
        {
            string delay = diagnostic.Properties["Delay"];
            string newTextExpression = $"await Task.Delay({delay}).ConfigureAwait(false)";
            ExpressionSyntax newExpression = SyntaxFactory.ParseExpression(newTextExpression)
                                                          .WithLeadingTrivia(sleepExpression.GetLeadingTrivia().ToArray())
                                                          .WithTrailingTrivia(sleepExpression.GetTrailingTrivia().ToArray());

            return ReplaceOriginalNode(document, sleepExpression, newExpression, cancellationToken);
        }

        private Task<Document> AddConfigureAwaitAsync(Document document, Diagnostic diagnostic, AwaitExpressionSyntax awaitExpression, CancellationToken cancellationToken)
        {
            string textExpr = $"{awaitExpression.ToString()}.ConfigureAwait(false)";
            ExpressionSyntax newExpression = SyntaxFactory.ParseExpression(textExpr)
                                                          .WithTriviaFrom(awaitExpression);

            return ReplaceOriginalNode(document, awaitExpression, newExpression, cancellationToken);
        }
    }
}
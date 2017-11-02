using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System;
using Code.Analyzers.Common.Extensions;

namespace Code.Analyzers.General
{
    public partial class GeneralSyntaxAnalyzer
    {
        protected override void RegisterActions(AnalysisContext context)
        {
            context.RegisterSyntaxNodeAction(AnalyzeThrowExpressionSyntaxNode, SyntaxKind.ThrowStatement);
            context.RegisterSyntaxNodeAction(AnalyzeAwaitExpressionSyntaxNode, SyntaxKind.AwaitExpression);
            context.RegisterSyntaxNodeAction(AnalyzeInvokeThreadSleepSyntaxNode, SyntaxKind.InvocationExpression);
            context.RegisterSymbolAction(AnalyzeSingleAwaitMethodSymbol, SymbolKind.Method);
        }

        private void AnalyzeSingleAwaitMethodSymbol(SymbolAnalysisContext context)
        {
            var methodSymbol = context.Symbol as IMethodSymbol;
            if (!methodSymbol.IsAsync)
                return;

            var returnType = methodSymbol.ReturnType as INamedTypeSymbol;
            if (returnType.IsGenericType)
                AnalyzeSingleAwaitAsyncGenericCodeBlock(context);
            else
                AnalyzeSingleAwaitAsyncNonGenericCodeBlock(context);
        }

        private void AnalyzeSingleAwaitAsyncNonGenericCodeBlock(SymbolAnalysisContext context)
        {
            AwaitExpressionSyntax[] awaitExpressions = GetTopLevelAwaitExpressions(context).ToArray();
            if (awaitExpressions.Length == 1)
            {
                StatementSyntax[] statements = GetTopLevelStatements(context).ToArray();
                if (!IsLastStatement(statements, GetParentStatement(awaitExpressions[0])))
                    return;
                if (!IsDirectStatement(awaitExpressions[0]))
                    return;

                var propertyBag = CreatePropertyBag(new KeyValuePair<string, string>("Fixer", "NonGeneric"));
                var diagnostic = Diagnostic.Create(ReturnSingleAwaitRule, context.Symbol.Locations.First(), context.Symbol.Locations.Skip(1), propertyBag, context.Symbol.Name);
                context.ReportDiagnostic(diagnostic);
            }
        }

        private bool IsDirectStatement(SyntaxNode node)
        {
            while (node.Parent.IsKind(SyntaxKind.ParenthesizedExpression))
                node = node.Parent;

            return node.Parent.IsKind(SyntaxKind.ExpressionStatement);
        }

        private IEnumerable<StatementSyntax> GetTopLevelStatements(SymbolAnalysisContext context)
        {
            return GetRootNode(context).Body.Statements;
        }

        private void AnalyzeSingleAwaitAsyncGenericCodeBlock(SymbolAnalysisContext context)
        {
            AwaitExpressionSyntax[] awaitExpressions = GetTopLevelAwaitExpressions(context).ToArray();
            if (!awaitExpressions.Any())
                return;

            if (Array.TrueForAll(awaitExpressions, IsPartOfReturnStatement))
            {
                ReturnStatementSyntax[] returnStatements = GetTopLevelReturnStatements(context).ToArray();
                if (returnStatements.Length == awaitExpressions.Length)
                {
                    var propertyBag = CreatePropertyBag(new KeyValuePair<string, string>("Fixer", "Generic"));
                    var diagnostic = Diagnostic.Create(ReturnSingleAwaitRule, context.Symbol.Locations.First(), context.Symbol.Locations.Skip(1), propertyBag, context.Symbol.Name);
                    context.ReportDiagnostic(diagnostic);
                }
            }
        }

        private IEnumerable<ReturnStatementSyntax> GetTopLevelReturnStatements(SymbolAnalysisContext context)
        {
            MethodDeclarationSyntax rootNode = GetRootNode(context);
            return rootNode.GetTopLevelExpressions<ReturnStatementSyntax>(IsPartOfReturnStatement);
        }

        private bool IsPartOfReturnStatement(SyntaxNode node)
        {
            while (!node.IsKind(SyntaxKind.MethodDeclaration))
            {
                if (node.IsKind(SyntaxKind.ReturnStatement))
                    return true;
                
                node = node.Parent;

                if (node is ExpressionSyntax && !node.IsKind(SyntaxKind.ParenthesizedExpression))
                    return false;
            }

            return false;
        }

        private IEnumerable<AwaitExpressionSyntax> GetTopLevelAwaitExpressions(SymbolAnalysisContext context)
        {
            MethodDeclarationSyntax rootNode = GetRootNode(context);
            return rootNode.GetTopLevelNonClosureExpressions<AwaitExpressionSyntax>(n => n.IsKind(SyntaxKind.AwaitExpression));
        }

        private static MethodDeclarationSyntax GetRootNode(SymbolAnalysisContext context)
        {
            var methodSymbol = context.Symbol as IMethodSymbol;
            SyntaxReference syntaxRef = methodSymbol.DeclaringSyntaxReferences.Single();
            var rootNode = syntaxRef.GetSyntax() as MethodDeclarationSyntax;
            return rootNode;
        }

        private StatementSyntax GetParentStatement(SyntaxNode node)
        {
            while (!(node is StatementSyntax))
                node = node.Parent;

            return (StatementSyntax)node;
        }

        private bool IsLastStatement(StatementSyntax[] statements, StatementSyntax syntaxNode)
        {
            int index = Array.IndexOf(statements, syntaxNode);
            return (index == statements.Length - 1);
        }

        private static Regex _threadSleepPattern = new Regex(@"((System\.)?Threading\.)?Thread\.Sleep", RegexOptions.CultureInvariant);
        private void AnalyzeInvokeThreadSleepSyntaxNode(SyntaxNodeAnalysisContext context)
        {
            var expr = context.Node as InvocationExpressionSyntax;
            SyntaxNode node = expr.ChildNodes().FirstOrDefault(n => n.IsKind(SyntaxKind.SimpleMemberAccessExpression));
            if (node != null && _threadSleepPattern.IsMatch(node.ToString()))
            {
                var methodSymbol = context.ContainingSymbol as IMethodSymbol;
                if (methodSymbol.IsAsync)
                {
                    var token = context.Node.DescendantTokens().First(t => t.IsKind(SyntaxKind.NumericLiteralToken));
                    var propertyBag = CreatePropertyBag(new KeyValuePair<string, string>("Delay", token.Text));

                    var diagnostic = Diagnostic.Create(ThreadSleepInAsyncRule, expr.GetLocation(), propertyBag);
                    context.ReportDiagnostic(diagnostic);
                }
            }
        }

        /// <summary>
        /// List of exceptions not to be explicity thrown.
        /// See <see href="https://msdn.microsoft.com/en-us/library/ms229007.aspx?tduid=(0b98c168a29f113416632d7bcdd9e02c)(256380)(2459594)(TnL5HPStwNw-6N9P4DHlopl4qDB1YtGAMA)()">documenation</see>
        /// </summary>
        private static readonly string[] InvalidThrownExceptionTypes = { "Exception", "SystemException", "ApplicationException", "StackOverflowException", "OutOfMemoryException" };

        private void AnalyzeThrowExpressionSyntaxNode(SyntaxNodeAnalysisContext context)
        {
            var throwStatementSyntax = (ThrowStatementSyntax)context.Node;
            var exceptionIdentifierSyntax =
                throwStatementSyntax.Expression?.DescendantNodes()
                .Where(n => n.IsKind(SyntaxKind.IdentifierName))
                .Cast<IdentifierNameSyntax>()
                .FirstOrDefault(s => InvalidThrownExceptionTypes.Any(e => e.Equals(s.Identifier.Text)) && s.Ancestors().Any(a => a.IsKind(SyntaxKind.ObjectCreationExpression)));

            if (exceptionIdentifierSyntax == null)
                return;

            var diagnostic = Diagnostic.Create(NoSystemExceptionThrownRule, exceptionIdentifierSyntax.GetLocation(), exceptionIdentifierSyntax.Identifier.Text);
            context.ReportDiagnostic(diagnostic);
        }

        private static void AnalyzeAwaitExpressionSyntaxNode(SyntaxNodeAnalysisContext context)
        {
            if (!context.Node.DescendantNodes().Any(IsConfigureAwaitIdentifier))
            {
                //Task.Yield is a special case.
                InvocationExpressionSyntax expr = context.Node.ChildNodes().FirstOrDefault(n => n.Kind() == SyntaxKind.InvocationExpression) as InvocationExpressionSyntax;
                if (expr != null && expr.ToString() == "Task.Yield()")
                    return;

                var diagnostic = Diagnostic.Create(ConfigureAwaitRule, context.Node.GetLocation());
                context.ReportDiagnostic(diagnostic);

                return;
            }
        }

        private static bool IsConfigureAwaitIdentifier(SyntaxNode node)
        {
            return IsIdentifierNode(node, "^ConfigureAwait(.*)?$");
        }

        private static bool IsIdentifierNode(SyntaxNode node, string pattern)
        {            
            return ((node.Kind() == SyntaxKind.IdentifierName) &&
                Regex.IsMatch(((IdentifierNameSyntax)node).Identifier.ValueText, pattern));
        }
    }
}

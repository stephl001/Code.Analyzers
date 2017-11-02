using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Code.Analyzers.Common.Extensions
{
    public static class SyntaxNodeExtensions
    {
        public static IEnumerable<T> GetTopLevelExpressions<T>(this MethodDeclarationSyntax methodSyntax, Func<SyntaxNode, bool> predicate) where T : SyntaxNode
        {
            return GetDescendingNonBlockNodes(methodSyntax.Body.Statements)
                    .Where(predicate)
                    .OfType<T>();
        }

        public static IEnumerable<T> GetTopLevelNonClosureExpressions<T>(this MethodDeclarationSyntax methodSyntax, Func<SyntaxNode, bool> predicate) where T : SyntaxNode
        {
            return GetDescendingNonClosureNodes(methodSyntax.Body.Statements)
                    .Where(predicate)
                    .OfType<T>();
        }

        private static IEnumerable<SyntaxNode> GetDescendingNonClosureNodes(IEnumerable<SyntaxNode> nodes)
        {
            return nodes.SelectMany(s => s.DescendantNodesAndSelf(IsNotLambdaExpression));
        }

        private static IEnumerable<SyntaxNode> GetDescendingNonBlockNodes(IEnumerable<SyntaxNode> nodes)
        {
            return nodes.SelectMany(s => s.DescendantNodesAndSelf(IsNotBlockExpression));
        }

        private static bool IsNotBlockExpression(SyntaxNode node)
        {
            return (IsNotLambdaExpression(node) && IsNotUsingStatement(node) && IsNotTryStatement(node));
        }

        private static bool IsNotLambdaExpression(SyntaxNode node)
        {
            return !(node.IsKind(SyntaxKind.ParenthesizedLambdaExpression) || node.IsKind(SyntaxKind.SimpleLambdaExpression));
        }

        private static bool IsNotUsingStatement(SyntaxNode node)
        {
            return !node.IsKind(SyntaxKind.UsingStatement);
        }

        private static bool IsNotTryStatement(SyntaxNode node)
        {
            return !node.IsKind(SyntaxKind.TryStatement);
        }
    }
}

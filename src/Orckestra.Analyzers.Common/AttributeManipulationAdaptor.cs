using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Diagnostics;

namespace Code.Analyzers.Common
{
    internal class AttributeManipulationAdaptor
    {
        private readonly SyntaxNode _node;

        public AttributeManipulationAdaptor(SyntaxNode node)
        {
            _node = node;
        }

        internal SyntaxNode WithAttributeLists(SyntaxList<AttributeListSyntax> attrList)
        {
            Debug.Assert((_node is ClassDeclarationSyntax) || (_node is MethodDeclarationSyntax) || (_node is EnumDeclarationSyntax));

            if (_node is ClassDeclarationSyntax)
                return ((ClassDeclarationSyntax)_node).WithAttributeLists(attrList);
            else if (_node is MethodDeclarationSyntax)
                return ((MethodDeclarationSyntax)_node).WithAttributeLists(attrList);

            return ((EnumDeclarationSyntax)_node).WithAttributeLists(attrList);
        }
    }
}
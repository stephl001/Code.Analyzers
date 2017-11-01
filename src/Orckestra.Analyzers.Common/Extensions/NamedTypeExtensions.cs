using Microsoft.CodeAnalysis;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace Orckestra.Analyzers.Common.Extensions
{
    public static class NamedTypeExtensions
    {
        public static bool InheritsFrom(this INamedTypeSymbol typeDeclaration, string typeName, string @namespace)
        {
            if (typeDeclaration.Name != typeName)
                return false;
            if (typeDeclaration.ContainingNamespace.ToString() != @namespace)
                return false;

            return true;
        }

        public static bool IsImplementingInterface(this ITypeSymbol symbol, string name, string @namespace)
        {
            ImmutableArray<INamedTypeSymbol> interfaces = symbol.GetAllImplementedInterfaces();
            bool isImplemented = interfaces.Any(i => ((i.Name == name) && (i.ContainingNamespace.ToString() == @namespace)));

            return isImplemented;
        }

        public static ImmutableArray<INamedTypeSymbol> GetAllImplementedInterfaces(this ITypeSymbol symbol)
        {
            ImmutableArray<INamedTypeSymbol> interfaces = ImmutableArray<INamedTypeSymbol>.Empty;
            
            ITypeSymbol currentSymbol = symbol;
            while (currentSymbol != null)
            {
                interfaces = CollectInterfaces(currentSymbol, interfaces);
                currentSymbol = currentSymbol.BaseType;
            }

            return interfaces;
        }

        private static ImmutableArray<INamedTypeSymbol> CollectInterfaces(ITypeSymbol symbol, ImmutableArray<INamedTypeSymbol> interfaces)
        {
            var newInterfaces = interfaces.AddRange(symbol.Interfaces);
            
            foreach (var i in symbol.Interfaces)
            {
                newInterfaces = CollectInterfaces(i, newInterfaces);
            }

            return newInterfaces;
        }

        public static IEnumerable<ISymbol> GetAllMembers(this ITypeSymbol typeSymbol)
        {
            ITypeSymbol currentSymbol = typeSymbol;
            while (currentSymbol != null)
            {
                foreach (ISymbol s in currentSymbol.GetMembers())
                    yield return s;

                currentSymbol = currentSymbol.BaseType;
            }
        }
    }
}

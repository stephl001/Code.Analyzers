using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.CodeAnalysis;
using System.Collections.Immutable;
using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using Orckestra.Analyzers.Common.Extensions;

namespace Orckestra.Analyzers.Common
{
    public abstract class BaseDiagnosticAnalyzer : DiagnosticAnalyzer
    {
        protected abstract void RegisterActions(AnalysisContext context);

        protected static bool ClassInheritsFrom(ITypeSymbol typeDeclaration, string baseTypeName, string baseTypeNamespace)
        {
            return GetBaseTypes(typeDeclaration).Any(bt => bt.InheritsFrom(baseTypeName, baseTypeNamespace));
        }

        private static IEnumerable<INamedTypeSymbol> GetBaseTypes(ITypeSymbol rootSymbol)
        {
            var currentBaseType = rootSymbol.BaseType;
            while (currentBaseType != null)
            {
                yield return currentBaseType;

                currentBaseType = currentBaseType.BaseType;
            }
        }

        protected static ImmutableDictionary<TKey, TValue> CreatePropertyBag<TKey, TValue>(params KeyValuePair<TKey, TValue>[] properties)
        {
            var propertyBag = ImmutableDictionary.Create<TKey, TValue>();
            propertyBag = propertyBag.AddRange(properties);

            return propertyBag;
        }

        protected static bool IsNamedArgumentUndeclaredOrEmpty(AttributeData attribute, string argumentName)
        {
            KeyValuePair<string, TypedConstant> argument = attribute.NamedArguments.FirstOrDefault(kvp => kvp.Key == argumentName);
            if (argument.Equals(default(KeyValuePair<string, TypedConstant>)))
                return true;

            return (argument.Value.Value.ToString().Length == 0);
        }

        protected static AttributeArgumentSyntax GetAttributeNamedProperty(AttributeSyntax atribute, string propertyName)
        {
            var args = atribute.ArgumentList.Arguments;
            return args.FirstOrDefault(a => MatchNamedProperty(a, propertyName));
        }

        protected static AttributeArgumentSyntax GetAttributePositionArgument(AttributeSyntax atribute, int position)
        {
            var args = atribute.ArgumentList.Arguments;
            return args[position];
        }

        private static bool MatchNamedProperty(AttributeArgumentSyntax arg, string propertyName)
        {
            if (arg.NameEquals == null)
                return false;

            return (arg.NameEquals.GetFirstToken().ValueText == propertyName);
        }

        protected static bool AreAllConditionsMet<T>(T target, params Predicate<T>[] predicates)
        {
            return predicates.All(p => p(target));
        }
    }
}

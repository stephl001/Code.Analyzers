using System.Collections.Immutable;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;
using System.Text.RegularExpressions;

namespace Orckestra.UnitTestAnalyzers.UnitTest
{
    public partial class UnitTestSyntaxAnalyzer
    {
        protected override void RegisterActions(AnalysisContext context)
        {
            // TODO: Consider registering other actions that act on syntax instead of or in addition to symbols
            // See https://github.com/dotnet/roslyn/blob/master/docs/analyzers/Analyzer%20Actions%20Semantics.md for more information
            context.RegisterSymbolAction(AnalyzeSymbol, SymbolKind.Method);
        }

        private static void AnalyzeSymbol(SymbolAnalysisContext context)
        {
            CheckForCategoryAttributes(context);
            CheckForAsyncVoidSignature(context);
            CheckForClientIntegrationTests(context);
            CheckForAsynchronousTests(context);
        }

        private static void CheckForAsynchronousTests(SymbolAnalysisContext context)
        {
            var attributes = context.Symbol.GetAttributes();
            if (IsActiveTestMethod(attributes))
            {
                var method = context.Symbol as IMethodSymbol;
                if (method.IsAsync && (method.ReturnType.Name == "Task") && !attributes.Any(IsValidTestCategoryIntegrationAsyncAttribute))
                {
                    var diagnostic = Diagnostic.Create(AsyncTaskRule, context.Symbol.Locations[0], context.Symbol.Name);
                    context.ReportDiagnostic(diagnostic);
                }
            }
        }

        private static void CheckForClientIntegrationTests(SymbolAnalysisContext context)
        {
            var attributes = context.Symbol.GetAttributes();
            if (IsActiveTestMethod(attributes) && attributes.Any(IsValidTestCategoryIntegrationAttribute))
            {
                var method = context.Symbol as IMethodSymbol;
                var classDeclaration = method.ContainingType;
                if (ClassInheritsFrom(classDeclaration, "ClientIntegrationTestBase", "Orckestra.Overture.Testing.Utilities"))
                {
                    if (!attributes.Any(IsValidDataSourceAttribute))
                    {
                        var diagnostic = Diagnostic.Create(ClientIntegrationTestRule, context.Symbol.Locations[0], context.Symbol.Name);
                        context.ReportDiagnostic(diagnostic);
                    }
                }
            }
        }

        private static void CheckForAsyncVoidSignature(SymbolAnalysisContext context)
        {
            var attributes = context.Symbol.GetAttributes();
            if (IsActiveTestMethod(attributes))
            {
                var method = context.Symbol as IMethodSymbol;
                if (method.IsAsync && method.ReturnsVoid)
                {
                    var diagnostic = Diagnostic.Create(AsyncVoidRule, context.Symbol.Locations[0], context.Symbol.Name);
                    context.ReportDiagnostic(diagnostic);
                }
            }
        }

        private static void CheckForCategoryAttributes(SymbolAnalysisContext context)
        {
            var attributes = context.Symbol.GetAttributes();
            if (IsActiveTestMethod(attributes))
            {
                int countUnitCategory = attributes.Count(IsValidTestCategoryUnitAttribute);
                int countIntegrationCategory = attributes.Count(IsValidTestCategoryIntegrationAttribute);

                if ((countUnitCategory + countIntegrationCategory) != 1)
                {
                    var diagnostic = Diagnostic.Create(TestCategoryRule, context.Symbol.Locations[0], context.Symbol.Name);
                    context.ReportDiagnostic(diagnostic);
                }
            }
        }

        private static bool IsActiveTestMethod(ImmutableArray<AttributeData> attributes)
        {
            if (attributes.Any(IsIgnoredTestMethodAttribute))
                return false;

            return attributes.Any(IsTestMethodAttribute);
        }

        private static bool IsIgnoredTestMethodAttribute(AttributeData attrData)
        {
            return Regex.IsMatch(attrData.AttributeClass.MetadataName, "^Ignore(Attribute)?$");
        }

        private static bool IsTestMethodAttribute(AttributeData attrData)
        {
            return Regex.IsMatch(attrData.AttributeClass.MetadataName, "^TestMethod(Attribute)?$");
        }

        private static bool IsValidTestCategoryUnitAttribute(AttributeData attrData)
        {
            return IsValidTestCategoryAttribute(attrData, "Unit");
        }

        private static bool IsValidTestCategoryIntegrationAttribute(AttributeData attrData)
        {
            return IsValidTestCategoryAttribute(attrData, "Integration");
        }

        private static bool IsValidTestCategoryIntegrationAsyncAttribute(AttributeData attrData)
        {
            return IsValidTestCategoryAttribute(attrData, "Async");
        }

        private static bool IsValidTestCategoryAttribute(AttributeData attrData, string category)
        {
            if (Regex.IsMatch(attrData.AttributeClass.MetadataName, "^TestCategory(Attribute)?$"))
            {
                string val = (string)attrData.ConstructorArguments[0].Value;
                return val == category;
            }

            return false;
        }

        private static bool IsValidDataSourceAttribute(AttributeData attrData)
        {
            if (Regex.IsMatch(attrData.AttributeClass.MetadataName, "^DataSource(Attribute)?$"))
            {
                string val = (string)attrData.ConstructorArguments[0].Value;
                return ((val == "AllFormatsDataSource") || (val == "JsonDataSource") || (val == "XmlDataSource"));
            }

            return false;
        }
    }
}

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;
using System.Linq;

namespace Orckestra.Analyzers.WorkflowFoundation
{
    public partial class WorkflowFoundationSyntaxAnalyzer
    {
        protected override void RegisterActions(AnalysisContext context)
        {
            context.RegisterSymbolAction(AnalyzeField, SymbolKind.Field);
            context.RegisterSymbolAction(AnalyzeProperty, SymbolKind.Property);
        }

        private static void AnalyzeField(SymbolAnalysisContext context)
        {
            var field = context.Symbol as IFieldSymbol;
            if (IsInvalidActivityField(field))
            { 
                var diagnostic = Diagnostic.Create(NoWritableFieldsInActivityRule, field.Locations.First(), field.Locations.Skip(1), field.Name, field.ContainingType.Name);
                context.ReportDiagnostic(diagnostic);
            }            
        }

        private static bool IsInvalidActivityField(IFieldSymbol field)
        {
            return (ClassInheritsFrom(field.ContainingSymbol as INamedTypeSymbol, "Activity", "System.Activities") &&
                    !field.IsReadOnly && !field.IsConst);
        }

        private static void AnalyzeProperty(SymbolAnalysisContext context)
        {
            var property = context.Symbol as IPropertySymbol;
            if (IsInvalidActivityProperty(property))
            {
                var diagnostic = Diagnostic.Create(NoWritablePropertiesInActivityRule, property.Locations.First(), property.Locations.Skip(1), property.Name, property.ContainingType.Name);
                context.ReportDiagnostic(diagnostic);
            }
        }

        private static bool IsInvalidActivityProperty(IPropertySymbol property)
        {
            return (ClassInheritsFrom(property.ContainingSymbol as INamedTypeSymbol, "Activity", "System.Activities") &&
                    !property.IsReadOnly && !property.IsWriteOnly && !IsWorkflowProperty(property));
        }

        private static bool IsWorkflowProperty(IPropertySymbol property)
        {
            return ClassInheritsFrom(property.Type, "Argument", "System.Activities");
        }
    }
}

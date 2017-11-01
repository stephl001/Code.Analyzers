using Microsoft.CodeAnalysis;
using System;

namespace Analyzers.Tests.Common
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
    public class ExpectedWarningDiagnosticAttribute : ExpectedDiagnosticAttribute
    {
        public ExpectedWarningDiagnosticAttribute(string diagnosticId, string message, int line, int column)
            : base(diagnosticId, message, DiagnosticSeverity.Warning, line, column)
        {            
        }
    }
}

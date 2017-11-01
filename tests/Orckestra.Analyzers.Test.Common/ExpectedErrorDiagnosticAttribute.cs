using Microsoft.CodeAnalysis;
using System;

namespace Analyzers.Tests.Common
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
    public class ExpectedErrorDiagnosticAttribute : ExpectedDiagnosticAttribute
    {
        public ExpectedErrorDiagnosticAttribute(string diagnosticId, int line, int column)
            : base(diagnosticId, DiagnosticSeverity.Error, line, column)
        {
        }

        public ExpectedErrorDiagnosticAttribute(string diagnosticId, string message, int line, int column)
            : base(diagnosticId, message, DiagnosticSeverity.Error, line, column)
        {            
        }
    }
}

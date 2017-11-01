using Microsoft.CodeAnalysis;
using System;

namespace Analyzers.Tests.Common
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
    public class ExpectedDiagnosticAttribute : Attribute
    {
        public ExpectedDiagnosticAttribute(string diagnosticId, DiagnosticSeverity severity, int line, int column)
            : this(diagnosticId, null, severity, line, column)
        {
        }

        public ExpectedDiagnosticAttribute(string diagnosticId, string message, DiagnosticSeverity severity, int line, int column)
        {
            DiagnosticId = diagnosticId;
            Message = message;
            Severity = severity;
            Line = line;
            Column = column;
        }

        public int Column { get; private set; }

        public string DiagnosticId { get; }

        public int Line { get; private set; }

        public string Message { get; }

        public DiagnosticSeverity Severity { get; }
    }
}

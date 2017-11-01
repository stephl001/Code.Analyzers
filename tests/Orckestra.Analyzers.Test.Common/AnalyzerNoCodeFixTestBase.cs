using Analyzers.Tests.Common.Helpers;
using Microsoft.CodeAnalysis.CodeFixes;
using Microsoft.CodeAnalysis.Diagnostics;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Analyzers.Tests.Common
{
    public abstract class AnalyzerNoCodeFixTestBase<TAnalyzer> : CodeFixVerifier where TAnalyzer: DiagnosticAnalyzer, new()
    {
        protected override DiagnosticAnalyzer GetCSharpDiagnosticAnalyzer()
        {
            return new TAnalyzer();
        }
    }
}

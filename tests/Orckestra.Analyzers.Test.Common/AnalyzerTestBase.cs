using Analyzers.Tests.Common.Helpers;
using Microsoft.CodeAnalysis.CodeFixes;
using Microsoft.CodeAnalysis.Diagnostics;
using System;
using System.Collections.Generic;
using System.Reflection;
using Xunit;

namespace Analyzers.Tests.Common
{
    public abstract class AnalyzerTestBase<TAnalyzer,TCodeProvider> : CodeFixVerifier where TAnalyzer: DiagnosticAnalyzer, new()
                                                                                              where TCodeProvider : CodeFixProvider, new()
    {
        protected override CodeFixProvider GetCSharpCodeFixProvider()
        {
            var provider =  new TCodeProvider();
            Assert.NotEmpty(provider.FixableDiagnosticIds);
            Assert.Equal(WellKnownFixAllProviders.BatchFixer, provider.GetFixAllProvider());
            return provider;
        }

        protected override DiagnosticAnalyzer GetCSharpDiagnosticAnalyzer()
        {
            return new TAnalyzer();
        }
    }
}

using Analyzers.Tests.Common;
using Analyzers.Tests.Common.Helpers;
using Code.Analyzers.General;
using System.Reflection;
using Xunit;

namespace Code.Analyzers.Tests
{
    public class ThreadSleepAsyncTests : AnalyzerTestBase<GeneralSyntaxAnalyzer, GeneralSyntaxAnalyzerCodeFixProvider>
    {
        private const string WarningMessage = "Do not use Thread.Sleep within an asynchronous context.";

        [Fact]
        [ExpectedWarningDiagnostic(GeneralSyntaxAnalyzer.ThreadSleepInAsyncDiagnosticId, WarningMessage, 11, 13)]
        [ExpectedWarningDiagnostic(GeneralSyntaxAnalyzer.ThreadSleepInAsyncDiagnosticId, WarningMessage, 18, 13)]
        public void TestThreadSleepAsyncDiagnostic()
        {
            VerifyCSharpDiagnostic(SourceStream.ToUri("Sources.ThreadSleepAsync.cs"), MethodBase.GetCurrentMethod());
        }

        [Fact]
        public void TestThreadSleepAsyncCodeFix()
        {
            VerifyCSharpFix(SourceStream.ToUri("Sources.ThreadSleepAsync.cs"), SourceStream.ToUri("Sources.FixedThreadSleepAsync.cs"));
        }
    }
}
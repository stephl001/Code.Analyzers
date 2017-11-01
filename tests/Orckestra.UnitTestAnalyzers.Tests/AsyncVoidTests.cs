using Analyzers.Tests.Common;
using Analyzers.Tests.Common.Helpers;
using Orckestra.UnitTestAnalyzers.UnitTest;
using System.Reflection;
using Xunit;

namespace Orckestra.UnitTestAnalyzers.Tests
{
    public class AsyncVoidTest : AnalyzerTestBase<UnitTestSyntaxAnalyzer, UnitTestSyntaxAnalyzerCodeFixProvider>
    {
        [Fact]
        [ExpectedErrorDiagnostic(UnitTestSyntaxAnalyzer.AsyncVoidDiagnosticId, "The test method 'InvalidTest' is marked as async void and should be marked as async Task.", 11, 27)]
        public void TestAsyncVoidDiagnostic()
        {
            VerifyCSharpDiagnostic(SourceStream.ToUri("Sources.AsyncTest.cs"), MethodBase.GetCurrentMethod());
        }

        [Fact]
        public void TestAsyncVoidCodeFix()
        {
            VerifyCSharpFix(SourceStream.ToUri("Sources.AsyncTest.cs"), SourceStream.ToUri("Sources.FixedAsyncTest.cs"));
        }
    }
}
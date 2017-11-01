using Analyzers.Tests.Common;
using Analyzers.Tests.Common.Helpers;
using Orckestra.UnitTestAnalyzers.UnitTest;
using System.Reflection;
using Xunit;

namespace Orckestra.UnitTestAnalyzers.Tests
{
    public class AsyncTaskTest : AnalyzerTestBase<UnitTestSyntaxAnalyzer, UnitTestSyntaxAnalyzerCodeFixProvider>
    {
        [Fact]
        [ExpectedWarningDiagnostic(UnitTestSyntaxAnalyzer.AsyncTaskDiagnosticId, "The test method 'InvalidTest' should declare an Async TestCategory.", 11, 27)]
        public void TestAsyncTaskDiagnostic()
        {
            VerifyCSharpDiagnostic(SourceStream.ToUri("Sources.AsyncTaskTest.cs"), MethodBase.GetCurrentMethod());
        }

        [Fact]
        public void TestAsyncTaskCodeFix()
        {
            VerifyCSharpFix(SourceStream.ToUri("Sources.AsyncTaskTest.cs"), SourceStream.ToUri("Sources.FixedAsyncTaskTest.cs"));
        }
    }
}
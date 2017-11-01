using Analyzers.Tests.Common;
using Analyzers.Tests.Common.Helpers;
using Orckestra.UnitTestAnalyzers.UnitTest;
using System.Reflection;
using Xunit;

namespace Orckestra.UnitTestAnalyzers.Tests
{
    public class ClientintegrationTest : AnalyzerTestBase<UnitTestSyntaxAnalyzer, UnitTestSyntaxAnalyzerCodeFixProvider>
    {
        [Fact]
        [ExpectedErrorDiagnostic(UnitTestSyntaxAnalyzer.ClientIntegrationTestDiagnosticId, "The test method 'InvalidTest' must declare a DataSourceAttribute.", 11, 21)]
        public void TestClientIntegrationDiagnostic()
        {
            VerifyCSharpDiagnostic(SourceStream.ToUri("Sources.ClientIntegrationTest.cs"), MethodBase.GetCurrentMethod());
        }

        [Fact]
        public void TestClientIntegrationDiagnosticWrongBase()
        {
            VerifyCSharpDiagnostic(SourceStream.ToUri("Sources.ClientIntegrationTest2.cs"));
        }

        [Fact]
        public void TestDataSourceCodeFix()
        {
            VerifyCSharpFix(SourceStream.ToUri("Sources.ClientIntegrationTest.cs"), SourceStream.ToUri("Sources.FixedClientIntegrationTest.cs"));
        }
    }
}
using Analyzers.Tests.Common;
using Analyzers.Tests.Common.Helpers;
using Code.Analyzers.General;
using System.Reflection;
using Xunit;

namespace Code.Analyzers.Tests
{
    public class ConfigureAwaitTest : AnalyzerTestBase<GeneralSyntaxAnalyzer, GeneralSyntaxAnalyzerCodeFixProvider>
    {
        private const string ErrorMessage = "The await expression must end with a call to ConfigureAwait().";
        
        //No diagnostics expected to show up
        [Fact]
        [ExpectedErrorDiagnostic(GeneralSyntaxAnalyzer.ConfigureAwaitDiagnosticId, ErrorMessage, 9, 13)]
        [ExpectedErrorDiagnostic(GeneralSyntaxAnalyzer.ConfigureAwaitDiagnosticId, ErrorMessage, 16, 45)]
        [ExpectedErrorDiagnostic(GeneralSyntaxAnalyzer.ConfigureAwaitDiagnosticId, ErrorMessage, 17, 13)]
        [ExpectedErrorDiagnostic(GeneralSyntaxAnalyzer.ConfigureAwaitDiagnosticId, ErrorMessage, 34, 20)]
        public void TestConfigureAwaitDiagnostic()
        {
            VerifyCSharpDiagnostic(SourceStream.ToUri("Sources.ConfigureAwaitTest.cs"), MethodBase.GetCurrentMethod());
        }

        [Fact]
        public void TestConfigureAwaitCodeFix()
        {
            VerifyCSharpFix(SourceStream.ToUri("Sources.ConfigureAwaitTest.cs"), SourceStream.ToUri("Sources.FixedConfigureAwaitTest.cs"));
        }
    }
}
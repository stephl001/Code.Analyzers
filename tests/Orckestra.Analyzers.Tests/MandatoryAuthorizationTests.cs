using Analyzers.Tests.Common;
using Analyzers.Tests.Common.Helpers;
using Microsoft.CodeAnalysis;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Orckestra.Analyzers.Tests
{
    [TestClass]
    public class MandatoryAuthorizationTests : AnalyzerTestBase<RequestSyntaxAnalyzer, RequestAnalyzerCodeFixProvider>
    {
        [TestMethod]
        public void TestMandatoryAuthorizationDiagnostic()
        {
            var expected = new DiagnosticResult[]
            {
                CreateMandatoryAuthorizationDiagnosticResult("AddAddressToCustomProfileRequest", 14, 18),
            };

            VerifyCSharpDiagnostic(SourceStream.ToUri("Sources.SampleRequest.cs"), expected);
        }

        private DiagnosticResult CreateMandatoryAuthorizationDiagnosticResult(string requestName, int line, int column)
        {
            return new DiagnosticResult
            {
                Id = "AuthorizationMandatoryDiagnostic",
                Message = $"The request {requestName} must declare an AuthorizationAttribute.",
                Severity = DiagnosticSeverity.Error,
                Locations = new[] {
                        new DiagnosticResultLocation("Test0.cs", line, column)
                    }
            };
        }

        [TestMethod]
        public void TestMandatoryAutorizationCodeFix()
        {
            VerifyCSharpFix(SourceStream.ToUri("Sources.SampleRequest.cs"), SourceStream.ToUri("Sources.FixedSampleRequest.cs"), null, true);
        }
    }
}
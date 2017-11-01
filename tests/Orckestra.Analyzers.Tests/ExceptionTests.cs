using Analyzers.Tests.Common;
using Analyzers.Tests.Common.Helpers;
using Orckestra.Analyzers.General;
using System.Reflection;
using Xunit;

namespace Orckestra.Analyzers.Tests
{
    public class ExceptionTests : AnalyzerNoCodeFixTestBase<GeneralSyntaxAnalyzer>
    {
        //No diagnostics expected to show up
        [Fact]
        [ExpectedWarningDiagnostic(GeneralSyntaxAnalyzer.NoSystemExceptionThrownDiagnosticId, "Exception should never be explicitly thrown.", 10, 23)]
        [ExpectedWarningDiagnostic(GeneralSyntaxAnalyzer.NoSystemExceptionThrownDiagnosticId, "Exception should never be explicitly thrown.", 15, 30)]
        [ExpectedWarningDiagnostic(GeneralSyntaxAnalyzer.NoSystemExceptionThrownDiagnosticId, "SystemException should never be explicitly thrown.", 19, 23)]
        [ExpectedWarningDiagnostic(GeneralSyntaxAnalyzer.NoSystemExceptionThrownDiagnosticId, "ApplicationException should never be explicitly thrown.", 24, 23)]
        [ExpectedWarningDiagnostic(GeneralSyntaxAnalyzer.NoSystemExceptionThrownDiagnosticId, "StackOverflowException should never be explicitly thrown.", 29, 23)]
        [ExpectedWarningDiagnostic(GeneralSyntaxAnalyzer.NoSystemExceptionThrownDiagnosticId, "OutOfMemoryException should never be explicitly thrown.", 34, 23)]
        public void TestThrowExceptionDiagnostic()
        {
            VerifyCSharpDiagnostic(SourceStream.ToUri("Sources.ThrowException.cs"), MethodBase.GetCurrentMethod());
        }
    }
}
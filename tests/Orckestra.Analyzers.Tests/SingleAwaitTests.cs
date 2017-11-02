using Analyzers.Tests.Common;
using Analyzers.Tests.Common.Helpers;
using Code.Analyzers.General;
using System.Reflection;
using Xunit;

namespace Code.Analyzers.Tests
{
    public class SingleAwaitTests : AnalyzerTestBase<GeneralSyntaxAnalyzer, GeneralSyntaxAnalyzerCodeFixProvider>
    {
        private const string WarningMessage = "The method {0} can be refactored to get rid of the async keyword.";

        [Fact]
        [ExpectedWarningDiagnostic(GeneralSyntaxAnalyzer.ReturnSingleAwaitDiagnosticId, "The method GetValueAsync can be refactored to get rid of the async keyword.", 14, 32)]
        [ExpectedWarningDiagnostic(GeneralSyntaxAnalyzer.ReturnSingleAwaitDiagnosticId, "The method WaitAsync can be refactored to get rid of the async keyword.", 31, 27)]
        [ExpectedWarningDiagnostic(GeneralSyntaxAnalyzer.ReturnSingleAwaitDiagnosticId, "The method MoreComplexAsync can be refactored to get rid of the async keyword.", 46, 32)]
        [ExpectedWarningDiagnostic(GeneralSyntaxAnalyzer.ReturnSingleAwaitDiagnosticId, "The method MoreComplexAsync2 can be refactored to get rid of the async keyword.", 69, 32)]
        [ExpectedWarningDiagnostic(GeneralSyntaxAnalyzer.ReturnSingleAwaitDiagnosticId, "The method MultipleReturnAwaitAsync can be refactored to get rid of the async keyword.", 116, 32)]
        public void TestSingleAwaitDiagnostic()
        {
            VerifyCSharpDiagnostic(SourceStream.ToUri("Sources.SingleAsyncExpression.cs"), MethodBase.GetCurrentMethod());
        }
        
        [Fact]
        public void TestSingleAwaitDiagnosticCodeFix()
        {
            VerifyCSharpFix(SourceStream.ToUri("Sources.SingleAsyncExpression.cs"), SourceStream.ToUri("Sources.FixedSingleAsyncExpression.cs"));
        }
    }
}
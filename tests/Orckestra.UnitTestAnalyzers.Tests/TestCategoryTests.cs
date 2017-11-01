using Analyzers.Tests.Common;
using Analyzers.Tests.Common.Helpers;
using Orckestra.UnitTestAnalyzers.UnitTest;
using System.Reflection;
using Xunit;

namespace Orckestra.UnitTestAnalyzers.Tests
{
    public class UnitTest : AnalyzerTestBase<UnitTestSyntaxAnalyzer, UnitTestSyntaxAnalyzerCodeFixProvider>
    {
        [Fact]
        public void TestEmpty()
        {
            VerifyCSharpDiagnostic(SourceStream.ToUri("Sources.Empty.cs"));
        }
                
        [Fact]
        [ExpectedErrorDiagnostic(UnitTestSyntaxAnalyzer.TestCategoryDiagnosticId, "The test method 'InvalidTest' must define a TestCategory attribute with exactly one of the following values: Unit, Integration.", 9, 21)]
        [ExpectedErrorDiagnostic(UnitTestSyntaxAnalyzer.TestCategoryDiagnosticId, "The test method 'InvalidTest2' must define a TestCategory attribute with exactly one of the following values: Unit, Integration.", 14, 21)]
        [ExpectedErrorDiagnostic(UnitTestSyntaxAnalyzer.TestCategoryDiagnosticId, "The test method 'InvalidCategoryTest' must define a TestCategory attribute with exactly one of the following values: Unit, Integration.", 37, 21)]
        [ExpectedErrorDiagnostic(UnitTestSyntaxAnalyzer.TestCategoryDiagnosticId, "The test method 'InvalidUnitIntegrationTest' must define a TestCategory attribute with exactly one of the following values: Unit, Integration.", 50, 21)]
        public void TestWrongCategoryDiagnostic()
        {   
            VerifyCSharpDiagnostic(SourceStream.ToUri("Sources.MissingTestCategory.cs"), MethodBase.GetCurrentMethod());
        }

        [Fact]
        public void TestCodeFix()
        {            
            VerifyCSharpFix(SourceStream.ToUri("Sources.MissingTestCategory.cs"), SourceStream.ToUri("Sources.FixedMissingTestCategory.cs"));
        }
    }
}
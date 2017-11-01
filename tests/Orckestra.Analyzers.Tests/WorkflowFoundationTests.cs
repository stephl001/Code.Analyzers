using Analyzers.Tests.Common;
using Analyzers.Tests.Common.Helpers;
using Orckestra.Analyzers.WorkflowFoundation;
using System.Reflection;
using Xunit;

namespace Orckestra.Analyzers.Tests
{
    public class WorkflowFoundationTests : AnalyzerNoCodeFixTestBase<WorkflowFoundationSyntaxAnalyzer>
    {
        [Fact]
        [ExpectedWarningDiagnostic(WorkflowFoundationSyntaxAnalyzer.NoWritableFieldsInActivityDiagnosticId, "Field _backingField of class TaskActivity is not allowed.", 13, 21)]
        [ExpectedWarningDiagnostic(WorkflowFoundationSyntaxAnalyzer.NoWritablePropertiesInActivityDiagnosticId, "Property WritableProperty of class TaskActivity is not allowed.", 19, 23)]
        public void TestNowritableFieldsInActivity()
        {
            VerifyCSharpDiagnostic(SourceStream.ToUri("Sources.TaskActivity.cs"), MethodBase.GetCurrentMethod());
        }
    }
}
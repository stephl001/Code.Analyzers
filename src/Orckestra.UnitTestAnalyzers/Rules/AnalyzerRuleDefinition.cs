using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;
using Orckestra.Analyzers.Common;
using Microsoft.CodeAnalysis.CodeFixes;
using System.Composition;

namespace Orckestra.UnitTestAnalyzers.UnitTest
{
	[DiagnosticAnalyzer(LanguageNames.CSharp)]
	public partial class UnitTestSyntaxAnalyzer : BaseDiagnosticAnalyzer
	{	
		public const string AsyncTaskDiagnosticId = "AsyncTaskDiagnostic";
		public const string ClientIntegrationTestDiagnosticId = "ClientIntegrationTestDiagnostic";
		public const string TestCategoryDiagnosticId = "TestCategoryDiagnostic";
		public const string AsyncVoidDiagnosticId = "AsyncVoidDiagnostic";
		
		private static DiagnosticDescriptor AsyncTaskRule = new DiagnosticDescriptor(AsyncTaskDiagnosticId, "Asynchronous test methods should declare an Async TestCategory.", "The test method '{0}' should declare an Async TestCategory.", "Naming", DiagnosticSeverity.Warning, isEnabledByDefault: true, description: "All asynchronous test methods should declare an Async TestCategory attribute. This will help the test runner to use an appropriate test configuration for the test.");
		private static DiagnosticDescriptor ClientIntegrationTestRule = new DiagnosticDescriptor(ClientIntegrationTestDiagnosticId, "ClientIntegration based tests must declare a DataSource", "The test method '{0}' must declare a DataSourceAttribute.", "Naming", DiagnosticSeverity.Error, isEnabledByDefault: true, description: "All test methods marked with a TestCategory Integration and that is declared in a class inheriting from ClientIntegrationTestBase must declare a dataSource attribute.");
		private static DiagnosticDescriptor TestCategoryRule = new DiagnosticDescriptor(TestCategoryDiagnosticId, "TestCategory Mandatory", "The test method '{0}' must define a TestCategory attribute with exactly one of the following values: Unit, Integration.", "Naming", DiagnosticSeverity.Error, isEnabledByDefault: true, description: "All unit tests are required to have a valid TestCategory attribute.");
		private static DiagnosticDescriptor AsyncVoidRule = new DiagnosticDescriptor(AsyncVoidDiagnosticId, "Async void tests are invalid", "The test method '{0}' is marked as async void and should be marked as async Task.", "Naming", DiagnosticSeverity.Error, isEnabledByDefault: true, description: "Asynchronous unit tests must use the signature async Task.");
		
		public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get { return ImmutableArray.Create(AsyncTaskRule, ClientIntegrationTestRule, TestCategoryRule, AsyncVoidRule); } }

		public override sealed void Initialize(AnalysisContext context)
		{
			#if !DEBUG
			context.EnableConcurrentExecution();
			#endif
			
			RegisterActions(context);
		}
	}

	[ExportCodeFixProvider(LanguageNames.CSharp, Name = nameof(UnitTestSyntaxAnalyzerCodeFixProvider)), Shared]
	public partial class UnitTestSyntaxAnalyzerCodeFixProvider : BaseCodeFixProvider
	{
		public sealed override ImmutableArray<string> FixableDiagnosticIds
		{
			get { return ImmutableArray.Create<string>(UnitTestSyntaxAnalyzer.AsyncTaskDiagnosticId, UnitTestSyntaxAnalyzer.ClientIntegrationTestDiagnosticId, UnitTestSyntaxAnalyzer.TestCategoryDiagnosticId, UnitTestSyntaxAnalyzer.AsyncVoidDiagnosticId); }
		}

		public sealed override FixAllProvider GetFixAllProvider()
		{
			// See https://github.com/dotnet/roslyn/blob/master/docs/analyzers/FixAllProvider.md for more information on Fix All Providers
			return WellKnownFixAllProviders.BatchFixer;
		}
	}
}


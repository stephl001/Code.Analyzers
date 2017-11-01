using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;
using Orckestra.Analyzers.Common;
using Microsoft.CodeAnalysis.CodeFixes;
using System.Composition;

namespace Orckestra.Analyzers.General
{
	[DiagnosticAnalyzer(LanguageNames.CSharp)]
	public partial class GeneralSyntaxAnalyzer : BaseDiagnosticAnalyzer
	{	
		public const string ReturnSingleAwaitDiagnosticId = "ReturnSingleAwaitDiagnostic";
		public const string ConfigureAwaitDiagnosticId = "ConfigureAwaitDiagnostic";
		public const string NoSystemExceptionThrownDiagnosticId = "NoSystemExceptionThrownDiagnostic";
		public const string ThreadSleepInAsyncDiagnosticId = "ThreadSleepInAsyncDiagnostic";
		
		private static DiagnosticDescriptor ReturnSingleAwaitRule = new DiagnosticDescriptor(ReturnSingleAwaitDiagnosticId, "Await expression can be simplified", "The method {0} can be refactored to get rid of the async keyword.", "Naming", DiagnosticSeverity.Warning, isEnabledByDefault: true, description: "When an async method awaits a single statement and this statement is the last statement of the method, we can get rid of the async keyword and return the Task directly instead.");
		private static DiagnosticDescriptor ConfigureAwaitRule = new DiagnosticDescriptor(ConfigureAwaitDiagnosticId, "Every await expression must end with a ConfigureAwait() call.", "The await expression must end with a call to ConfigureAwait().", "Naming", DiagnosticSeverity.Error, isEnabledByDefault: true, description: "Every await expression must end with a call to ConfigureAwait() to avoid deadlocks.");
		private static DiagnosticDescriptor NoSystemExceptionThrownRule = new DiagnosticDescriptor(NoSystemExceptionThrownDiagnosticId, "System exception should never be explicitly thrown.", "{0} should never be explicitly thrown.", "Naming", DiagnosticSeverity.Warning, isEnabledByDefault: true, description: "System exception should never be explicitly thrown.");
		private static DiagnosticDescriptor ThreadSleepInAsyncRule = new DiagnosticDescriptor(ThreadSleepInAsyncDiagnosticId, "Thread.Sleep should never be called within an asynchronous calling context.", "Do not use Thread.Sleep within an asynchronous context.", "Naming", DiagnosticSeverity.Warning, isEnabledByDefault: true, description: "Thread.Sleep will block the current thread. Within an asynchronous call context, this would defeat the purpse of asynchronity.");
		
		public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get { return ImmutableArray.Create(ReturnSingleAwaitRule, ConfigureAwaitRule, NoSystemExceptionThrownRule, ThreadSleepInAsyncRule); } }

		public override sealed void Initialize(AnalysisContext context)
		{
			#if !DEBUG
			context.EnableConcurrentExecution();
			#endif
			
			context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.None);

			RegisterActions(context);
		}
	}

		[ExportCodeFixProvider(LanguageNames.CSharp, Name = nameof(GeneralSyntaxAnalyzerCodeFixProvider)), Shared]
	public partial class GeneralSyntaxAnalyzerCodeFixProvider : BaseCodeFixProvider
	{
		public sealed override ImmutableArray<string> FixableDiagnosticIds
		{
			get { return ImmutableArray.Create<string>(GeneralSyntaxAnalyzer.ReturnSingleAwaitDiagnosticId, GeneralSyntaxAnalyzer.ConfigureAwaitDiagnosticId, GeneralSyntaxAnalyzer.ThreadSleepInAsyncDiagnosticId); }
		}

		public sealed override FixAllProvider GetFixAllProvider()
		{
			// See https://github.com/dotnet/roslyn/blob/master/docs/analyzers/FixAllProvider.md for more information on Fix All Providers
			return WellKnownFixAllProviders.BatchFixer;
		}
	}
	}
namespace Orckestra.Analyzers.WorkflowFoundation
{
	[DiagnosticAnalyzer(LanguageNames.CSharp)]
	public partial class WorkflowFoundationSyntaxAnalyzer : BaseDiagnosticAnalyzer
	{	
		public const string NoWritableFieldsInActivityDiagnosticId = "NoWritableFieldsInActivityDiagnostic";
		public const string NoWritablePropertiesInActivityDiagnosticId = "NoWritablePropertiesInActivityDiagnostic";
		
		private static DiagnosticDescriptor NoWritableFieldsInActivityRule = new DiagnosticDescriptor(NoWritableFieldsInActivityDiagnosticId, "A workflow activity should not declare any writable backing fields", "Field {0} of class {1} is not allowed.", "Naming", DiagnosticSeverity.Warning, isEnabledByDefault: true, description: "Fields and properties should not be used within activities since they are being shared between workflows. You should use constants or store values in the workflow context.");
		private static DiagnosticDescriptor NoWritablePropertiesInActivityRule = new DiagnosticDescriptor(NoWritablePropertiesInActivityDiagnosticId, "A workflow activity should not declare any writable properties", "Property {0} of class {1} is not allowed.", "Naming", DiagnosticSeverity.Warning, isEnabledByDefault: true, description: "Fields and properties should not be used within activities since they are being shared between workflows. You should use constants or store values in the workflow context.");
		
		public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get { return ImmutableArray.Create(NoWritableFieldsInActivityRule, NoWritablePropertiesInActivityRule); } }

		public override sealed void Initialize(AnalysisContext context)
		{
			#if !DEBUG
			context.EnableConcurrentExecution();
			#endif
			
			context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.None);

			RegisterActions(context);
		}
	}

	}


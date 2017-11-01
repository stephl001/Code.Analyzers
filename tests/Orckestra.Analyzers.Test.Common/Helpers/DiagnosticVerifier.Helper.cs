using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.CodeAnalysis.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;

namespace Analyzers.Tests.Common.Helpers
{
    /// <summary>
    /// Class for turning strings into documents and getting the diagnostics on them
    /// All methods are static
    /// </summary>
    [ExcludeFromCodeCoverage]
    public abstract partial class DiagnosticVerifier
    {
        private static readonly MetadataReference CorlibReference = MetadataReference.CreateFromFile(typeof(object).Assembly.Location);
        private static readonly MetadataReference SystemCoreReference = MetadataReference.CreateFromFile(typeof(Enumerable).Assembly.Location);
        private static readonly MetadataReference SystemActivitiesReference = MetadataReference.CreateFromFile(typeof(System.Activities.Activity).Assembly.Location);
        private static readonly MetadataReference RuntimeSerializationReference = MetadataReference.CreateFromFile(typeof(DataContractAttribute).Assembly.Location);
        private static readonly MetadataReference CSharpSymbolsReference = MetadataReference.CreateFromFile(typeof(CSharpCompilation).Assembly.Location);
        private static readonly MetadataReference CodeAnalysisReference = MetadataReference.CreateFromFile(typeof(Compilation).Assembly.Location);
        private static readonly MetadataReference UnitTestingReference = MetadataReference.CreateFromFile(typeof(Assert).Assembly.Location);
        private static readonly MetadataReference AnalyzersTestCommonReference = MetadataReference.CreateFromFile(typeof(DiagnosticVerifier).Assembly.Location);

        internal static string DefaultFilePathPrefix = "Test";
        internal static string CSharpDefaultFileExt = "cs";
        internal static string VisualBasicDefaultExt = "vb";
        internal static string TestProjectName = "TestProject";

        #region  Get Diagnostics

        /// <summary>
        /// Given classes in the form of strings, their language, and an IDiagnosticAnlayzer to apply to it, return the diagnostics found in the string after converting it to a document.
        /// </summary>
        /// <param name="sources">Classes in the form of strings</param>
        /// <param name="language">The language the source classes are in</param>
        /// <param name="analyzer">The analyzer to be run on the sources</param>
        /// <returns>An IEnumerable of Diagnostics that surfaced in the source code, sorted by Location</returns>
        private static Diagnostic[] GetSortedDiagnostics(Uri[] sourcePaths, string language, DiagnosticAnalyzer analyzer, params MetadataReference[] additionalReferences)
        {
            return GetSortedDiagnosticsFromDocuments(analyzer, GetDocuments(sourcePaths, language, additionalReferences));
        }

        /// <summary>
        /// Given an analyzer and a document to apply it to, run the analyzer and gather an array of diagnostics found in it.
        /// The returned diagnostics are then ordered by location in the source document.
        /// </summary>
        /// <param name="analyzer">The analyzer to run on the documents</param>
        /// <param name="documents">The Documents that the analyzer will be run on</param>
        /// <returns>An IEnumerable of Diagnostics that surfaced in the source code, sorted by Location</returns>
        protected static Diagnostic[] GetSortedDiagnosticsFromDocuments(DiagnosticAnalyzer analyzer, Document[] documents)
        {
            var projects = new HashSet<Project>();
            foreach (var document in documents)
            {
                projects.Add(document.Project);
            }

            var diagnostics = new List<Diagnostic>();
            foreach (var project in projects)
            {
                var compilationWithAnalyzers = project.GetCompilationAsync().Result.WithAnalyzers(ImmutableArray.Create(analyzer));
                var diags = compilationWithAnalyzers.GetAnalyzerDiagnosticsAsync().Result;
                foreach (var diag in diags)
                {
                    if (diag.Location == Location.None || diag.Location.IsInMetadata)
                    {
                        diagnostics.Add(diag);
                    }
                    else
                    {
                        for (int i = 0; i < documents.Length; i++)
                        {
                            var document = documents[i];
                            var tree = document.GetSyntaxTreeAsync().Result;
                            if (tree == diag.Location.SourceTree)
                            {
                                diagnostics.Add(diag);
                            }
                        }
                    }
                }
            }

            var results = SortDiagnostics(diagnostics);
            diagnostics.Clear();
            return results;
        }

        /// <summary>
        /// Sort diagnostics by location in source document
        /// </summary>
        /// <param name="diagnostics">The list of Diagnostics to be sorted</param>
        /// <returns>An IEnumerable containing the Diagnostics in order of Location</returns>
        private static Diagnostic[] SortDiagnostics(IEnumerable<Diagnostic> diagnostics)
        {
            return diagnostics.OrderBy(d => d.Location.SourceSpan.Start).ToArray();
        }

        /// <summary>
        /// Sort diagnostic results by location in source document
        /// </summary>
        /// <param name="diagnostics">The list of DiagnosticResult to be sorted</param>
        /// <returns>An IEnumerable containing the DiagnosticResult in order of Location</returns>
        private static DiagnosticResult[] SortDiagnosticResults(IEnumerable<DiagnosticResult> diagnostics)
        {
            return diagnostics.OrderBy(dr => (dr.Line*1000 + dr.Column)).ToArray();
        }

        #endregion

        #region Set up compilation and documents
        /// <summary>
        /// Given an array of strings as sources and a language, turn them into a project and return the documents and spans of it.
        /// </summary>
        /// <param name="sources">Classes in the form of strings</param>
        /// <param name="language">The language the source code is in</param>
        /// <returns>A Tuple containing the Documents produced from the sources and their TextSpans if relevant</returns>
        private static Document[] GetDocuments(Uri[] sourcePaths, string language, params MetadataReference[] additionalReferences)
        {
            if (language != LanguageNames.CSharp && language != LanguageNames.VisualBasic)
            {
                throw new ArgumentException("Unsupported Language");
            }

            var project = CreateProject(sourcePaths, language, additionalReferences);
            var documents = project.Documents.ToArray();

            if (sourcePaths.Length != documents.Length)
            {
                throw new SystemException("Amount of sources did not match amount of Documents created");
            }

            return documents;
        }

        /// <summary>
        /// Create a Document from a string through creating a project that contains it.
        /// </summary>
        /// <param name="source">Classes in the form of a string</param>
        /// <param name="language">The language the source code is in</param>
        /// <returns>A Document created from the source string</returns>
        protected static Document CreateDocument(Uri source, string language = LanguageNames.CSharp, params MetadataReference[] additionalReferences)
        {
            return CreateProject(new[] { source }, language, additionalReferences).Documents.First();
        }

        /// <summary>
        /// Create a project using the inputted strings as sources.
        /// </summary>
        /// <param name="sources">Classes in the form of strings</param>
        /// <param name="language">The language the source code is in</param>
        /// <returns>A Project created out of the Documents created from the source strings</returns>
        private static Project CreateProject(Uri[] sourcePaths, string language = LanguageNames.CSharp, params MetadataReference[] additionalreferences)
        {
            string fileNamePrefix = DefaultFilePathPrefix;
            string fileExt = language == LanguageNames.CSharp ? CSharpDefaultFileExt : VisualBasicDefaultExt;

            var projectId = ProjectId.CreateNewId(debugName: TestProjectName);

            var solution = new AdhocWorkspace()
                .CurrentSolution
                .AddProject(projectId, TestProjectName, TestProjectName, language)
                .AddMetadataReference(projectId, CorlibReference)
                .AddMetadataReference(projectId, SystemCoreReference)
                .AddMetadataReference(projectId, SystemActivitiesReference)
                .AddMetadataReference(projectId, RuntimeSerializationReference)
                .AddMetadataReference(projectId, CSharpSymbolsReference)
                .AddMetadataReference(projectId, CodeAnalysisReference)
                .AddMetadataReference(projectId, UnitTestingReference)
                .AddMetadataReference(projectId, AnalyzersTestCommonReference);

            //In case we have some more references to add to the project...
            solution = solution.AddMetadataReferences(projectId, additionalreferences);

            int count = 0;
            foreach (var sourcePath in sourcePaths)
            {
                var newFileName = fileNamePrefix + count + "." + fileExt;
                var documentId = DocumentId.CreateNewId(projectId, debugName: newFileName);

                using (Stream stream = SourceStream.FromUri(sourcePath))
                {
                    solution = solution.AddDocument(documentId, newFileName, SourceText.From(stream));
                }
                    
                count++;
            }

            var p = solution.GetProject(projectId);

            CSharpCompilationOptions co = new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary, warningLevel: 1);
            return p.WithCompilationOptions(co);
        }
        #endregion
    }
}


using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Analyzers.Tests.Common.Helpers
{
    public static class SourceStream
    {
        public static Stream FromUri(Uri path)
        {
            Debug.Assert(path.Scheme == "res");

            Assembly assembly = AppDomain.CurrentDomain.GetAssemblies().First(a => StringComparer.InvariantCultureIgnoreCase.Compare(a.GetName().Name, path.Host) == 0);
            string resourceName = assembly.GetManifestResourceNames().First(name => name.ToUpperInvariant().EndsWith(path.PathAndQuery.Replace('/', '.').ToUpperInvariant()));

            return assembly.GetManifestResourceStream(resourceName);
        }

        public static Uri ToUri(string resourceName)
        {
            Assembly callingAssembly = Assembly.GetCallingAssembly();
            string[] resourceNames = callingAssembly.GetManifestResourceNames();

            string fullName = resourceNames.Single(rn => rn.EndsWith(resourceName));
            UriBuilder builder = new UriBuilder("res", callingAssembly.GetName().Name);
            builder.Path = resourceName;

            return builder.Uri;
        }
    }
}

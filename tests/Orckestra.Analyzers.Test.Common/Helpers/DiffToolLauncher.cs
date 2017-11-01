using IniParser;
using IniParser.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Orckestra.Analyzers.Test.Common.Helpers
{
    [ExcludeFromCodeCoverage]
    public static class DiffToolLauncher
    {
        [Conditional("DebugDiffTool")]
        public static void LaunchIfNotEqual(string actual, string expected)
        {
            if (actual.Equals(expected))
                return;

            string diffExecutable = GetDiffToolPath();
            if ((diffExecutable == null) || !File.Exists(diffExecutable))
                return;

            string tmpDirPath = GetTemporaryDirectory();
             
            string actualPath = Path.Combine(tmpDirPath, "Actual.cs");
            string expectedPath = Path.Combine(tmpDirPath, "Expected.cs"); ;

            File.WriteAllText(actualPath, actual);
            File.WriteAllText(expectedPath, expected);

            ProcessStartInfo psi = new ProcessStartInfo(diffExecutable);
            psi.Arguments = $"\"{actualPath}\" \"{expectedPath}\"";
            psi.UseShellExecute = true;

            Process.Start(psi);
        }

        private static string GetTemporaryDirectory()
        {
            string tempDirectory = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
            Directory.CreateDirectory(tempDirectory);
            return tempDirectory;
        }

        private static string GetDiffToolPath()
        {
            string gitConfigPath = GetGitConfigPath();
            if ((gitConfigPath == null) || !File.Exists(gitConfigPath))
                return null;

            string diffToolPath = GetDiffToolPathFromGitConfig(gitConfigPath);
            return diffToolPath;
        }

        private static string GetDiffToolPathFromGitConfig(string gitConfigPath)
        {
            var parser = new FileIniDataParser();
            IniData data = parser.ReadFile(gitConfigPath);

            string diffTool = data["diff"]?["tool"];
            string diffToolPath = data[$"difftool \"{diffTool}\""]?["path"];
            if (diffToolPath == null)
                return null;

            Regex pattern = new Regex("^[\"\']?\\/([a-z])\\/([^\"\']+)[\"\']?$");
            string realPath = pattern.Replace(diffToolPath, @"$1:/$2");

            return realPath;
        }

        private static string GetGitConfigPath()
        {
            string homePath = Environment.GetEnvironmentVariable("HOME");
            if (string.IsNullOrEmpty(homePath))
                return null;

            return Path.Combine(homePath, ".gitconfig");
        }
    }
}

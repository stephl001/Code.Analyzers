using System;
using System.Diagnostics.CodeAnalysis;

namespace ServiceStack
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    [ExcludeFromCodeCoverage]
    public class RouteAttribute : Attribute
    {
        public RouteAttribute(string path)
        { }

        public RouteAttribute(string path, string verbs)
        { }

        public string Notes { get; set; }
        public string Path { get; set; }
        public int Priority { get; set; }
        public string Summary { get; set; }
        public string Verbs { get; set; }
    }
}
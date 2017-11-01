using System;
using System.Diagnostics.CodeAnalysis;

namespace ServiceStack
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    [ExcludeFromCodeCoverage]
    public class ApiAttribute : Attribute
    {
        public ApiAttribute()
        { }

        public ApiAttribute(string description)
        { }

        public string Description { get; set; }
    }
}
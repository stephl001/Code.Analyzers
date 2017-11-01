using System;
namespace Orckestra.Overture.Routing
{
    /// <summary>
    /// Marks a request as Preview.
    /// </summary>
    /// <seealso cref="System.Attribute" />
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class PreviewAttribute : Attribute
    {
    }
}

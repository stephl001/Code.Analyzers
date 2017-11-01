using System;
using System.Diagnostics.CodeAnalysis;

namespace Orckestra.Overture
{
    /// <summary>
    /// This attribute should be placed on all domain request types to differenciate them from service request types.
    /// </summary>
    /// <remarks>
    /// This is a multicast attribute so it can be declared at assembly level and can target multiple types. 
    /// See <see cref="AttributeTargetTypes"/>.
    /// </remarks>
    [AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
    [ExcludeFromCodeCoverage]
    public sealed class DomainRequestAttribute : Attribute
    {
        /// <summary>
        /// A wildcard or regular expression specifying to which types this instance applies, or null this instance applies 
        /// either to all types. Regular expressions should start with the regex: prefix.
        /// </summary>
        public string AttributeTargetTypes
        {
            get; set;
        }
    }
}

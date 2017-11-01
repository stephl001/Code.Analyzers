using System;
using System.Diagnostics.CodeAnalysis;

namespace Orckestra.Overture.Security
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    [ExcludeFromCodeCoverage]
    public class AuthorizationAttribute : Attribute
    {
        public AuthorizationAttribute(AllowAnonymousTag tag)
        {
            ModuleName = Authorization.Modules.Any;
            RoleName = Authorization.Roles.None;
            AllowAnonymous = true;
        }

        public AuthorizationAttribute(string moduleName, string roleName)
        {

        }

        public bool AllowAnonymous { get; }

        public string ModuleName { get; }

        public string RoleName { get; }
    }
}
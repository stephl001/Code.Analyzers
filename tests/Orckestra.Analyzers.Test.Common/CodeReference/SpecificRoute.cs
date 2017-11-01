using ServiceStack;
using System.Diagnostics.CodeAnalysis;

namespace Orckestra.Overture.Routing
{
    [ExcludeFromCodeCoverage]
    public class SpecificRouteAttribute : RouteAttribute
    {
        public SpecificRouteAttribute(ApiType apiType, string path)
            : base(path)
        { }

        public SpecificRouteAttribute(ApiType apiType, string path, string verbs)
            : base(path, verbs)
        { }

        public ApiType ApiType { get; set; }

        public static string GenerateSpecificRoutePathFromRouteAttribute(RouteAttribute routeAttribute)
        {
            return null;
        }

        public static string GenerateSpecificRouteUrlFromRoutePath(string path)
        {
            return null;
        }
    }
}
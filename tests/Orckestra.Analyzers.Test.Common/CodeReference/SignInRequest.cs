using ServiceStack;
using ServiceStack.Web;
using System.Diagnostics.CodeAnalysis;

namespace Orckestra.Overture.ServiceModel.Requests.Authentication
{
    [ExcludeFromCodeCoverage]
    public class SignInRequest : IReturn<IHttpResult>
    {
        public string ReturnUrl { get; set; }
    }
}

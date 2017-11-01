using Orckestra.Overture.ServiceModel.Requests.Authentication;
using ServiceStack.Web;

namespace Orckestra.Overture.Foundation.ApplicationServices
{
    public class AuthenticationApplicationService : ServiceBase
    {
        public IHttpResult Any(SignInRequest request)
        {
            return null;
        }

        public IHttpResult NotAny(SignOutRequest request)
        {
            return null;
        }

        private void ProcessToken(string token)
        {
        }

        protected void SomeMethod(string token)
        {
        }

        public void NoRequestPublicMethod()
        {
        }

        public void NotRequestPublicMethod(int notRequest)
        {
        }

        public IHttpResult Any(int request)
        {
            return null;
        }
    }
}
using Orckestra.Overture.Security;
using Orckestra.Overture.ServiceModel.Requests.Customers.CustomProfiles;
using ServiceStack;
using System.Runtime.Serialization;

namespace Orckestra.Overture.ServiceModel.Requests
{
    [Api(CustomProfilesDocumentation.Api)]
    [Authorization(Authorization.Modules.Any, Authorization.Roles.Profiles.Editor)]
    [Authorization(Authorization.Modules.Any, Authorization.Roles.None), Authorization(Authorization.Modules.Any, Authorization.Roles.None)]
    [Authorization(Authorization.Modules.Any, Authorization.Roles.None)]
    [Authorization(/*comment*/Authorization.Modules.Any  /*more comments*/, Authorization.Roles.Profiles.Editor)]
    [Authorization(AllowAnonymousTag.AllowAnonymous)]
    [Authorization(Authorization.Modules.Orders, Authorization.Roles.Profiles.Editor)]
    [Route("/customProfiles/{ScopeId}/{EntityId}/addresses",
        Verbs = "POST",
        Notes = "Add an address to a custom profiles's addresses.",
        Summary = "Add an address to a custom profile's addresses - {AddAddressToCustomProfileRequest}")]
    [DataContract]
    public class AddAddressToCustomProfileRequest : ScopedRequest, IReturn<Address>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AddAddressToCustomProfileRequest"/> class.
        /// </summary>
		public AddAddressToCustomProfileRequest()
        {
        }
    }
}

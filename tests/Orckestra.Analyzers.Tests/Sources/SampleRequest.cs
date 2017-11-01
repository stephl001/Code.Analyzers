using Orckestra.Overture.Routing;
using Orckestra.Overture.ServiceModel.Requests.Customers.CustomProfiles;
using ServiceStack;
using System.Runtime.Serialization;

namespace Orckestra.Overture.ServiceModel.Requests
{
    [Api(CustomProfilesDocumentation.Api)]
    [RouteAttribute("/customProfiles/{ScopeId}/{EntityId}/addresses",
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

    [Api(CustomProfilesDocumentation.Api)]
    [Orckestra.Overture.Security.Authorization(Orckestra.Overture.Security.Authorization.Modules.Profiles, Orckestra.Overture.Security.Authorization.Roles.Profiles.Editor)]
    [SpecificRoute(ApiType.Customer,
        "/customProfiles/{ScopeId}/otheraddresses",
        Verbs = "POST",
        Notes = "Add a line item to the first shipment of a cart",
        Summary = "Add a line item to the first shipment of a cart - {AddOtherAddressToCustomProfileRequest}")]
    [ServiceStack.Route("/customProfiles/{ScopeId}/{EntityId}/otheraddresses",
        Verbs = "POST",
        Notes = "Add an address to a custom profiles's addresses.",
        Summary = "Add an address to a custom profile's addresses - {AddOtherAddressToCustomProfileRequest}")]
    [DataContract]
    public class AddOtherAddressToCustomProfileRequest : ScopedRequest, IReturn<Address>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AddAddressToCustomProfileRequest"/> class.
        /// </summary>
		public AddOtherAddressToCustomProfileRequest()
        {
        }
    }

    [DataContract]
    public enum DummyEnum
    {
        Test
    }
}

using Orckestra.Overture.Routing;
using Orckestra.Overture.Security;
using Orckestra.Overture.ServiceModel.Requests.Customers.CustomProfiles;
using ServiceStack;
using System;
using System.Runtime.Serialization;

namespace Orckestra.Overture.ServiceModel.Requests
{
    [Api(CustomProfilesDocumentation.Api)]
    [Authorization(Authorization.Modules.Orders, Authorization.Roles.Profiles.Editor)]
    [SpecificRoute(ApiType.Customer, "/customProfiles/{ScopeId}/{EntityId}/otheraddresses",
        Verbs = "POST",
        Notes = "Add an address to a custom profiles's addresses.",
        Summary = "This is a wrong summary - {" + nameof(AddAddressToCustomProfileRequest) + "}")]
    [Route("/customProfiles/{ScopeId}/{EntityId}/{CustomerId}/otheraddresses",
        Verbs = "POST",
        Notes = "Add an address to a custom profiles's addresses.",
        Summary = "This is a wrong summary - {" + nameof(AddAddressToCustomProfileRequest) + "}")]
    [DataContract]
    public class AddAddressToCustomProfileRequest : ScopedRequest, IReturn<Address>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AddAddressToCustomProfileRequest"/> class.
        /// </summary>
		public AddAddressToCustomProfileRequest()
        {
        }

        public Guid CustomerId { get; set; }
    }

    [Api(CustomProfilesDocumentation.Api)]
    [Authorization(Authorization.Modules.Orders, Authorization.Roles.Profiles.Editor)]
    [SpecificRoute(ApiType.Customer, "/customProfiles/{ScopeId}/{EntityId}/otheraddresses",
        Verbs = "POST",
        Notes = "Add an address to a custom profiles's addresses.",
        Summary = "This is a wrong summary - {" + nameof(AddAddressToCustomProfile2Request) + "}")]
    [Route("/customProfiles/{ScopeId}/{EntityId}/{CustomerId}/otheraddresses",
        Verbs = "POST",
        Notes = "Add an address to a custom profiles's addresses.",
        Summary = "This is a wrong summary - {" + nameof(AddAddressToCustomProfile2Request) + "}")]
    [DataContract]
    public class AddAddressToCustomProfile2Request : ScopedRequest, IReturn<Address>, ICustomerRequest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AddAddressToCustomProfileRequest"/> class.
        /// </summary>
		public AddAddressToCustomProfile2Request()
        {
        }

        public Guid CustomerId { get; set; }
    }
}

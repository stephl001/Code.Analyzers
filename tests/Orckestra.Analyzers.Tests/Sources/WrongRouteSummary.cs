using Orckestra.Overture.Security;
using Orckestra.Overture.ServiceModel.Requests.Customers.CustomProfiles;
using ServiceStack;
using System.Runtime.Serialization;

namespace Orckestra.Overture.ServiceModel.Requests
{
    [Api(CustomProfilesDocumentation.Api)]
    [Authorization(Authorization.Modules.Orders, Authorization.Roles.Profiles.Editor)]
    [Route("/customProfiles/{ScopeId}/{EntityId}/otheraddresses",
        Verbs = "POST",
        Notes = "Add an address to a custom profiles's addresses.",
        Summary = "This is a wrong summary")]
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
    [Authorization(Authorization.Modules.Profiles, Authorization.Roles.Profiles.Editor)]
    [Route("/customProfiles/{ScopeId}/{EntityId}/otheraddresses",
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

    [Api(CustomProfilesDocumentation.Api)]
    [Authorization(Authorization.Modules.Profiles, Authorization.Roles.Profiles.Editor)]
    [Route("/customProfiles/{ScopeId}/{EntityId}/otheraddresses",
        Verbs = "POST",
        Notes = "Add an address to a custom profiles's addresses.",
        Summary = RouteDetail)]
    [DataContract]
    public class AddYetAnotherAddressToCustomProfileRequest : ScopedRequest, IReturn<Address>
    {
        public const string RouteDetail = "Add an address to a custom profile's addresses - {AddYetAnotherAddressToCustomProfileRequest}";

        /// <summary>
        /// Initializes a new instance of the <see cref="AddAddressToCustomProfileRequest"/> class.
        /// </summary>
		public AddYetAnotherAddressToCustomProfileRequest()
        {
        }
    }

    [Api(CustomProfilesDocumentation.Api)]
    [Authorization(Authorization.Modules.Profiles, Authorization.Roles.Profiles.Editor)]
    [Route("/customProfiles/{ScopeId}/{EntityId}/otheraddresses",
        Verbs = "POST",
        Notes = "Add an address to a custom profiles's addresses.",
        Summary = "Add an address to a custom profile's addresses - {" + nameof(AddYetYetAnotherAddressToCustomProfileRequest) + "}")]
    [DataContract]
    public class AddYetYetAnotherAddressToCustomProfileRequest : ScopedRequest, IReturn<Address>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AddAddressToCustomProfileRequest"/> class.
        /// </summary>
		public AddYetYetAnotherAddressToCustomProfileRequest()
        {
        }
    }

    [Api(CustomProfilesDocumentation.Api)]
    [Authorization(Authorization.Modules.Profiles, Authorization.Roles.Profiles.Editor)]
    [Route("/customProfiles/{ScopeId}/{EntityId}/otheraddresses",
        Verbs = "POST",
        Notes = "Add an address to a custom profiles's addresses.",
        Summary = "Add an address to a custom profile's addresses - {" + nameof(AddAddressToCustomProfileRequest) + "}")]
    [DataContract]
    public class AddYetYetYetAnotherAddressToCustomProfileRequest : ScopedRequest, IReturn<Address>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AddAddressToCustomProfileRequest"/> class.
        /// </summary>
		public AddYetYetYetAnotherAddressToCustomProfileRequest()
        {
        }
    }
}

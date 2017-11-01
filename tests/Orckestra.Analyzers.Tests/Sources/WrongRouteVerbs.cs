using Orckestra.Overture.Security;
using Orckestra.Overture.ServiceModel.Requests.Customers.CustomProfiles;
using ServiceStack;
using System.Runtime.Serialization;

namespace Orckestra.Overture.ServiceModel.Requests
{
    [Api(CustomProfilesDocumentation.Api)]
    [Authorization(Authorization.Modules.Orders, Authorization.Roles.Profiles.Editor)]
    [Route("/customProfiles/{ScopeId}/{EntityId}/otheraddresses",
        Verbs = "PATATE",
        Notes = "Add an address to a custom profiles's addresses.",
        Summary = "This is a valid summary - {AddAddressToCustomProfileRequest}")]
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
    [Authorization(Authorization.Modules.Orders, Authorization.Roles.Profiles.Editor)]
    [Route("/customProfiles/{ScopeId}/{EntityId}/sameaddresses", "POULET",
        Notes = "Add an address to a custom profiles's addresses.",
        Summary = "This is a valid summary - {AddSameAddressToCustomProfileRequest}")]
    [DataContract]
    public class AddSameAddressToCustomProfileRequest : ScopedRequest, IReturn<Address>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AddSameAddressToCustomProfileRequest"/> class.
        /// </summary>
		public AddSameAddressToCustomProfileRequest()
        {
        }
    }

    [Api(CustomProfilesDocumentation.Api)]
    [Authorization(Authorization.Modules.Orders, Authorization.Roles.Profiles.Editor)]
    [Route("/customProfiles/{ScopeId}/{EntityId}/sameagainaddresses", "POST",
        Notes = "Add an address to a custom profiles's addresses.",
        Summary = "This is a valid summary - {AddSameAgainAddressToCustomProfileRequest}")]
    [DataContract]
    public class AddSameAgainAddressToCustomProfileRequest : ScopedRequest, IReturn<Address>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AddSameAgainAddressToCustomProfileRequest"/> class.
        /// </summary>
		public AddSameAgainAddressToCustomProfileRequest()
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
        Verbs = "GET",
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
        Verbs = "PUT",
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
        Verbs = "DELETE",
        Notes = "Add an address to a custom profiles's addresses.",
        Summary = "Add an address to a custom profile's addresses - {" + nameof(AddYetYetYetAnotherAddressToCustomProfileRequest) + "}")]
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

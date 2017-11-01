using Orckestra.Overture;
using Orckestra.Overture.Messages;
using Orckestra.Overture.Messaging;
using Orckestra.Overture.Security;
using Orckestra.Overture.ServiceModel.Requests.Customers.CustomProfiles;
using ServiceStack;
using System.Runtime.Serialization;

[assembly: DomainRequest(AttributeTargetTypes = "Orckestra.Overture.Domain.*")]
[assembly: DomainRequest(AttributeTargetTypes = @"regex:Orckestra\.Overture\.P(atate|oulet)\..+")]

namespace Orckestra.Overture.ServiceModel.Requests
{
    [Api(CustomProfilesDocumentation.Api)]
    [Authorization(Authorization.Modules.Orders, Authorization.Roles.Profiles.Editor)]
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
    [DataContract]
    public class AddAddressToCustomProfileCommand : IReturn<Address>, ICommand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AddAddressToCustomProfileCommand"/> class.
        /// </summary>
		public AddAddressToCustomProfileCommand()
        {
        }
    }

    [Api(CustomProfilesDocumentation.Api)]
    [Authorization(Authorization.Modules.Orders, Authorization.Roles.Profiles.Editor)]
    [DataContract]
    public class AddAddressToCustomProfile2Command : Command, IReturn<Address>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AddAddressToCustomProfile2Command"/> class.
        /// </summary>
		public AddAddressToCustomProfile2Command()
        {
        }
    }

    [Api(CustomProfilesDocumentation.Api)]
    [Authorization(Authorization.Modules.Orders, Authorization.Roles.Profiles.Editor)]
    [DataContract]
    public class NotARequest //Not implementing IReturn
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NotARequest"/> class.
        /// </summary>
		public NotARequest()
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
    [Authorization(Authorization.Modules.Orders, Authorization.Roles.Profiles.Editor)]
    [DataContract]
    [DomainRequest]
    public class AddAddressToCustomProfileDomainRequest : IReturn<Address>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AddAddressToCustomProfileDomainRequest"/> class.
        /// </summary>
		public AddAddressToCustomProfileDomainRequest()
        {
        }
    }

    [DataContract]
    public enum GetShipmentFulfillmentInfosDateRangeFilter
    {
        [EnumMember]
        FulfillmentBeginDate = 0,

        [EnumMember]
        OrderCreatedDate
    }
}

//The following requests will be excluded by the DomainRequestAttribute.
namespace Orckestra.Overture.Domain
{
    [DataContract]
    public class CustomDomainRequest : IReturn<int>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CustomDomainRequest"/> class.
        /// </summary>
        public CustomDomainRequest()
        {
        }
    }
}

namespace Orckestra.Overture
{
    namespace Poulet
    {
        [DataContract]
        public class CustomDomainRequest : IReturn<int>
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="CustomDomainRequest"/> class.
            /// </summary>
            public CustomDomainRequest()
            {
            }
        }
    }

    namespace Patate
    {
        [DataContract]
        public class CustomDomainRequest : IReturn<int>
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="CustomDomainRequest"/> class.
            /// </summary>
            public CustomDomainRequest()
            {
            }
        }
    }
}
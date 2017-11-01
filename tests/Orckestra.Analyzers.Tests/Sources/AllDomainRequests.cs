using Orckestra.Overture;
using Orckestra.Overture.Messages;
using Orckestra.Overture.Messaging;
using Orckestra.Overture.ServiceModel.Requests;
using ServiceStack;
using System.Runtime.Serialization;

[assembly: DomainRequest(AttributeTargetTypes = null)]

namespace Orckestra.Overture.ServiceModel.Requests2
{
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
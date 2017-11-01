using Orckestra.Overture.Security;
using Orckestra.Overture.ServiceModel.Requests.Customers.CustomProfiles;
using ServiceStack;
using System.Runtime.Serialization;

namespace Orckestra.Overture.ServiceModel.Requests
{
    [Api(CustomProfilesDocumentation.Api)]
    [Authorization(Authorization.Modules.Orders, Authorization.Roles.Profiles.Editor)]
    [Route("/test/route",
        Notes = "Small note",
        Summary = "Test - {AddAddressToCustomProfileRequest}", Verbs = "")]
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
    [Route("/test/route",
        Verbs = "POST",
        Notes = "Small note",
        Summary = "Test - {AddAddressToCustomProfileRequest2}")]
    [DataContract]
    public class AddAddressToCustomProfileRequest2 : ScopedRequest, IReturn<Address>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AddAddressToCustomProfileRequest2"/> class.
        /// </summary>
		public AddAddressToCustomProfileRequest2()
        {
        }
    }

    [Api(CustomProfilesDocumentation.Api)]
    [Authorization(Authorization.Modules.Orders, Authorization.Roles.Profiles.Editor)]
    [Route("/test/route", "POST",
        Notes = "Small note",
        Summary = "Test - {AddAddressToCustomProfileRequest3}")]
    [DataContract]
    public class AddAddressToCustomProfileRequest3 : ScopedRequest, IReturn<Address>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AddAddressToCustomProfileRequest3"/> class.
        /// </summary>
		public AddAddressToCustomProfileRequest3()
        {
        }
    }
}

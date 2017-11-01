using Orckestra.Overture.ServiceModel.Requests.Customers.CustomProfiles;
using ServiceStack;
using System.Runtime.Serialization;

namespace Orckestra.Overture.ServiceModel.Requests
{
    [Api(CustomProfilesDocumentation.Api)]
    [Security.Authorization(Security.Authorization.Modules.Profiles, Security.Authorization.Roles.Profiles.Editor)]
    [Route("/customProfiles/{ScopeId}/{EntityId}/sample",
        Verbs = "POST",
        Notes = "Add an address to a custom profiles's addresses.",
        Summary = "Sample - {SampleRequest}")]
    [System.Runtime.Serialization.DataContract]
    public class SampleRequest : IReturn<string>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SampleRequest"/> class.
        /// </summary>
		public SampleRequest()
        {
        }

        public SomeOtherEnum Param { get; set; }

        public SomeOtherEnumAgain Param2 { get; set; }
    }

    public enum SomeEnum
    {
        SomeValue
    }

    [DataContract]
    public enum SomeOtherEnum
    {
        SomeValue
    }

    [System.Runtime.Serialization.DataContract]
    public enum SomeOtherEnumAgain
    {
        SomeValue
    }
}

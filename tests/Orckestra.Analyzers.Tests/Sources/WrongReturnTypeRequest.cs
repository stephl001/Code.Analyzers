using Orckestra.Overture.Security;
using Orckestra.Overture.ServiceModel.Requests.Customers.CustomProfiles;
using ServiceStack;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Orckestra.Overture.ServiceModel.Requests
{
    [Api(CustomProfilesDocumentation.Api)]
    [Route("/customProfiles/{ScopeId}/{EntityId}/otheraddresses",
        Verbs = "POST",
        Notes = "Add an address to a custom profiles's addresses.", Summary = "<Add summary here> - {AddAddressToCustomProfileRequest}")]
    [Authorization(Authorization.Modules.Orders, Authorization.Roles.Profiles.Editor)]
    [DataContract]
    public class AddAddressToCustomProfileRequest : ScopedRequest, IReturn<Address>
    {
        public AddAddressToCustomProfileRequest()
        {
        }
    }

    [Api(CustomProfilesDocumentation.Api)]
    [Route("/customProfiles/{ScopeId}/{EntityId}/otheraddresses2",
        Verbs = "POST",
        Notes = "Add an address to a custom profiles's addresses.", Summary = "<Add summary here> - {AddAddressToCustomProfileRequest2}")]
    [Authorization(Authorization.Modules.Orders, Authorization.Roles.Profiles.Editor)]
    [DataContract]
    public class AddAddressToCustomProfileRequest2 : ScopedRequest, IReturn<IFormatter>
    {
        public AddAddressToCustomProfileRequest2()
        {
        }
    }

    [Api(CustomProfilesDocumentation.Api)]
    [Route("/customProfiles/{ScopeId}/{EntityId}/otheraddresses3",
        Verbs = "POST",
        Notes = "Add an address to a custom profiles's addresses.", Summary = "<Add summary here> - {AddAddressToCustomProfileRequest3}")]
    [Authorization(Authorization.Modules.Orders, Authorization.Roles.Profiles.Editor)]
    [DataContract]
    public class AddAddressToCustomProfileRequest3 : ScopedRequest, IReturn<object>
    {
        public AddAddressToCustomProfileRequest3()
        {
        }
    }

    [Api(CustomProfilesDocumentation.Api)]
    [Route("/customProfiles/{ScopeId}/{EntityId}/otheraddresses4",
        Verbs = "POST",
        Notes = "Add an address to a custom profiles's addresses.", Summary = "<Add summary here> - {AddAddressToCustomProfileRequest4}")]
    [Authorization(Authorization.Modules.Orders, Authorization.Roles.Profiles.Editor)]
    [DataContract]
    public class AddAddressToCustomProfileRequest4 : ScopedRequest, IReturn<object>
    {
        public AddAddressToCustomProfileRequest4()
        {
        }
    }

    [Api(CustomProfilesDocumentation.Api)]
    [Route("/customProfiles/{ScopeId}/{EntityId}/otheraddresses5",
        Verbs = "POST",
        Notes = "Add an address to a custom profiles's addresses.", Summary = "<Add summary here> - {AddAddressToCustomProfileRequest5}")]
    [Authorization(Authorization.Modules.Orders, Authorization.Roles.Profiles.Editor)]
    [DataContract]
    public class AddAddressToCustomProfileRequest5 : ScopedRequest, IReturn<int>
    {
        public AddAddressToCustomProfileRequest5()
        {
        }
    }

    [Api(CustomProfilesDocumentation.Api)]
    [Route("/customProfiles/{ScopeId}/{EntityId}/otheraddresses6",
        Verbs = "POST",
        Notes = "Add an address to a custom profiles's addresses.", Summary = "<Add summary here> - {AddAddressToCustomProfileRequest6}")]
    [Authorization(Authorization.Modules.Orders, Authorization.Roles.Profiles.Editor)]
    [DataContract]
    public class AddAddressToCustomProfileRequest6 : ScopedRequest, IReturn
    {
        public AddAddressToCustomProfileRequest6()
        {
        }
    }

    [Api(CustomProfilesDocumentation.Api)]
    [Route("/customProfiles/{ScopeId}/{EntityId}/otheraddresses7",
        Verbs = "POST",
        Notes = "Add an address to a custom profiles's addresses.", Summary = "<Add summary here> - {AddAddressToCustomProfileRequest7}")]
    [Authorization(Authorization.Modules.Orders, Authorization.Roles.Profiles.Editor)]
    [DataContract]
    public class AddAddressToCustomProfileRequest7 : ScopedRequest, IReturn<IEnumerable<int>>
    {
        public AddAddressToCustomProfileRequest7()
        {
        }
    }

    [Api(CustomProfilesDocumentation.Api)]
    [Route("/customProfiles/{ScopeId}/{EntityId}/otheraddresses8",
        Verbs = "POST",
        Notes = "Add an address to a custom profiles's addresses.", Summary = "<Add summary here> - {AddAddressToCustomProfileRequest8}")]
    [Authorization(Authorization.Modules.Orders, Authorization.Roles.Profiles.Editor)]
    [DataContract]
    public class AddAddressToCustomProfileRequest8 : ScopedRequest, IReturn<string>
    {
        public AddAddressToCustomProfileRequest8()
        {
        }
    }
}

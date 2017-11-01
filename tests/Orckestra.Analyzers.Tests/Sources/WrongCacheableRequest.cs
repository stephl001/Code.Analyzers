using Orckestra.Overture.Caching;
using Orckestra.Overture.Security;
using Orckestra.Overture.ServiceModel.Requests.Customers.CustomProfiles;
using ServiceStack;
using System;
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

        public DateTime WhateverTime { get; set; }
    }

    [Api(CustomProfilesDocumentation.Api)]
    [Route("/customProfiles/{ScopeId}/{EntityId}/otheraddresses2",
        Verbs = "POST",
        Notes = "Add an address to a custom profiles's addresses.", Summary = "<Add summary here> - {AddAddressToCustomProfileRequest2}")]
    [Authorization(Authorization.Modules.Orders, Authorization.Roles.Profiles.Editor)]
    [Cacheable]
    [DataContract]
    public class AddAddressToCustomProfileRequest2 : ScopedRequest, IReturn<IFormatter>
    {
        public AddAddressToCustomProfileRequest2()
        {
        }

        public DateTime WhateverTime { get; set; }
    }

    public abstract class BaseRequest
    {
        public DateTime WhateverTime { get; set; }
    }

    [Api(CustomProfilesDocumentation.Api)]
    [Route("/customProfiles/{ScopeId}/{EntityId}/otheraddresses3",
        Verbs = "POST",
        Notes = "Add an address to a custom profiles's addresses.", Summary = "<Add summary here> - {AddAddressToCustomProfileRequest3}")]
    [Authorization(Authorization.Modules.Orders, Authorization.Roles.Profiles.Editor)]
    [Cacheable]
    [DataContract]
    public class AddAddressToCustomProfileRequest3 : BaseRequest, IReturn<IFormatter>
    {
        public string WhateverString { get; set; }
    }
}

using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace Orckestra.Overture.ServiceModel.Requests
{
    [DataContract]
    [ExcludeFromCodeCoverage]
    public abstract class ScopedRequest
    {
        protected ScopedRequest()
        { }

        [DataMember]
        [DefaultValue("Global")]
        public string ScopeId { get; set; }
    }
}
using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace Orckestra.Overture.ServiceModel
{
    [DataContract]
    [ExcludeFromCodeCoverage]
    public class Address
    {
        [DataMember]
        public Guid Id { get; set; }

    }
}

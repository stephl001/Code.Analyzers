//To properly compile this file, you must add a reference to Orckestra.ServiceFoundation
using ServiceStack;
using System.Runtime.Serialization;

namespace MyNamespace
{
    [Api("MyCustomApi")]
    [RouteAttribute("/api/custom",
        Verbs = "POST",
        Notes = "This is a ServiceFoundation request.",
        Summary = "Custom request - {MyCustomRequest}")]
    [DataContract]
    public class MyCustomRequest : IReturn<string>
    {
        public MyCustomRequest()
        {
        }
    }

    [Api("MyCustomApi")]
    [Orckestra.ServiceFoundation.Security.Authorization("MyModule", "MyRole")]
    [ServiceStack.Route("/api/custom/other",
        Verbs = "POST",
        Notes = "This is another ServiceFoundation request.",
        Summary = "Other custom request - {MyOtherCustomRequest}")]
    [DataContract]
    public class MyOtherCustomRequest : IReturn<string>
    {
        public MyOtherCustomRequest()
        {
        }
    }

    [DataContract]
    public enum DummyEnum
    {
        Test
    }
}

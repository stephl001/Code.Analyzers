using Orckestra.Overture.Routing;
using Orckestra.Overture.Security;
using ServiceStack;
using System.Runtime.Serialization;

namespace Orckestra.Overture.ServiceModel.Requests
{
    /// <summary> [Preview] {Add your comment here} </summary>
    [Route("/whatever",
        Verbs = "GET",
        Notes = "Add an address to a custom profiles's addresses.",
        Summary = "Whatever request - {GetWhateverRequest}")]
    [DataContract]
    [Authorization(Authorization.Modules.Any, Authorization.Roles.None)]
    [Preview]
    public class GetWhateverRequest : IReturn<string>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetWhateverRequest"/> class.
        /// </summary>
		public GetWhateverRequest()
        {
        }
    }

    /// <summary>
    /// [Preview] This is a comment
    /// </summary>
    [Route("/whatever",
        Verbs = "GET",
        Notes = "Add an address to a custom profiles's addresses.",
        Summary = "Whatever request - {GetWhatever2Request}")]
    [DataContract]
    [Authorization(Authorization.Modules.Any, Authorization.Roles.None)]
    [Preview]
    public class GetWhatever2Request : IReturn<string>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetWhatever2Request"/> class.
        /// </summary>
		public GetWhatever2Request()
        {
        }
    }

    /// <summary>
    /// [Preview] This is a comment
    /// </summary>
    /// <remarks>This should stay.</remarks>
    [Route("/whatever",
        Verbs = "GET",
        Notes = "Add an address to a custom profiles's addresses.",
        Summary = "Whatever request - {GetWhatever3Request}")]
    [DataContract]
    [Authorization(Authorization.Modules.Any, Authorization.Roles.None)]
    [Preview]
    public class GetWhatever3Request : IReturn<string>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetWhatever3Request"/> class.
        /// </summary>
		public GetWhatever3Request()
        {
        }
    }

    /** 
     * <summary>
     * [Preview] This is a comment
     * </summary>
     * <remarks>
     * This should stay.
     * </remarks>
    **/
    [Route("/whatever",
        Verbs = "GET",
        Notes = "Add an address to a custom profiles's addresses.",
        Summary = "Whatever request - {GetWhatever4Request}")]
    [DataContract]
    [Authorization(Authorization.Modules.Any, Authorization.Roles.None)]
    [Preview]
    public class GetWhatever4Request : IReturn<string>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetWhatever4Request"/> class.
        /// </summary>
		public GetWhatever4Request()
        {
        }
    }

    /** 
     * <summary>
     * [Preview] This is a multi-line comment
     * </summary>
     * <remarks>This should stay.</remarks>
     **/
    [Route("/whatever",
        Verbs = "GET",
        Notes = "Add an address to a custom profiles's addresses.",
        Summary = "Whatever request - {GetWhatever5Request}")]
    [DataContract]
    [Authorization(Authorization.Modules.Any, Authorization.Roles.None)]
    [Preview]
    public class GetWhatever5Request : IReturn<string>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetWhatever5Request"/> class.
        /// </summary>
		public GetWhatever5Request()
        {
        }
    }
}

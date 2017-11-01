using ServiceStack;
using Orckestra.Overture.Server;

namespace Orckestra.Overture.Foundation
{
    /// <summary>
    /// Base class for all service implementations. Provides service implementation with common functionalities, 
    /// such as authorization, invocation policy and tracing.
    /// </summary>
	public abstract class ServiceBase : IHandler, IService
    {

    }
}
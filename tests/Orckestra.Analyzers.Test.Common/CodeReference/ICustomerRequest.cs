using System;

namespace Orckestra.Overture.Routing
{
    public interface ICustomerRequest
    {
        Guid CustomerId { get; set; }
    }
}
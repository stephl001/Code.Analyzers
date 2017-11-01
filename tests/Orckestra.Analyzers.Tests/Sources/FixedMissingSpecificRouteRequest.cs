using System;
using System.ComponentModel;
using ServiceStack;
using System.Runtime.Serialization;
using Orckestra.Overture.Security;
using Orckestra.Overture.Routing;

namespace Orckestra.Overture.ServiceModel.Requests.Customers
{
    /// <summary>
    /// Adds a new payment profile for a specified customer.
    /// </summary>
    [Authorization(Authorization.Modules.Customer, Authorization.Roles.Customer.Editor)]
    [SpecificRoute(ApiType.Customer, "/customers/{ScopeId}/{PaymentProviderName}/paymentProfile",
        Verbs = "POST",
        Notes = "Add customer payment profile related to a specific payment provider.",
        Summary = "Add customer payment profile related to a specific payment provider - {AddCustomerPaymentProfileRequest}")]
    [Route("/customers/{ScopeId}/{CustomerId}/{PaymentProviderName}/paymentProfile",
        Verbs = "POST",
        Notes = "Add customer payment profile related to a specific payment provider.",
        Summary = "Add customer payment profile related to a specific payment provider - {AddCustomerPaymentProfileRequest}")]
    [DataContract]
    public class AddCustomerPaymentProfileRequest : ScopedRequest, Routing.ICustomerRequest, IReturn<string>
    {
        /// <summary>
        /// Gets or sets the unique identifier for this payment profile.
        /// </summary>
        /// <remarks>
        /// This identifier will be generated if not specified.
        /// </remarks>
        [DataMember]
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier of the <see cref="Customer"/>.
        /// </summary>
        [DataMember]
        public Guid CustomerId { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier of the provider related to the payment profile.
        /// </summary>
        [DataMember]
        public string PaymentProviderName { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier returned by external provider related to the payment profile.
        /// </summary>
        [DataMember]
        public string ExternalIds { get; set; }

        /// <summary>
        /// Gets or sets the ordr location id.
        /// </summary>
        [DataMember]
        public string OrderLocationId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to overwrite the way pyment profile are added.
        /// </summary>
        [DataMember]
        [DefaultValue(false)]
        public bool AllowMultiplePaymentProfileOnProvider { get; set; }
    }

    /// <summary>
    /// Adds a new payment profile for a specified customer.
    /// </summary>
    [Authorization(Authorization.Modules.Customer, Authorization.Roles.Customer.Editor)]
    [SpecificRoute(ApiType.Customer, "/customers/{ScopeId}/{PaymentProviderName}/paymentProfile2",
        Verbs = "POST",
        Notes = "Add customer payment profile related to a specific payment provider.",
        Summary = "Add customer payment profile related to a specific payment provider - {AddCustomerPaymentProfile2Request}")]
    [RouteAttribute("/customers/{ScopeId}/{CustomerId}/{PaymentProviderName}/paymentProfile2",
        Verbs = "POST",
        Notes = "Add customer payment profile related to a specific payment provider.",
        Summary = "Add customer payment profile related to a specific payment provider - {AddCustomerPaymentProfile2Request}")]
    [DataContract]
    public class AddCustomerPaymentProfile2Request : ScopedRequest, Routing.ICustomerRequest, IReturn<string>
    {
        /// <summary>
        /// Gets or sets the unique identifier for this payment profile.
        /// </summary>
        /// <remarks>
        /// This identifier will be generated if not specified.
        /// </remarks>
        [DataMember]
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier of the <see cref="Customer"/>.
        /// </summary>
        [DataMember]
        public Guid CustomerId { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier of the provider related to the payment profile.
        /// </summary>
        [DataMember]
        public string PaymentProviderName { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier returned by external provider related to the payment profile.
        /// </summary>
        [DataMember]
        public string ExternalIds { get; set; }

        /// <summary>
        /// Gets or sets the ordr location id.
        /// </summary>
        [DataMember]
        public string OrderLocationId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to overwrite the way pyment profile are added.
        /// </summary>
        [DataMember]
        [DefaultValue(false)]
        public bool AllowMultiplePaymentProfileOnProvider { get; set; }
    }

    /// <summary>
    /// Adds a new payment profile for a specified customer.
    /// </summary>
    [Authorization(Authorization.Modules.Customer, Authorization.Roles.Customer.Editor)]
    [SpecificRoute(ApiType.Customer, "/customers/{ScopeId}/{PaymentProviderName}/paymentProfile3",
        Verbs = "POST",
        Notes = "Add customer payment profile related to a specific payment provider.",
        Summary = "Add customer payment profile related to a specific payment provider - {AddCustomerPaymentProfile3Request}")]
    [ServiceStack.Route("/customers/{ScopeId}/{CustomerId}/{PaymentProviderName}/paymentProfile3",
        Verbs = "POST",
        Notes = "Add customer payment profile related to a specific payment provider.",
        Summary = "Add customer payment profile related to a specific payment provider - {AddCustomerPaymentProfile3Request}")]
    [DataContract]
    public class AddCustomerPaymentProfile3Request : ScopedRequest, Routing.ICustomerRequest, IReturn<string>
    {
        /// <summary>
        /// Gets or sets the unique identifier for this payment profile.
        /// </summary>
        /// <remarks>
        /// This identifier will be generated if not specified.
        /// </remarks>
        [DataMember]
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier of the <see cref="Customer"/>.
        /// </summary>
        [DataMember]
        public Guid CustomerId { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier of the provider related to the payment profile.
        /// </summary>
        [DataMember]
        public string PaymentProviderName { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier returned by external provider related to the payment profile.
        /// </summary>
        [DataMember]
        public string ExternalIds { get; set; }

        /// <summary>
        /// Gets or sets the ordr location id.
        /// </summary>
        [DataMember]
        public string OrderLocationId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to overwrite the way pyment profile are added.
        /// </summary>
        [DataMember]
        [DefaultValue(false)]
        public bool AllowMultiplePaymentProfileOnProvider { get; set; }
    }
}
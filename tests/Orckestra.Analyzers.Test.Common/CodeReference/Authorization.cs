using System.Diagnostics.CodeAnalysis;

namespace Orckestra.Overture.Security
{
    //
    // Summary:
    //     Defines built-in constants for authorization modules, scopes and roles.
    [ExcludeFromCodeCoverage]
    public static class Authorization
    {
        //
        // Summary:
        //     Defines built-in constants for authorization modules.
        public static class Modules
        {
            //
            // Summary:
            //     Use this constant to allow role from any module to execute a request.
            public const string Any = "*";
            //
            // Summary:
            //     The customer module.
            public const string Customer = "Customer";
            //
            // Summary:
            //     The digital asset management module.
            public const string DAM = "DAM";
            //
            // Summary:
            //     The inventory module.
            public const string Inventories = "Inventories";
            //
            // Summary:
            //     The marketing module.
            public const string Marketing = "Marketing";
            //
            // Summary:
            //     The orchestrator administration UI module.
            public const string Orchestrator = "Orchestrator";
            //
            // Summary:
            //     The order management module.
            public const string Orders = "Orders";
            //
            // Summary:
            //     The price management module.
            public const string PriceManagement = "PriceManagement";
            //
            // Summary:
            //     The product module.
            public const string Products = "Products";
            //
            // Summary:
            //     The profiles module (previously Customers).
            public const string Profiles = "Profiles";
            //
            // Summary:
            //     The report module.
            public const string Report = "Report";
            //
            // Summary:
            //     The cross-system search module.
            public const string Search = "Search";
            //
            // Summary:
            //     The shopping cart management module.
            public const string Shopping = "Shopping";
            //
            // Summary:
            //     The foundation system cross-cutting module. Foundation data management including
            //     scopes, countries, supported languages,etc...
            public const string System = "System";
            //
            // Summary:
            //     The orchestration user management module.
            public const string UserManagement = "UserManagement";
        }
        //
        // Summary:
        //     Defines built-in constants for roles
        public static class Roles
        {
            //
            // Summary:
            //     The "administrator" role for all possible rights.
            public const string Administrator = "Administrator";
            //
            // Summary:
            //     The "editor" role for updating data through a management application.
            public const string Editor = "Editor";
            //
            // Summary:
            //     The "FullTrust" role to grant all possible rights without the need to specify
            //     a role.
            public const string FullTrust = "FullTrust";
            //
            // Summary:
            //     The GlobalAdministrator role to grant.
            //
            // Remarks:
            //     This role is not currently used in any service request. Use FullTrust instead
            //     and specify a module to which it applies.
            public const string GlobalAdministrator = "GlobalAdministrator";
            //
            // Summary:
            //     Specifies absence of role, implying that no specific role is required
            public const string None = "*";
            //
            // Summary:
            //     The "reader" role for reading and querying data.
            public const string Reader = "Reader";

            //
            // Summary:
            //     Defines roles for the report module.
            public static class Customer
            {
                //
                // Summary:
                //     The "editor" role for editing customer related data.
                public const string Editor = "Editor";
                //
                // Summary:
                //     The "reader" role for querying customer related data.
                public const string Reader = "Reader";
            }
            //
            // Summary:
            //     Defines built-in constants for roles applicable to the DAM module.
            //
            // Remarks:
            //     Theses permissions are not currently used in any existing service requests.
            public static class Dam
            {
                //
                // Summary:
                //     The "administrator" role for full-control of the DAM module.
                public const string Administrator = "Administrator";
                //
                // Summary:
                //     The "editor" role for editing assets and their metadata through a management
                //     application.
                public const string Editor = "Editor";
                //
                // Summary:
                //     The "publisher" role for publishing or rejecting assets through a management
                //     application.
                public const string Publisher = "Publisher";
                //
                // Summary:
                //     The "reader" role for querying assets.
                public const string Reader = "Reader";
            }
            //
            // Summary:
            //     Defines roles for the inventory module.
            public static class Inventory
            {
                //
                // Summary:
                //     The "administrator" role for full-control of the inventory-related data.
                public const string Administrator = "Administrator";
                //
                // Summary:
                //     The "editor" role for updating inventory-related data through a management application.
                public const string Editor = "Editor";
                //
                // Summary:
                //     The "integrator" role for integration operation on the inventory module such
                //     as import/export.
                public const string Integrator = "Integrator";
                //
                // Summary:
                //     The "reader" role for querying inventory-related data.
                public const string Reader = "Reader";
            }
            //
            // Summary:
            //     Defines roles for the customer module.
            public static class Marketing
            {
                //
                // Summary:
                //     The "administrator" role for full-control of the marketing module.
                public const string Administrator = "Administrator";
                //
                // Summary:
                //     Campaign manager role managing campaign-related data through a management application.
                public const string CampaignManager = "CampaignManager";
                //
                // Summary:
                //     The "campaign coordinator" role for editing campaign-related data through a management
                //     application. A campaign coordinator can not publish, reject or pause a campaign.
                public const string CampaingCoordonator = "CampaignCoordinator";
                //
                // Summary:
                //     The "editor" role for editing marketing-related data through a management application.
                public const string Editor = "Editor";
                //
                // Summary:
                //     Integrator role allows to use the integration services of the marketing module
                //     for operations such as importing campaigns and promotions.
                public const string Integrator = "Integrator";
                //
                // Summary:
                //     The "reader" role for querying marketing-related data.
                public const string Reader = "Reader";
            }
            //
            // Summary:
            //     Defines roles for the Orchestrator module.
            public static class Orchestrator
            {
                //
                // Summary:
                //     The required role for managing centralized customers membership.
                public const string ManageMembership = "ManageMembership";
                //
                // Summary:
                //     The required role for reading configuration settings using the configuration
                //     service.
                public const string ReadConfiguration = "ReadConfiguration";
                //
                // Summary:
                //     The required role to be considered and Orchestrator user with the rights to view
                //     and edit dashboard personal settings.
                public const string User = "User";
                //
                // Summary:
                //     The required role for writing configuration settings using the configuration
                //     service.
                public const string WriteConfiguration = "WriteConfiguration";
                //
                // Summary:
                //     The required role for writing to a log using the Overture logging service.
                public const string WriteToLog = "WriteToLog";
            }
            //
            // Summary:
            //     Defines roles for the order module.
            public static class Order
            {
                //
                // Summary:
                //     The "administrator" role for full-control of the order module.
                public const string Administrator = "Administrator";
                //
                // Summary:
                //     The "editor" role for editing submitted orders through a management application.
                public const string Editor = "Editor";
                //
                // Summary:
                //     The "integrator" role for integration operation on the order module such as saving
                //     an order without processing, locking an order, etc...
                public const string Integrator = "Integrator";
                //
                // Summary:
                //     The "picker" role for picking orders.
                public const string Picker = "Picker";
                //
                // Summary:
                //     The "reader" role for querying submitted orders.
                public const string Reader = "Reader";
            }
            //
            // Summary:
            //     Defines roles for the product module.
            public static class PriceManagement
            {
                //
                // Summary:
                //     The "administrator" role for full-control of the product module.
                public const string Administrator = "Administrator";
                //
                // Summary:
                //     The "editor" role for updating price list data through a management application.
                public const string PriceListEditor = "PriceListEditor";
                //
                // Summary:
                //     The "reader" role for querying price list data through a management application.
                public const string PriceListReader = "PriceListReader";
                //
                // Summary:
                //     The "editor" role for updating product-related data through a management application.
                public const string PriceUpdateEditor = "PriceUpdateEditor";
            }
            //
            // Summary:
            //     Defines roles for the product module.
            public static class Product
            {
                //
                // Summary:
                //     The "administrator" role for full-control of the product module.
                public const string Administrator = "Administrator";
                //
                // Summary:
                //     The "editor" role for updating product categories through a management application.
                public const string CategoryEditor = "CategoryEditor";
                //
                // Summary:
                //     The "editor" role for updating product-related data through a management application.
                public const string Editor = "Editor";
                //
                // Summary:
                //     The "publisher" role for publishing or rejecting products through a management
                //     application.
                public const string Publisher = "Publisher";
                //
                // Summary:
                //     The "reader" role for querying product-related data.
                public const string Reader = "Reader";
            }
            //
            // Summary:
            //     Defines roles for the profiles module.
            public static class Profiles
            {
                //
                // Summary:
                //     The "administrator" role for full-control in the profiles module.
                public const string Administrator = "Administrator";
                //
                // Summary:
                //     The "editor" role for editing profiles through a management application.
                public const string Editor = "Editor";
                //
                // Summary:
                //     The "reader" role for querying profiles.
                public const string Reader = "Reader";
            }
            //
            // Summary:
            //     Defines roles for the report module.
            public static class Report
            {
                //
                // Summary:
                //     The "administrator" role for full-control of the Report module.
                public const string Administrator = "Administrator";
                //
                // Summary:
                //     The role for accessing marketing related reports.
                public const string MarketingReportsReader = "MarketingReportsReader";
                //
                // Summary:
                //     The role for accessing order related reports.
                public const string OrderReportsReader = "OrderReportsReader";
                //
                // Summary:
                //     The role for accessing products related reports.
                public const string ProductsReportsReader = "ProductsReportsReader";
            }
            //
            // Summary:
            //     Search security roles
            public static class Search
            {
                //
                // Summary:
                //     The "administrator" role for full-control of the search module.
                public const string Administrator = "Administrator";
                //
                // Summary:
                //     The editor role for edition of saved queries.
                public const string Editor = "Editor";
                //
                // Summary:
                //     The reader role for executing search request.
                public const string Reader = "Reader";
            }
            //
            // Summary:
            //     Defines roles for the shopping module.
            public static class Shopping
            {
                //
                // Summary:
                //     The "administrator" role for full-control of the shopping module.
                public const string Administrator = "Administrator";
                //
                // Summary:
                //     The "editor" role for editing live shopping carts through a management application,
                //     through user impersonation.
                public const string Editor = "Editor";
                //
                // Summary:
                //     The "reader" role for querying live shopping carts.
                public const string Reader = "Reader";
            }
            //
            // Summary:
            //     Search security roles
            public static class UserManagementRoles
            {
                //
                // Summary:
                //     The administrator role to be able to perform any user management task.
                public const string Administrator = "Administrator";
                //
                // Summary:
                //     The role that must be assigned to user to allow him to create/update/delete groups.
                public const string GroupEditor = "GroupEditor";
                //
                // Summary:
                //     The group editor role for updating group members.
                public const string GroupPermissionEditor = "GroupPermissionEditor";
                //
                // Summary:
                //     The reader role for querying users and authorizations information.
                public const string Reader = "Reader";
                //
                // Summary:
                //     The role editor role for creating and editing new authorization roles in the
                //     system.
                public const string UserEditor = "UserEditor";
                //
                // Summary:
                //     The use editor role for updating user related information.
                public const string UserPermissionEditor = "UserPermissionEditor";
            }
        }
        //
        // Summary:
        //     Defines built-in constants for authorization scopes.
        public static class Scopes
        {
            //
            // Summary:
            //     The "any" scope, meaning all possible scopes.
            public const string All = "*";
        }
    }
}
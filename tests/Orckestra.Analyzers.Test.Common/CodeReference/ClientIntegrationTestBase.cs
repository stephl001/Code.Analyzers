using System.Diagnostics.CodeAnalysis;

namespace Orckestra.Overture.Testing.Utilities
{
    [ExcludeFromCodeCoverage]
    public abstract class ClientIntegrationTestBase
    {
        protected internal const string AllFormats = "AllFormatsDataSource";
        protected internal const string JSon = "JsonDataSource";
        protected internal const string Xml = "XmlDataSource";
    }
}

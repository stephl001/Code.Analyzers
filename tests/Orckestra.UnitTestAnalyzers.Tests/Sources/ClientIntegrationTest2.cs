using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Orckestra.Analyzers.Tests
{
    public class ClientIntegrationTestBase
    {        
    }

    [TestClass]
    class SomeIntegrationTestClass : ClientIntegrationTestBase
    {
        [TestMethod]
        [TestCategory("Integration")]
        public void ValidTest()
        {
        }
    }
}
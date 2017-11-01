using Microsoft.VisualStudio.TestTools.UnitTesting;
using Orckestra.Overture.Testing.Utilities;

namespace Orckestra.Analyzers.Tests
{
    [TestClass]
    class SomeIntegrationTestClass : ClientIntegrationTestBase
    {
        [TestMethod]
        [TestCategory("Integration")]
        [DataSource(AllFormats)]
        public void InvalidTest()
        {
        }

        [TestMethod]
        [TestCategory("Integration")]
        [DataSource(AllFormats)]
        public void ValidTest1()
        {
        }

        [TestMethod]
        [TestCategory("Integration")]
        [DataSource(JSon)]
        public void ValidTest2()
        {
        }

        [TestMethod]
        [TestCategory("Integration")]
        [DataSource(Xml)]
        public void ValidTest3()
        {
        }
    }
}
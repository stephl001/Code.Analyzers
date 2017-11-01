using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Orckestra.Analyzers.Tests
{
    [TestClass]
    public class SomeTestClass
    {
        [TestMethod]
        [TestCategory("Unit")]
        public void InvalidTest()
        {
        }

        [TestMethod()]
        [TestCategory("Unit")]
        public void InvalidTest2()
        {
        }

        [TestMethod,Ignore]
        public void IgnoredTest()
        {
        }

        [TestMethod]
        [TestCategory("Unit")]
        public void ValidUnitTest()
        {
        }

        [TestMethod]
        [TestCategory("Integration")]
        public void ValidIntegrationTest()
        {
        }

        [TestMethod]
        [TestCategory("Invalid")]
        [TestCategory("Unit")]
        public void InvalidCategoryTest()
        {
        }

        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethod]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCategory("Unit")]
        public void ValidCategoryTest()
        {
        }

        [TestMethod]
        [TestCategory("Unit")]
        public void InvalidUnitIntegrationTest()
        {
        }
    }
}
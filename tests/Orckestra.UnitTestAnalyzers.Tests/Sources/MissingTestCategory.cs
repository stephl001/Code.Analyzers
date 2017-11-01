using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Orckestra.Analyzers.Tests
{
    [TestClass]
    public class SomeTestClass
    {
        [TestMethod]
        public void InvalidTest()
        {
        }

        [TestMethod()]
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
        public void InvalidCategoryTest()
        {
        }

        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethod]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCategory("Unit")]
        public void ValidCategoryTest()
        {
        }

        [TestMethod]
        [TestCategory("Integration")]
        [TestCategory("Unit")]
        public void InvalidUnitIntegrationTest()
        {
        }
    }
}
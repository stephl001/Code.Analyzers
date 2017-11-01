using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace Orckestra.Analyzers.Tests
{
    [TestClass]
    public class SomeTestClass
    {
        [TestMethod]
        [TestCategory("Unit")]
        public async Task InvalidTest()
        {
            await Task.Delay(10).ConfigureAwait(false);
        }

        [TestMethod,Ignore]
        public async void IgnoredTest()
        {
            await Task.Delay(10).ConfigureAwait(false);
        }

        [TestMethod]
        [TestCategory("Unit")]
        [TestCategory("Async")]
        public async Task ValidTest()
        {
            await Task.Delay(10).ConfigureAwait(false);
        }        
    }
}
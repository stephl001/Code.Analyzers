using System.Threading.Tasks;

namespace Orckestra.Analyzers.Tests.Sources
{
    public class ConfigureAwaitTest
    {
        public async Task<int> GetValueAsync()
        {
            await Task.Delay(12);
            return 10;
        }

        public async Task DoWork()
        {
            await Task.Delay(10).ConfigureAwait(false);
            Task t = Task.Run(async () => { await Task.Delay(10); });
            await t;
        }

        public async Task<int> DoSomeWork()
        {
            await Task.Yield();
            return 50;
        }

        private OtherClass GetClass()
        {
            return new OtherClass();
        }

        public async Task<int> LastTest()
        {
            await Task.Delay(10).ConfigureAwait(false);
            return await GetClass().GetValueAsync();
        }
        
        //This method should not generate a diagnostic since the analyzer will accept
        //any call to ConfigureAwait*
        public async Task<int> GetValue2Async()
        {
            await Task.Delay(12).ConfigureAwaitWithCulture(false);
            return 10;
        }
    }

    public class OtherClass
    {
        public Task<int> GetValueAsync()
        {
            return Task.FromResult(50);
        }
    }
}
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Code.Analyzers.Tests.Sources
{
    public class ThreadSleepAsyncTest
    {
        public async Task<int> GetValueAsync()
        {
            Thread.Sleep(50);
            await Task.Delay(10).ConfigureAwait(false);
            return await GetAnswerAsync().ConfigureAwait(false);
        }

        public async Task<int> GetValue2Async()
        {
            System.Threading.Thread.Sleep(350);
            await Task.Delay(10).ConfigureAwait(false);
            return await GetAnswerAsync().ConfigureAwait(false);
        }

        private async Task<int> GetAnswerAsync()
        {
            await Task.Delay(10).ConfigureAwait(false);
            return 42;
        }
    }
}
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Code.Analyzers.Tests.Sources
{
    public class ThreadSleepAsyncTest
    {
        public async Task<int> GetValueAsync()
        {
            await Task.Delay(50).ConfigureAwait(false);
            await Task.Delay(10).ConfigureAwait(false);
            return await GetAnswerAsync().ConfigureAwait(false);
        }

        public async Task<int> GetValue2Async()
        {
            await Task.Delay(350).ConfigureAwait(false);
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
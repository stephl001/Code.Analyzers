using System;
using System.IO;
using System.Threading.Tasks;

namespace Orckestra.Analyzers.Tests.Sources
{
    public class Dummy
    {
        private Task<int> GetNumberAsync()
        {
            return Task.FromResult(42);
        }

        public Task<int> GetValueAsync()
        {
            return GetNumberAsync();
        }

        public async Task<int> GeDelayedtValueAsync()
        {
            await Task.Delay(50).ConfigureAwait(false);
            return await GetNumberAsync().ConfigureAwait(false);
        }

        public async Task<int> GeDelayedtResponseToLifeAndUniverseAsync()
        {
            await Task.Delay(50).ConfigureAwait(false);
            return 42;
        }

        public Task WaitAsync(int delay)
        {
            return (Task.Delay(delay));
        }

        public async Task WaitWithLoggingAsync(int delay)
        {
            await Task.Delay(delay).ConfigureAwait(false);
            Log("Test");
        }

        private void Log(string text)
        {
        }

        public Task<int> MoreComplexAsync()
        {
            return Task.Run(async () => 
            {
                await Task.Delay(1234).ConfigureAwait(false);
                return 42;
            });
        }

        public async Task WithMultipleExits(bool condition, int delay)
        {
            if (condition)
                await Task.Delay(delay).ConfigureAwait(false);
        }

        public async Task WithMultipleExits2(bool condition, int delay)
        {
            if (condition)
                await Task.Delay(delay).ConfigureAwait(false);

            await Task.Delay(250).ConfigureAwait(false);
        }

        public Task<int> MoreComplexAsync2()
        {
            Func<int, Task<int>> func = async i => 
            {
                await Task.Delay(10).ConfigureAwait(false);
                return i;
            };
            return func(42);
        }

        public async Task WithMultipleExits3(bool condition, int delay)
        {
            if (condition)
            {
                await Task.Delay(delay).ConfigureAwait(false);
            }

            await Task.Delay(250).ConfigureAwait(false);
        }

        public async Task WithControlBlock(bool condition, int delay)
        {
            if (condition)
            {
                await Task.Delay(delay).ConfigureAwait(false);
            }
        }

        public async Task<int> WithControlBlock2(bool condition, int delay)
        {
            bool localCondition = true;
            while (localCondition == condition)
            {
                await Task.Delay(delay).ConfigureAwait(false);
            }

            return 10;
        }

        public async Task<int> GetValueInControlBlockAsync(bool condition)
        {
            if (condition)
                return await GetNumberAsync().ConfigureAwait(false);

            return 5;
        }

        public Task<int> MultipleReturnAwaitAsync(bool condition)
        {
            if (condition)
                return GetNumberAsync();

            return GetNumberAsync();
        }

        public async Task<int> GetData()
        {
            using (FileStream fs = File.OpenRead(@"test.dat"))
            {
                byte[] buffer = new byte[100];
                return await fs.ReadAsync(buffer, 0, 100).ConfigureAwait(false);
            }
        }

        public async Task<int> GetData2()
        {
            try
            {
                return await GetNumberAsync().ConfigureAwait(false);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task UseData()
        {
            using (FileStream fs = File.OpenRead(@"test.dat"))
            {
                await Task.Delay(10).ConfigureAwait(false);
            }
        }

        public async Task UseData2()
        {
            using (FileStream fs = File.OpenRead(@"test.dat"))
            {
                await Task.Delay(10).ConfigureAwait(false);
            }

            await Task.Delay(10).ConfigureAwait(false);
        }

        public async Task UseData3()
        {
            try
            {
                await Task.Delay(10).ConfigureAwait(false);
            }
            finally
            {
            }
        }

        public async Task UseData4()
        {
            try
            {
            }
            finally
            {
                await Task.Delay(10).ConfigureAwait(false);
            }
        }

        public async Task UseData5()
        {
            try
            {
            }
            catch
            {
                await Task.Delay(10).ConfigureAwait(false);
            }
        }

        public async Task<int> EvenMoreComplexAsync()
        {
            Task t = Task.Run(async () =>
            {
                await Task.Delay(10).ConfigureAwait(false);
                return 42;
            });
            return 12;
        }

        public async Task<DataObject> GetDataObjectAsync()
        {
            return new DataObject
            {
                Number = await GetNumberAsync().ConfigureAwait(false)
            };
        }

        public sealed class DataObject
        {
            public DataObject()
            {
            }

            public DataObject(int number)
            {
                Number = number;
            }

            public int Number { get; set; }
        }

        private int _number;
        public async Task Assignment()
        {
            _number = (await GetNumberAsync().ConfigureAwait(false));
        }
    }
}
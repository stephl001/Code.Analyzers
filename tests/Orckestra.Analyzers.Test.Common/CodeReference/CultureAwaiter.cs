using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace Orckestra.AsyncExtensions
{
    [ExcludeFromCodeCoverage]
    public class CultureAwaiter : INotifyCompletion
    {
        public CultureAwaiter(ConfiguredTaskAwaitable task)
        { }

        public bool IsCompleted { get; }

        public CultureAwaiter GetAwaiter()
        {
            throw new NotImplementedException();
        }
        
        public void GetResult()
        {

        }

        public void OnCompleted(Action continuation)
        {

        }
    }

    [ExcludeFromCodeCoverage]
    public class CultureAwaiter<T> : INotifyCompletion
    {
        public CultureAwaiter(ConfiguredTaskAwaitable<T> task)
        { }

        public bool IsCompleted { get; }

        public CultureAwaiter<T> GetAwaiter()
        {
            throw new NotImplementedException();
        }

        public T GetResult()
        {
            throw new NotImplementedException();
        }

        public void OnCompleted(Action continuation)
        {

        }
    }
}

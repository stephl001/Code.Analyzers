using Orckestra.AsyncExtensions;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Threading.Tasks
{
    [ExcludeFromCodeCoverage]
    public static class CultureAwaiterExtensions
    {
        public static CultureAwaiter ConfigureAwaitWithCulture(this Task task, bool continueOnCapturedContext)
        {
            throw new NotImplementedException();
        }
        
        public static CultureAwaiter<T> ConfigureAwaitWithCulture<T>(this Task<T> task, bool continueOnCapturedContext)
        {
            throw new NotImplementedException();
        }
    }
}

using System;
using System.Threading.Tasks;

namespace Code.Analyzers.Tests.Sources
{
    public class ThrowException
    {
        void InvalidExceptionThrow()
        {
            throw new Exception();
        }

        void InvalidQualifiedExceptionThrow()
        {
            throw new System.Exception();
        }
        void InvalidSystemExceptionThrow()
        {
            throw new SystemException();
        }

        void InvalidApplicationExceptionThrow()
        {
            throw new ApplicationException();
        }

        void InvalidStackOverflowExceptionThrow()
        {
            throw new StackOverflowException();
        }

        void InvalidOutOfMemoryExceptionThrow()
        {
            throw new OutOfMemoryException();
        }

        void ValidThrow()
        {
            throw new ArgumentException();
        }

        void ReThrow()
        {
            try
            {
                throw new InvalidOperationException();
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}
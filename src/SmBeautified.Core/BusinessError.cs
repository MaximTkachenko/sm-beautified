using System;

namespace SmBeautified.Core
{
    public sealed class BusinessError : Exception
    {
        public BusinessError(string message) : base(message)
        { }
    }
}

using System;

namespace SmBeautified.Ui
{
    public sealed class ParserError : Exception
    {
        public ParserError(string message) : base(message)
        { }
    }
}

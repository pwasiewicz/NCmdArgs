using System;

namespace NCmdArgs.Exceptions
{
    public abstract class ParserException : Exception
    {
        protected ParserException()
        {
        }

        protected ParserException(string message) : base(message)
        {
        }

        protected ParserException(string message, Exception inner) : base(message, inner)
        {
        }
    
        public abstract override string Message { get; }
    }
}

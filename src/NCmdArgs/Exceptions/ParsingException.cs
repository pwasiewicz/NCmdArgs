using System;

namespace NCmdArgs.Exceptions
{
    public class ParsingException : ParserException
    {
        public ParsingException(Exception thrown)
        {
            Thrown = thrown;
        }

        public Exception Thrown { get; }
        
        public override string Message => "Invalid type of argument";
    }
}

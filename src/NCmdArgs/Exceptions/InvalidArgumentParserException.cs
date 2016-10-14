
using System;

namespace NCmdArgs.Exceptions
{
    public class InvalidArgumentParserException : ParserException
    {

        public InvalidArgumentParserException(string argumentName)
        {
            this.ArgumentName = argumentName;
        }

        public string ArgumentName { get; set; }

        public InvalidArgumentParserException(Exception inner, string argumentName) : base(null, inner)
        {
            this.ArgumentName = argumentName;
        }

        public override string Message => $"Invalid argument type: \"{this.ArgumentName}\"";
    }
}

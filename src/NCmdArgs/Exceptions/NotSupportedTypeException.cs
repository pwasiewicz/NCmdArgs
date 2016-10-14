using System;

namespace NCmdArgs.Exceptions
{
    public class NotSupportedTypeException : ParserException
    {
        public NotSupportedTypeException(string type)
        {
            this.Type = type;
        }

        public NotSupportedTypeException(Exception inner, string type) : base(null, inner)
        {
            this.Type = type;
        }

        public override string Message => $"Type \"{this.Type}\"  of argument is not supported.";

        public virtual string Type { get; }
    }
}

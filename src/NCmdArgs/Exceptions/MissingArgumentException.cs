using System;

namespace NCmdArgs.Exceptions
{
    public class MissingArgumentException : ParserException
    {
        public MissingArgumentException(string argumentName)
        {
            if (argumentName == null) throw new ArgumentNullException(nameof(argumentName));

            this.ArgumentName = argumentName;
        }

        public string ArgumentName { get; }

        public override string Message => $"Argument \"{this.ArgumentName}\" missing";
    }
}

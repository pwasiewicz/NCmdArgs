using System;

namespace NCmdArgs.Exceptions
{
    public class DoubleArgumentException : ParserException
    {
        public DoubleArgumentException(string switchName)
        {
            if (switchName == null) throw new ArgumentNullException(nameof(switchName));
            this.ArgumentName = switchName;
        }

        public string ArgumentName { get; }

        public override string Message => $"Argument \"{this.ArgumentName}\" used twice.";
    }
}

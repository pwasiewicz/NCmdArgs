using System;

namespace NCmdArgs.Exceptions
{
    public class UnknownSwitchException : ParserException
    {
        
        public UnknownSwitchException(string switchName)
        {
            this.SwitchName = switchName;
        }
        
        public UnknownSwitchException(Exception inner, string switchName) : base(null, inner)
        {
            this.SwitchName = switchName;
        }

        public override string Message => $"Switch \"{this.SwitchName}\" is uknown.";

        public virtual string SwitchName { get; }
    }
}

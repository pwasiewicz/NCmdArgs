namespace NCmdArgs.Exceptions
{
    public class MissingSwitchArguments : ParserException
    {
        public MissingSwitchArguments(string switchName)
        {
            this.SwitchName = switchName;
        }

        public string SwitchName { get; }
        public override string Message => $"Invalid arguments for switch \"{this.SwitchName}\"";
    }
}

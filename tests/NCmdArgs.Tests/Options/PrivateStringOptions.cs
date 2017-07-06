using NCmdArgs.Attributes;

namespace NCmdArgs.Tests.Options
{
    internal class PrivateStringOptions
    {
        [CommandArgument]
        public string Prop { get; private set; } = "auto-field";
    }
}

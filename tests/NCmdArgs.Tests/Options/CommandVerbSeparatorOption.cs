using NCmdArgs.Attributes;

namespace NCmdArgs.Tests.Options
{
    internal class CommandVerbSeparatorOption
    {
        [CommandArgument]
        public string PropTest { get; private set; } = "auto-field";
    }
}

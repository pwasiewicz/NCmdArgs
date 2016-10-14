using NCmdArgs.Attributes;

namespace NCmdArgs.Tests.Options
{

    internal class SimpleStringOptions
    {
        [CommandArgument]
        public string Hello { get; set; }
    }
}

using NCmdArgs.Attributes;

namespace NCmdArgs.Tests.Options
{

    internal class SimpleStringOptions
    {
        [CommandArgument]
        [CommandInlineArgument(0)]
        public string Hello { get; set; }

        [CommandArgument]
        [CommandInlineArgument(1)]
        public string HelloSecond { get; set; }
    }
}

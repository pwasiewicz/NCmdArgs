using NCmdArgs.Attributes;

namespace NCmdArgs.Tests.Options
{
    public class VerbOptions
    {
        [CommandVerb (Description = "My sample verb")]
        public VerbCommand MyVerb { get; set; }

        [CommandArgument(Description =  "Hello world switch.")]
        public string Hello { get; set; }
    }

    public class VerbCommand
    {
        [CommandArgument]
        public string Hello { get; set; }
    }
}

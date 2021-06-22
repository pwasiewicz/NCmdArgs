using NCmdArgs.Attributes;

namespace NCmdArgs.Tests.Options
{
    public class VerbWithRequiredOptions
    {
        [CommandVerb (Description = "My sample verb")]
        public VerbWithRequiredCommand MyVerb { get; set; }

        [CommandArgument(Description =  "Hello world switch.")]
        public string Hello { get; set; }
    }

    public class VerbWithRequiredCommand
    {
        [CommandArgument(Required = true)]
        public string VerbHello { get; set; }
    }
}

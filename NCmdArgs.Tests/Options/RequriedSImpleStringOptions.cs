using NCmdArgs.Attributes;

namespace NCmdArgs.Tests.Options
{

    public class RequriedSImpleStringOptions
    {
        [CommandArgument(Required = true, Description = "Hello test value")]
        public string Hello { get; set; }
    }
}

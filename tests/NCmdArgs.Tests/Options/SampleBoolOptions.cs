using NCmdArgs.Attributes;

namespace NCmdArgs.Tests.Options
{
    public class SampleBoolOptions
    {
        [CommandArgument(ShortName = "h")]
        public bool MyBool { get; set; }
    }
}

using NCmdArgs.Attributes;

namespace NCmdArgs.Tests.Options
{
    public class CollectionsTestOptions<TCollection>
    {
        [CommandArgument]
        public TCollection Some { get; set; } 
    }
}

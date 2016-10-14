using System;
using NCmdArgs.Helpers.Types.Handlers.Helpers;

namespace NCmdArgs.Helpers.Types.Handlers
{
    internal class ShortHandler : ParsableBaseHandler<short>
    {
        public override Type Type => typeof(short);

        public ShortHandler() : base(short.TryParse)
        {
        }
    }

    internal class NullableShortHandler : ParsableBaseHandler<short?>
    {
        public override Type Type => typeof(short?);

        public NullableShortHandler() : base(NullableTryParseFactory.Produce<short>(short.TryParse))
        {
        }
    }
}

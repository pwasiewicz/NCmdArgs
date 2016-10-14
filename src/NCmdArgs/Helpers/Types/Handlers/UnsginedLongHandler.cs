using System;
using NCmdArgs.Helpers.Types.Handlers.Helpers;

namespace NCmdArgs.Helpers.Types.Handlers
{
    internal class UnsignedLongHandler : ParsableBaseHandler<ulong>
    {
        public override Type Type => typeof(ulong);

        public UnsignedLongHandler() : base(ulong.TryParse)
        {
        }
    }

    internal class UnsignedNullableLongHandler : ParsableBaseHandler<ulong?>
    {
        public override Type Type => typeof(ulong?);

        public UnsignedNullableLongHandler() : base(NullableTryParseFactory.Produce<ulong>(ulong.TryParse))
        {
        }
    }
}

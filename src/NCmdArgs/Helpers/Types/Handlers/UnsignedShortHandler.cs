using System;
using NCmdArgs.Helpers.Types.Handlers.Helpers;

namespace NCmdArgs.Helpers.Types.Handlers
{
    internal class UnsignedShortHandler : ParsableBaseHandler<ushort>
    {
        public override Type Type => typeof(ushort);

        public UnsignedShortHandler() : base(ushort.TryParse)
        {
        }
    }

    internal class UnsignedNullableShortHandler : ParsableBaseHandler<ushort?>
    {
        public override Type Type => typeof(ushort?);

        public UnsignedNullableShortHandler() : base(NullableTryParseFactory.Produce<ushort>(ushort.TryParse))
        {
        }
    }
}

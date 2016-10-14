using System;
using NCmdArgs.Helpers.Types.Handlers.Helpers;

namespace NCmdArgs.Helpers.Types.Handlers
{
    internal class UnsignedIntHandler : ParsableBaseHandler<uint>
    {
        public override Type Type => typeof(uint);

        public UnsignedIntHandler() : base(uint.TryParse)
        {
        }
    }

    internal class UnsignedNullableIntHandler : ParsableBaseHandler<uint?>
    {
        public override Type Type => typeof(uint?);

        public UnsignedNullableIntHandler() : base(NullableTryParseFactory.Produce<uint>(uint.TryParse))
        {
        }
    }
}

using System;
using NCmdArgs.Helpers.Types.Handlers.Helpers;

namespace NCmdArgs.Helpers.Types.Handlers
{
    internal class ByteHandler : ParsableBaseHandler<byte>
    {
        public override Type Type => typeof(byte);

        public ByteHandler() : base(byte.TryParse)
        {
        }
    }

    internal class NullableByteHandler : ParsableBaseHandler<byte?>
    {
        public override Type Type => typeof(byte?);

        public NullableByteHandler() : base(NullableTryParseFactory.Produce<byte>(byte.TryParse))
        {
        }
    }
}

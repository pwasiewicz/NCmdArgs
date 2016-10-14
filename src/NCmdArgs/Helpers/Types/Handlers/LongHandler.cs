using System;
using NCmdArgs.Helpers.Types.Handlers.Helpers;

namespace NCmdArgs.Helpers.Types.Handlers
{
    internal class LongHandler : ParsableBaseHandler<long>
    {
        public override Type Type => typeof(long);

        public LongHandler() : base(long.TryParse)
        {
        }
    }

    internal class NullableLongHandler : ParsableBaseHandler<long?>
    {
        public override Type Type => typeof(long?);

        public NullableLongHandler() : base(NullableTryParseFactory.Produce<long>(long.TryParse))
        {
        }
    }
}

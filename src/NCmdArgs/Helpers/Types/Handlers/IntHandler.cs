using System;
using NCmdArgs.Helpers.Types.Handlers.Helpers;

namespace NCmdArgs.Helpers.Types.Handlers
{
    internal class IntHandler : ParsableBaseHandler<int>
    {
        public override Type Type => typeof (int);

        public IntHandler() : base(int.TryParse)
        {
        }
    }

    internal class NullableIntHandler : ParsableBaseHandler<int?>
    {
        public override Type Type => typeof(int?);

        public NullableIntHandler() : base(NullableTryParseFactory.Produce<int>(int.TryParse))
        {
        }
    }
}

using System;
using NCmdArgs.Helpers.Types.Handlers.Helpers;

namespace NCmdArgs.Helpers.Types.Handlers
{
    internal class BoolHandler : ParsableBaseHandler<bool>
    {
        public override Type Type => typeof (bool);

        public BoolHandler() : base(bool.TryParse)
        {
        }

        protected override bool RequiresArgument => false;

        protected override object ParseInternal(string arg)
        {
            return true;
        }
    }

    internal class NullableBoolHandler : ParsableBaseHandler<bool?>
    {
        public override Type Type => typeof(bool?);

        public NullableBoolHandler() : base(NullableTryParseFactory.Produce<bool>(bool.TryParse))
        {
        }

        protected override bool RequiresArgument => false;

        protected override object ParseInternal(string arg)
        {
            return true;
        }
    }
}

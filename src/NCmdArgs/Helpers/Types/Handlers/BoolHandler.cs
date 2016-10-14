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

        protected override object ParseInternal(string arg)
        {
            if (arg == "t") return true;
            if (arg == "f") return false;
            if (arg == "1") return true;
            if (arg == "0") return false;

            return base.ParseInternal(arg);
        }
    }

    internal class NullableBoolHandler : ParsableBaseHandler<bool?>
    {
        public override Type Type => typeof(bool?);

        public NullableBoolHandler() : base(NullableTryParseFactory.Produce<bool>(bool.TryParse))
        {
        }

        protected override object ParseInternal(string arg)
        {
            if (arg == "t") return true;
            if (arg == "f") return false;
            if (arg == "1") return true;
            if (arg == "0") return false;

            return base.ParseInternal(arg);
        }
    }
}

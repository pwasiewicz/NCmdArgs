using System;
using System.Linq;
using NCmdArgs.Exceptions;

namespace NCmdArgs.Helpers.Types.Handlers
{
    internal class StringHandler : TypeHandler
    {
        public override Type Type => typeof (string);

        public override object Parse(ParserContext ctx)
        {
            if (!ctx.Arguments.Any()) throw new MissingSwitchArguments(ctx.AttributeHolder.Property.Name);

            return ctx.Arguments.Dequeue();
        }
    }
}

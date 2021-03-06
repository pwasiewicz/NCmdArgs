﻿using System.Linq;
using NCmdArgs.Exceptions;

namespace NCmdArgs.Helpers.Types.Handlers.Helpers
{
    public abstract class ParsableBaseHandler<T> : TypeHandler
    {
        public delegate bool ParseDelegate(string input, out T value);

        private readonly ParseDelegate parseFunction;

        protected ParsableBaseHandler(ParseDelegate parseFunction)
        {
            this.parseFunction = parseFunction;
        }

        protected virtual bool RequiresArgument => true;

        public override object Parse(ParserContext ctx)
        {
            if (RequiresArgument && !ctx.Arguments.Any()) throw new MissingSwitchArguments(ctx.AttributeHolder.Property.Name);

            if (!RequiresArgument) return this.ParseInternal(null);

            var arg = ctx.Arguments.First.Value;
            ctx.Arguments.RemoveFirst();

            if (string.IsNullOrWhiteSpace(arg))
                return ctx.AttributeHolder.Attribute.DefaultValue ?? default(T);

            return this.ParseInternal(arg);
        }

        protected virtual object ParseInternal(string arg)
        {

            T result;
            if (!this.parseFunction(arg, out result))
            {
                throw new InvalidArgumentParserException("arg");
            }

            return result;
        }
    }
}

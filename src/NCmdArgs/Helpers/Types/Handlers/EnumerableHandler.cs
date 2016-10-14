using System;
using System.Collections;
using System.Collections.Generic;

namespace NCmdArgs.Helpers.Types.Handlers
{
    public class EnumerableHandler : TypeHandler
    {
        public override Type Type => typeof (IEnumerable<>);

        public override object Parse(ParserContext ctx)
        {
            var elementType = this.GetElementType(ctx);
            var result = (IList) Activator.CreateInstance(typeof (List<>).MakeGenericType(elementType));

            while (ctx.Arguments.Count > 0)
            {
                var currentElement = ctx.Arguments.Peek();
                if (ctx.IsParameter(currentElement))
                {
                    break;
                }

                var handler = ctx.GetHandlerFor(elementType);
                result.Add(handler.Parse(ctx));
            }

            // generic !
            return result;
        }

        protected virtual Type GetElementType(ParserContext ctx)
        {
            return ctx.AttributeHolder.Property.PropertyType.GenericTypeArguments[0];
        }
    }
}

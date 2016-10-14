using System;
using System.Collections;
using System.Linq;

namespace NCmdArgs.Helpers.Types.Handlers
{
    public class ArrayHandler : EnumerableHandler
    {
        public override Type Type => typeof (Array);

        public override object Parse(ParserContext ctx)
        {
            var parsedEnumerable = (IEnumerable) base.Parse(ctx);
            var objectCollection = parsedEnumerable.Cast<object>().ToArray();

            var result = Array.CreateInstance(ctx.AttributeHolder.Property.PropertyType.GetElementType(),
                objectCollection.Length);

            Array.Copy(objectCollection, result, objectCollection.Length);

            return result;
        }

        protected override Type GetElementType(ParserContext ctx)
        {
            return ctx.AttributeHolder.Property.PropertyType.GetElementType();
        }
    }
}

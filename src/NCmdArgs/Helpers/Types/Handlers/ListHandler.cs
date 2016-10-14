using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace NCmdArgs.Helpers.Types.Handlers
{
    public class ListHandler : EnumerableHandler
    {
        public override Type Type => typeof (List<>);

        public override object Parse(ParserContext ctx)
        {
            var enumerable = (IEnumerable) base.Parse(ctx);

            if (ctx.AttributeHolder.Property.PropertyType.GetTypeInfo().IsAssignableFrom(enumerable.GetType().GetTypeInfo()))
            {
                return enumerable;
            }

            var list =
                (IList)Activator.CreateInstance(typeof (List<>).MakeGenericType(this.GetElementType(ctx)));

            foreach (var element in enumerable)
                list.Add(element);

            return list;
        }
    }
}

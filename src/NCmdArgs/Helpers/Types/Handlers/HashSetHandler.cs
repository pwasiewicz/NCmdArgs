using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace NCmdArgs.Helpers.Types.Handlers
{
    public class HashSetHandler : EnumerableHandler
    {
        public override Type Type => typeof (HashSet<>);

        public override object Parse(ParserContext ctx)
        {
            var enumerable = (IEnumerable)base.Parse(ctx);

            if (ctx.AttributeHolder.Property.PropertyType.GetTypeInfo().IsAssignableFrom(enumerable.GetType().GetTypeInfo()))
            {
                return enumerable;
            }

            var hashSet = Activator.CreateInstance(typeof(HashSet<>).MakeGenericType(this.GetElementType(ctx)));
            var addMethod = hashSet.GetType().GetRuntimeMethod("Add", new[] {this.GetElementType(ctx)});

            foreach (var element in enumerable)
                addMethod.Invoke(hashSet, new[] {element});

            return hashSet;
        }
    }
}

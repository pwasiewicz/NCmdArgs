using System;
using System.Collections.Generic;
using System.Reflection;
using NCmdArgs.Helpers.Types.Handlers;

namespace NCmdArgs.Helpers.Types
{
    internal  class TypeHandlersCollection
    {
        private readonly IDictionary<Type, TypeHandler> handlers;

        internal TypeHandlersCollection()
        {
            this.handlers = new Dictionary<Type, TypeHandler>();

            this.RegisterHandler(new BoolHandler());
            this.RegisterHandler(new NullableBoolHandler());

            this.RegisterHandler(new IntHandler());
            this.RegisterHandler(new NullableIntHandler());
            this.RegisterHandler(new UnsignedIntHandler());
            this.RegisterHandler(new UnsignedNullableIntHandler());

            this.RegisterHandler(new LongHandler());
            this.RegisterHandler(new NullableLongHandler());
            this.RegisterHandler(new UnsignedLongHandler());
            this.RegisterHandler(new UnsignedNullableLongHandler());

            this.RegisterHandler(new ShortHandler());
            this.RegisterHandler(new NullableShortHandler());
            this.RegisterHandler(new UnsignedShortHandler());
            this.RegisterHandler(new UnsignedNullableShortHandler());

            this.RegisterHandler(new ByteHandler());
            this.RegisterHandler(new NullableByteHandler());

            this.RegisterHandler(new StringHandler());

            this.RegisterHandler(new EnumerableHandler());
            this.RegisterHandler(new ArrayHandler());
            this.RegisterHandler(new ListHandler());
            this.RegisterHandler(new HashSetHandler());
        }

        internal void RegisterHandler(TypeHandler handler)
        {
            if (this.handlers.ContainsKey(handler.Type))
            {
                throw new InvalidOperationException("Handler already regitered.");
            }

            this.handlers.Add(handler.Type, handler);
        }

        public TypeHandler For(Type type)
        {
            if (this.handlers.ContainsKey(type))
                    return this.handlers[type];


            if (type.IsConstructedGenericType)
            {
                type = type.GetGenericTypeDefinition();

            }
            else if (type.IsArray)
            {
                type = type.GetTypeInfo().BaseType;
            }


            if (this.handlers.ContainsKey(type))
                return this.handlers[type];

            return null;
        }
    }
}

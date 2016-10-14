using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Reflection;

namespace NCmdArgs.Helpers.Types
{
    internal static class TypesCache
    {
        private static readonly ConcurrentDictionary<object, Type> TypeCache = new ConcurrentDictionary<object, Type>();

        private static readonly ConcurrentDictionary<Type, IEnumerable<PropertyInfo>> PropertiesCache =
            new ConcurrentDictionary<Type, IEnumerable<PropertyInfo>>();

        public static Type TypeFor(object obj)
        {
            return TypeCache.GetOrAdd(obj, o => o.GetType());
        }

        public static IEnumerable<PropertyInfo> PropertiesFor(Type type)
        {
            return PropertiesCache.GetOrAdd(type, t => t.GetRuntimeProperties());
        }

        public static IEnumerable<PropertyInfo> PropertiesFor(object obj)
        {
            var type = TypeFor(obj);
            return PropertiesFor(type);
        }
    }
}

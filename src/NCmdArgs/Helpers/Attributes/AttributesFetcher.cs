using System;
using System.Collections.Generic;
using System.Reflection;

namespace NCmdArgs.Helpers.Attributes
{
    internal class AttributesFetcher
    {
        public static IEnumerable<T> GetAttributes<T>(object obj)
            where T : Attribute
        {
            return GetAttributes<T>(obj.GetType());
        }

        public static IEnumerable<T> GetAttributes<T>(Type objType)
            where T : Attribute
        {
            var typeInfo = objType.GetTypeInfo();
            return typeInfo.GetCustomAttributes<T>(inherit:     true);
        }
    }
}

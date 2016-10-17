using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using NCmdArgs.Helpers.Types;

namespace NCmdArgs.Helpers.Properties
{
    internal class PropertyFetcher
    {
        public static IEnumerable<PropertyAttributeHolder<T>> GetPropertiesWithAttributes<T>(object obj)
            where T : Attribute
        {
            if (obj == null) throw new ArgumentNullException(nameof(obj));

            return GetPropertiesWithAttributes<T>(TypesCache.TypeFor(obj));
        }

        public static IEnumerable<PropertyAttributeHolder<T>> GetPropertiesWithAttributes<T>(Type type)
            where T :Attribute
        {
            if (type == null) throw new ArgumentNullException(nameof(type));

            var properties = TypesCache.PropertiesFor(type);

            // TODO cache
            return from propertyInfo in properties
                let attr = propertyInfo.GetCustomAttribute<T>()
                where attr != null
                select new PropertyAttributeHolder<T>(propertyInfo, attr);
        }
    }


    public class PropertyAttributeHolder<T>
        where T : Attribute
    {
        public PropertyAttributeHolder(PropertyInfo property, T attribute)
        {
            this.Property = property;
            this.Attribute = attribute;
        }

        public PropertyInfo Property { get; }
        public T Attribute { get; }

        public TAttr GetOtherAttribute<TAttr>()
            where TAttr : Attribute
        {
            return this.Property.GetCustomAttribute<TAttr>();
        }
    }
}

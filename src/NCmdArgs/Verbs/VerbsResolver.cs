using System;
using System.Collections.Generic;
using NCmdArgs.Attributes;
using NCmdArgs.Helpers.Properties;

namespace NCmdArgs.Verbs
{
    internal class VerbsResolver
    {
        public static object FetchVerbOptions(object obj, Queue<string> args, ParserConfiguration configuration, ref string verbsPath)
        {
            if (obj == null) throw new ArgumentNullException(nameof(obj));
            if (args == null) throw new ArgumentNullException(nameof(args));

            if (verbsPath == null) verbsPath = string.Empty;

            if (args.Count == 0) return obj;

            var firstArg = args.Peek();

            foreach (var prop in PropertyFetcher.GetPropertiesWithAttributes<CommandVerbAttribute>(obj))
            {
                if (!(prop.Attribute.Name ?? prop.Property.Name).Equals(firstArg, StringComparison.OrdinalIgnoreCase))
                    continue;

                var verbOptions = prop.Property.GetValue(obj);
                if (verbOptions == null)
                {
                    verbOptions = configuration.InstanceFactory(prop.Property.PropertyType);
                    if (verbOptions == null)
                        throw new InvalidOperationException("Instance factory returned null instance.");

                    prop.Property.SetValue(obj, verbOptions);
                }

                if (verbsPath != string.Empty) verbsPath += " ";

                verbsPath += args.Peek();
                args.Dequeue();
                return FetchVerbOptions(verbOptions, args, configuration, ref verbsPath);
            }

            return obj;
        }
    }
}

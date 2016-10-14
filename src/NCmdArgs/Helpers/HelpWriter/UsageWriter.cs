using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using NCmdArgs.Attributes;
using NCmdArgs.Helpers.Properties;

namespace NCmdArgs.Helpers.HelpWriter
{
    internal class UsageWriter
    {
        private readonly TextWriter writer;

        public UsageWriter(TextWriter writer)
        {
            this.writer = writer;
        }

        public void Write(IEnumerable<PropertyAttributeHolder<CommandVerbAttribute>> verbs, IEnumerable<PropertyAttributeHolder<CommandArgumentAttribute>> switches, ParserConfiguration conf)
        {
            if (switches == null) throw new ArgumentNullException(nameof(switches));

            var arguments = switches.OrderByDescending(prop => prop.Attribute.Required)
                .Select(prop => new
                                {
                                    Holder = prop,
                                    ArgBrief =
                                        $"{conf.LongSwitch}{prop.Property.Name}" +
                                        (prop.Attribute.IsShortAvailable()
                                            ? $", {conf.ShortSwitch}{prop.Attribute.ShortName}"
                                            : string.Empty)
                                }).ToList();

            var verbsSwitches = verbs
                .Select(prop => new
                                {
                                    Holder = prop,
                                    ArgBrief =
                                        prop.Property.Name + " (verb) "
                                }).ToList();

            var longestSwitch = arguments.Any() ? arguments.Max(p => p.ArgBrief.Length) : -1;

            const string inted = "   ";

            this.writer.WriteLine("Program arguments:");

            foreach (var verb in verbsSwitches)
            {
                var missingChars = longestSwitch - verb.ArgBrief.Length;

                this.writer.Write(inted);
                this.writer.Write(verb.ArgBrief);

                if (missingChars > 0)
                    this.writer.Write(new string(' ', missingChars));

                this.writer.Write(" ");
                this.writer.Write(verb.Holder.Attribute.Description);

                this.writer.WriteLine();
            }

            foreach (var argument in arguments)
            {
                var missingChars = longestSwitch - argument.ArgBrief.Length;

                this.writer.Write(inted);
                this.writer.Write(argument.ArgBrief);

                if (missingChars > 0)
                    this.writer.Write(new string(' ', missingChars));

                this.writer.Write(" ");

                if (!argument.Holder.Attribute.Required)
                    this.writer.Write("(optional)");

                this.writer.Write(" ");
                this.writer.Write(argument.Holder.Attribute.Description);

                this.writer.WriteLine();
            }            
        }

        public void Write(Type optionsType, ParserConfiguration configuration)
        {
            if (optionsType == null) throw new ArgumentNullException(nameof(optionsType));
            if (configuration == null) throw new ArgumentNullException(nameof(configuration));

            this.Write(PropertyFetcher.GetPropertiesWithAttributes<CommandVerbAttribute>(optionsType),
                PropertyFetcher.GetPropertiesWithAttributes<CommandArgumentAttribute>(optionsType), configuration);
        }

        public void Write(object options, ParserConfiguration configuration)
        {
            if (options == null) throw new ArgumentNullException(nameof(options));
            if (configuration == null) throw new ArgumentNullException(nameof(configuration));

            this.Write(options.GetType(), configuration);
        }
    }
}

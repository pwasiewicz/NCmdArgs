using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using NCmdArgs.Attributes;
using NCmdArgs.Exceptions;
using NCmdArgs.Helpers.HelpWriter;
using NCmdArgs.Helpers.Properties;
using NCmdArgs.Helpers.Types;
using NCmdArgs.Verbs;

namespace NCmdArgs
{
    public class CommandLineParser
    {
        public CommandLineParser()
        {
            this.Configuration = new ParserConfiguration();
        }

        public ParserConfiguration Configuration { get; }

        public object LastVerb { get; private set; }
        public string VerbPath { get; private set; }

        public object Parse(Type optionsType, string[] args)
        {
            var instance = this.Configuration.InstanceFactory(optionsType);
            if (!this.Parse(instance, args)) return null;

            return instance;
        }

        public bool TryPredictVerbs(string[] args, out string verbs, out string[] newArgs)
        {
            if (args == null) throw new ArgumentNullException(nameof(args));

            if (!args.Any())
            {
                verbs = string.Empty;
                newArgs = new string[0];
                return true;
            }

            verbs = string.Empty;
            var q = new Queue<string>(args);
            while (q.Count > 0)
            {
                var candidate = q.Peek();
                if (CouldBeVerb(candidate))
                {
                    if (verbs != string.Empty) verbs += " ";
                    verbs += candidate;
                    q.Dequeue();
                }
                else
                {
                    break;
                }
            }

            newArgs = q.ToArray();
            return true;
        }


        private bool CouldBeVerb(string term)
        {
            var r = new Regex("^\\w+$");
            return r.IsMatch(term);
        }

        public T Parse<T>(string[] args)
            where T : new()
        {
            if (args == null) throw new ArgumentNullException(nameof(args));

            return (T) this.Parse(typeof (T), args);
        }

        public bool Parse(object options, string[] args)
        {
            if (options == null) throw new ArgumentNullException(nameof(options));
            if (args == null) throw new ArgumentNullException(nameof(args));

            var argsQueue = new Queue<string>(args);

            string verbPath = null;
            var newOptions = VerbsResolver.FetchVerbOptions(options, argsQueue, this.Configuration, ref verbPath);
            this.VerbPath = verbPath;

            var isVerb = false;

            if (!ReferenceEquals(options, newOptions))
            {
                this.LastVerb = newOptions;
                isVerb = true;
            }
            else
            {
                this.LastVerb = null;
            }

            try
            {
                return this.ParseNoVerb(newOptions, argsQueue);
            }
            finally
            {
                if (isVerb)
                {
                    this.Configuration.VerbParsed(newOptions);
                }
            }
        }

        public void Usage<T>(TextWriter writer)
        {
            if (writer == null) throw new ArgumentNullException(nameof(writer));

            var helperWriter = new UsageWriter(writer);
            helperWriter.Write(typeof (T), this.Configuration);
        }

        internal bool ParseNoVerb(object options, Queue<string> args)
        {
            try
            {
                this.ParseNoVerbInternal(options, args);
                return true;
            }
            catch (ParserException)
            {
                if (this.Configuration.ErrorOutput != null)
                {
                    var hlp = new UsageWriter(this.Configuration.ErrorOutput);
                    hlp.Write(options, this.Configuration);

                    return false;
                }

                throw;
            }
        }        

        private void ParseNoVerbInternal(object options, Queue<string> args)
        {
            if (options == null) throw new ArgumentNullException(nameof(options));
            if (args == null) throw new ArgumentNullException(nameof(args));

            var switches = PropertyFetcher.GetPropertiesWithAttributes<CommandArgumentAttribute>(options).ToList();

            var usedProperties = new HashSet<PropertyInfo>();

            while (args.Count > 0)
            {
                var arg = args.Dequeue();
                var matchedSwitch =
                    switches.SingleOrDefault(sw => sw.Attribute.IsNameMatch(arg, sw.Property.Name, this.Configuration));

                if (matchedSwitch == null)
                {
                    throw new UnknownSwitchException(arg);
                }

                usedProperties.Add(matchedSwitch.Property);

                var typeParser = this.Configuration.TypeHandler.For(matchedSwitch.Property.PropertyType);
                if (typeParser == null)
                    throw new NotSupportedTypeException(matchedSwitch.Property.PropertyType.FullName);

                var result =
                    typeParser.Parse(new ParserContext(args, matchedSwitch,
                        localArg =>
                            switches.Any(sw => sw.Attribute.IsNameMatch(localArg, sw.Property.Name, this.Configuration)),
                        this.Configuration));

                matchedSwitch.Property.SetValue(options, result);
            }


            var missedProperties = switches.Where(cm => !usedProperties.Contains(cm.Property)).ToList();

            foreach (var prop in missedProperties.Where(prop => prop.Attribute.Required))
            {
                throw new MissingArgumentException(prop.Property.Name.ToLower());
            }

            foreach (var prop in missedProperties)
            {
                var defaultValue = prop.Attribute.DefaultValue;
                prop.Property.SetValue(options, defaultValue);
            }
        }
    }
}

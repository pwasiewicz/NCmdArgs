using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace NCmdArgs.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class CommandArgumentAttribute : Attribute
    {
        /// <summary>
        /// Short name that starts with one dash. f.e -f
        /// </summary>
        public string ShortName { get; set; }

        /// <summary>
        /// Short name that starts with one dash. f.e -f
        /// </summary>
        public string LongName { get; set; }

        /// <summary>
        /// Default value for parameter.
        /// </summary>
        public object DefaultValue { get; set; }

        /// <summary>
        /// Indiciates that swtich is required
        /// </summary>
        public bool Required { get; set; }

        /// <summary>
        /// Description of option that will be printed as help
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Match case for options name
        /// </summary>
        public bool MatchCase { get; set; }

        public bool IsNameMatch(string arg, string propName, ParserConfiguration conf)
        {
            if (!conf.IsArgSwitch(arg)) return false;

            var argName = conf.GetArgFromSwitch(arg);

            var comparison = StringComparison.OrdinalIgnoreCase;

            if (!string.IsNullOrEmpty(conf.CommandVerbSeparator))
            {
                var verbs = Regex.Split(propName, @"(?<!^)(?=[A-Z])");
                propName = string.Join(conf.CommandVerbSeparator, verbs.Select(v => v.ToLowerInvariant()));
            }


            if (this.MatchCase)
            {
                comparison = StringComparison.Ordinal;
            }

            return this.LongName != null && this.LongName.Equals(argName, comparison) ||
                   this.ShortName != null && this.ShortName.Equals(argName, comparison) ||
                   propName.Equals(argName, comparison);
        }

        public bool IsShortAvailable()
        {
            return !string.IsNullOrWhiteSpace(this.ShortName);
        }
    }
}

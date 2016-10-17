using System;

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

            if (this.MatchCase)
            {
                return propName.Equals(argName, StringComparison.Ordinal) ||
                       (this.ShortName != null && this.ShortName.Equals(argName, StringComparison.Ordinal));
            }

            return propName.Equals(argName, StringComparison.OrdinalIgnoreCase) ||
                   (this.ShortName != null && this.ShortName.Equals(argName, StringComparison.OrdinalIgnoreCase));
        }

         public bool IsShortAvailable()
         {
             return !string.IsNullOrWhiteSpace(this.ShortName);
         }
    }
}

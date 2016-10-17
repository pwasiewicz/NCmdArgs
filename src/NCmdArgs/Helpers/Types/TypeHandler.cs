using System;
using System.Collections.Generic;
using NCmdArgs.Attributes;
using NCmdArgs.Helpers.Properties;

namespace NCmdArgs.Helpers.Types
{
    public abstract class TypeHandler
    {
        public abstract Type Type { get; }

        public abstract object Parse(ParserContext ctx);
    }

    public class ParserContext
    {
        private readonly Func<string, bool> isSwitchChecker;
        private readonly ParserConfiguration configuration;

        public ParserContext(
                LinkedList<string> arguments, 
                PropertyAttributeHolder<CommandArgumentAttribute> attributeHolder,
                Func<string, bool> isSwitchChecker, ParserConfiguration configuration)
        {
            this.Arguments = arguments;
            this.AttributeHolder = attributeHolder;

            this.isSwitchChecker = isSwitchChecker;
            this.configuration = configuration;
        }

        public LinkedList<string> Arguments { get; } 
        public PropertyAttributeHolder<CommandArgumentAttribute> AttributeHolder { get; }

        public TypeHandler GetHandlerFor(Type type)
        {
            return this.configuration.TypeHandler.For(type);
        }

        public bool IsParameter(string arg)
        {
            return this.isSwitchChecker(arg);
        }
    }
}

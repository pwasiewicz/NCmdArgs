namespace NCmdArgs.Attributes
{
    using System;

    public class CommandInlineArgument : Attribute
    {
        internal int Position { get; set; }

        public CommandInlineArgument(int position)
        {
            this.Position = position;
        }
    }
}

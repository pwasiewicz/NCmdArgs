using System;

namespace NCmdArgs.Attributes
{
    public class CommandVerbAttribute : Attribute
    {
        public string Description { get; set; }     
        
        public string Name { get; set; }   
    }
}

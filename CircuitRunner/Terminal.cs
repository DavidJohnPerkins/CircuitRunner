using System;
namespace CircuitDesign_2
{
    public class Terminal : Item
    {
        public string SourceParent
        {
            get; set;
        }

        public string SourceObject
        {
            get; set;
        }

        public string SourceType
        {
            get; set;
        }
    }
}

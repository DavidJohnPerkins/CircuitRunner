using System;
using System.Collections.Generic;

namespace CircuitRunner
{
    public class Outputs_x : Item
    {
        public CollectionBase<Output> List
        {
            get;
            private set;
        }

        public Outputs_x()
        {
            List = new CollectionBase<Output>();
        }

        public void Add(Output output, Node terminalAInput)
        {
            if (!OutputExists(output.Id))
            {
                output.AttachTerminal(output, terminalAInput, "A");
                List.Add(output);
            }
        }

        private bool OutputExists(string id)
        {
            if (List.ItemExists(id))
                throw new InvalidOperationException(
                    String.Format("Build: Error - a node with the Id {0} has already been added - please check metadata", id));
            return false;
        }
    }      
}
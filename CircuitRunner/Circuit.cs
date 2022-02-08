using System;
using System.Collections.Generic;

namespace CircuitRunner
{
    public class Circuit : Item
    {
        public CollectionBase<Input> Inputs
        {
            get;
            private set;
        }

        public CollectionBase<Gate> Gates
        {
            get;
            //private set;
        }

        public CollectionBase<Output> Outputs
        {
            get;
            //private set;
        }

        public Circuit()
        {
            Inputs = new CollectionBase<Input>();
            Gates = new CollectionBase<Gate>();
            Outputs = new CollectionBase<Output>();
        }

        public void Evaluate(bool verbose, bool recalculate)
        {
            foreach (Gate g in Gates)
            {
                if (recalculate == true)
                {
                    LogMessage(String.Format("Recalculating gate {0}...", g.Id), true);
                    Gates.Report(g, verbose);
                    g.SetState();
                }
                Gates.Report(g, verbose);
            }
        }

        /*
        public void ConfigureBuffer(Buffer buffer, Node inputSource)
        {
            if (!Inputs.ItemExists(inputSource.Id))
            {
                buffer.AttachTerminal(buffer, inputSource, "A");
                Buffers.Add(buffer);
            }
        }
        */

        /*public void Add(Gate gate, Node terminalAInput)
        {
            if (!ItemExists(Gates, gate.Id))
            {
                gate.AttachTerminal(gate, terminalAInput, "A");
                Gates.Add(gate);
            }
        }

        public void Add(Gate gate, Node terminalAInput, Node terminalBInput)
        {
            if (!ItemExists(Gates, gate.Id))
            {
                gate.AttachTerminal(gate, terminalBInput, "B");
                Add(gate, terminalAInput);
            }
        }*/

        /*bool ItemExists(CollectionBase<Gate> list, string id)
        {
            if (list.ItemExists(id))
                throw new InvalidOperationException(
                    String.Format("Build: Error - a node with the Id {0} has already been added to circuit {1} - please check metadata", id, Id));
            return false;
        }*/

        public bool GetTerminalState(string gateId, string terminalId)
        {
            return Gates.Find(t => t.Id == gateId).GetTerminalState(gateId + terminalId);
        }
    }      
}
using System;

namespace CircuitRunner
{
    public class TerminalNode : Node
    {

        public CollectionBase<Terminal> Terminals;

        public TerminalNode()
        {
            Terminals = new CollectionBase<Terminal>();
        }

        void AddTerminal(string id, Node inputNode)
        {
            if (inputNode == null)
                throw new InvalidOperationException(
                    String.Format("Build: Error - terminal {0} could not be added to gate {1} - input node {2} does not exist", id, Id, inputNode.Id));
                    
            inputNode.StateChanged -= HandleStateChange;
            inputNode.StateChanged += HandleStateChange;
        }

        public void AttachTerminal(TerminalNode newGate, Node inputTerminal, string label)
        {
            try
            {
                newGate.AddTerminal(label, inputTerminal);
                LogMessage(String.Format("Gate build: Gate {0} terminal {1} attached successfully", newGate.Id, inputTerminal.Id), true);
            }
            catch (InvalidOperationException e)
            {
                throw e;
            }
        }

        public void HandleStateChange(object sender, StateChangedEventArgs e)
        {
            var term = Terminals.Find(t => t.SourceObject == e.NodeId);
            if (term != null)
            {
                LogMessage(String.Format("Event received: Circuit {3} node {2} reports that node {0} sent new state {1}", e.NodeId, e.NewState, Id, e.ParentId), true);
                UpdateTerminalState(term, e.NewState, true);
            }
            else
            {
                Console.WriteLine("Event received from {0} by {1} but terminal was not found", e.NodeId, Id);

            }
        }

        public virtual void UpdateTerminalState(Terminal term, bool newState, bool verbose)
        {
            if (term.State != newState)
            {
                term.State = newState;

                if (verbose == true)
                    LogMessage(String.Format("State change: Circuit {0} Gate {1} terminal {2} has new state {3}", ParentId, Id, term.Id, term.State), verbose);
            }
        }

        public bool GetTerminalState(string terminalId)
        {
            return Terminals.Find(t => t.Id == terminalId).State;
        }
    }      
}

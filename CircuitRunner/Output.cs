
namespace CircuitDesign_2
{
    public class Output : TerminalNode
    {
        Output()
        {
            RaiseEvents = true;
        }

        public override void UpdateTerminalState(Terminal term, bool newState, bool verbose)
        {
            base.UpdateTerminalState(term, newState, verbose);
            State = Terminals.Find(t => t.Id == term.Id).State;
        }
    }
}
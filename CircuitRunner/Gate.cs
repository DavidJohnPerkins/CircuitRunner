using System;
namespace CircuitRunner
{
    public class Gate : TerminalNode
    {
        public string Type
        {
            get;
            set;
        }

        public Gate()
        {
            RaiseEvents = true;
        }

        public override void UpdateTerminalState(Terminal term, bool newState, bool verbose)
        {
            base.UpdateTerminalState(term, newState, verbose);
            SetState();
        }

        public virtual void SetState()
        {
            switch (Type)
            {
                case "XOR":
                    State = Terminals.FindAll(t => t.State == true).Count == 1;
                    break;
                case "OR":
                    State = Terminals.Exists(t => t.State == true);
                    break;
                case "AND":
                    State = Terminals.TrueForAll(t => t.State == true);
                    break;
                case "NOT":
                    State = !Terminals.Find(t => t.Id == "A").State;
                    break;
                default:
                    State = State;
                    break;
            }
        }
    }
}

using System;
namespace CircuitDesign_2
{
    public class AndGate : Gate
    {
        public override void SetState()
        {
            State = Terminals.TrueForAll(t => t.State == true);
        }
    }
}

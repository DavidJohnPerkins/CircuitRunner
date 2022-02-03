using System;
namespace CircuitRunner
{
    public class AndGate : Gate
    {
        public override void SetState()
        {
            State = Terminals.TrueForAll(t => t.State == true);
        }
    }
}

using System;
using System.Collections.Generic;

namespace CircuitRunner
{
    public class OrGate : Gate
    {
        public override void SetState()
        {
            State = Terminals.Exists(t => t.State == true);
        }
    }
}

using System;
using System.Collections.Generic;

namespace CircuitRunner
{
    public class XOrGate : Gate
    {
        public override void SetState()
        {
            State = Terminals.FindAll(t => t.State == true).Count == 1;
        }
    }
}

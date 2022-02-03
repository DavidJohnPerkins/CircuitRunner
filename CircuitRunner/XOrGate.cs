using System;
using System.Collections.Generic;

namespace CircuitDesign_2
{
    public class XOrGate : Gate
    {
        public override void SetState()
        {
            State = Terminals.FindAll(t => t.State == true).Count == 1;
        }
    }
}

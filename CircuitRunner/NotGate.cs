using System;
using System.Collections.Generic;

namespace CircuitRunner
{
    public class NotGate : Gate
    {
        public override void SetState()
        {
            State = !Terminals.Find(t => t.Id == Id + "A").State;
        }
    }
}
 
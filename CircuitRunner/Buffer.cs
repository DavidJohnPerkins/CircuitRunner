using System;
using System.Collections.Generic;

namespace CircuitRunner
{
    public class Buffer : Gate
    {
        public override void SetState()
        {
            State = Terminals.Find(t => t.Id == "A").State;
        }
    }
}
 
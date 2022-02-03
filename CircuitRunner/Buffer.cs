using System;
using System.Collections.Generic;

namespace CircuitDesign_2
{
    public class Buffer : Gate
    {
        public override void SetState()
        {
            State = Terminals.Find(t => t.Id == "A").State;
        }
    }
}
 
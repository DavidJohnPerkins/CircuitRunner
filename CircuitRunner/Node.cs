using System;
namespace CircuitRunner
{
    public abstract class Node : Item
    {
        public event EventHandler<StateChangedEventArgs> StateChanged;

        private readonly bool _verbose = true;
        protected bool _state;

        public string ParentId
        {
            get; set;
        }

        public bool RaiseEvents
        {
            get; set;
        }

        public override bool State
        {
            get => _state;
            set
            {
                if (_state != value)
                {
                    _state = value;

                    if (RaiseEvents)
                    {
                        StateChangedEventArgs args = new StateChangedEventArgs { ParentId = ParentId, NodeId = Id, NewState = _state };
                        OnStateChanged(args);
                    }
                }
            }
        }

        protected void OnStateChanged(StateChangedEventArgs e)
        {
            LogMessage(String.Format("Event raised: Parent {0} node {1} state --> {2}", e.ParentId, e.NodeId, e.NewState), _verbose);
            if (StateChanged != null)
            {
                StateChanged?.Invoke(this, e);
            }
            else
            {
                LogMessage(String.Format("Parent {0} node {1} has no event handler", e.ParentId, e.NodeId), _verbose);
            }
        }


        public void LogMessage(string message, bool verbose)
        {
            if (verbose)
                Console.WriteLine(message);
        }

        public override string ToString()
        {
            return ParentId + Id;
        }
    }
}

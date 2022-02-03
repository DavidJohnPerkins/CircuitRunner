using System;
namespace CircuitRunner
{
    public abstract class Item
    {

        public string Id
        {
            get;
            set;
        }

        public virtual bool State
        {
            get;
            set;
        }

        public bool Equals(Item other)
        {
            throw new NotImplementedException();
        }

        public string EvaluationMode
        {
            get;
            set;
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;

namespace CircuitDesign_2
{
    public class CollectionBase<T> : List<T> where T : Item
    {
        public string Id
        {
            get;
            set;
        }

        public T GetItem(string id)
        {
            return this.Find(i => i.Id == id);
        }

        public bool ItemExists(string id)
        {
            return this.Exists(i => i.Id == id);
        }

        public virtual void Report(T item, bool verbose)
        {
            if (verbose == true)
                Console.WriteLine("Component report: {0} has state {1}", item.Id, item.State);
        }
    }
}

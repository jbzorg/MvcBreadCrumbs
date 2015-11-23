using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace MvcBreadCrumbs
{
    class State : IEnumerable<KeyValuePair<int, StateEntry>>
    {
        SortedList<int, StateEntry> Crumbs { get; } = new SortedList<int, StateEntry>();

        public void Add(string url, string label, int level = 0, bool onlyInsert = false)
        {
            StateEntry newStateEntry = new StateEntry(url, label);
            if (Crumbs.Any(z => z.Value.Equals(newStateEntry)))
            {
                if (onlyInsert) return;
                foreach (var item in Crumbs.SkipWhile(z => !z.Value.Equals(newStateEntry)).ToList())
                { Crumbs.Remove(item.Key); }
            }
            level = (level != 0) ? level : (Crumbs.Count > 0) ? Crumbs.Last().Key + 1 : 1;
            if (Crumbs.ContainsKey(level))
            {
                foreach (var item in Crumbs.SkipWhile(z => z.Key < level).ToList())
                { Crumbs.Remove(item.Key); }
            }
            Crumbs.Add(level, newStateEntry);
        }

        public IEnumerator<KeyValuePair<int, StateEntry>> GetEnumerator()
        {
            return Crumbs.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
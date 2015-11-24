using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MvcBreadCrumbs
{
    class State : IEnumerable<StateEntry>
    {
        List<StateEntry> crumbs = new List<StateEntry>();

        public Task Task { get; set; } = null;
        public CancellationTokenSource CancellationTokenSource { get; set; } = null;

        public void Add(string url, string label, int level = -1, bool head = false) => crumbs.Add(new StateEntry(url, label, level, head));

        public IEnumerator<StateEntry> GetEnumerator() => crumbs.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
    }
}
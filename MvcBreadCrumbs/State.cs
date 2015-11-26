using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MvcBreadCrumbs
{
    /// <summary>
    /// Queue
    /// </summary>
    class State : IEnumerable<StateEntry>
    {
        List<StateEntry> crumbs = new List<StateEntry>();

        /// <summary>
        /// Task for self destroy
        /// </summary>
        public Task Task { get; set; } = null;

        /// <summary>
        /// Timeout for self destroy
        /// </summary>
        public CancellationTokenSource CancellationTokenSource { get; set; } = null;

        /// <summary>
        /// Add crumb to queue
        /// </summary>
        /// <param name="url">URL</param>
        /// <param name="label">name of crumb</param>
        /// <param name="level">group of crumb</param>
        /// <param name="head">this crumb is top of queue</param>
        /// <param name="link">this crumb is link or simple text</param>
        public void Add(string url, string label, int level = -1, bool head = false, bool link = true) => crumbs.Add(new StateEntry(url, label, level, head, link));

        /// <summary>
        /// Get enumerator for queue
        /// </summary>
        /// <returns>Enumerator</returns>
        public IEnumerator<StateEntry> GetEnumerator() => crumbs.GetEnumerator();

        /// <summary>
        /// Get enumerator for queue
        /// </summary>
        /// <returns>Enumerator</returns>
        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
    }
}
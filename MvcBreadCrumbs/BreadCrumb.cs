using System;
using System.Collections.Generic;
using System.Linq;

namespace MvcBreadCrumbs
{
    /// <summary>
    /// Bread crumbs manager
    /// </summary>
    public static class BreadCrumb
    {
        /// <summary>
        /// Callback preprocessor for modify url
        /// </summary>
        public static Func<string, string> UrlPreprocessor { get; set; }

        /// <summary>
        /// Add crumb to queue
        /// </summary>
        /// <param name="url">URL</param>
        /// <param name="label">name of crumb</param>
        /// <param name="level">group of crumb</param>
        /// <param name="head">this crumb is top of queue</param>
        /// <param name="link">this crumb is link or simple text</param>
        /// <param name="id">id queue</param>
        /// <param name="timeOut">self destory timeout</param>
        /// <returns>id queue</returns>
        public static string Add(string url, string label, int level = -1, bool head = false, bool link = true,  string id = null, int timeOut = 60000)
        {
            if (string.IsNullOrEmpty(id)) id = Guid.NewGuid().ToString("N");
            State state = StateManager.GetState(id, timeOut);
            if (state == null) throw new Exception("Could not get State");

            state.Add(UrlPreprocessor?.Invoke(url) ?? url, label, level, head, link);
            return id;
        }

        /// <summary>
        /// Get queue
        /// </summary>
        /// <param name="id">id queue</param>
        /// <param name="timeOut">self destory timeout</param>
        /// <returns>queue of crumbs</returns>
        public static IEnumerable<object> Get(string id, int timeOut = 60000)
        {
            if (string.IsNullOrEmpty(id)) throw new ArgumentNullException(nameof(id));

            return StateManager.GetState(id, timeOut).Select(z => new
            {
                level = z.Level,
                label = z.Label,
                url = z.Url,
                hash = z.UrlHash,
                head = z.Head,
                link = z.Link
            });
        }
    }
}

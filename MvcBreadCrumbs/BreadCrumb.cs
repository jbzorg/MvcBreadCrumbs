using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MvcBreadCrumbs
{
    public static class BreadCrumb
    {
        public static string Add(string url, string label, int level = -1, bool head = false, string id = null, int timeOut = 60000)
        {
            if (string.IsNullOrEmpty(id)) id = Guid.NewGuid().ToString("N");
            State state = StateManager.GetState(id, timeOut);
            if (state == null) throw new Exception("Could not get State");

            state.Add(url, label, level, head);
            return id;
        }

        public static IEnumerable<object> Get(string id, int timeOut = 60000)
        {
            if (string.IsNullOrEmpty(id)) throw new ArgumentNullException(nameof(id));

            return StateManager.GetState(id, timeOut).Select(z => new
            {
                level = z.Level,
                label = z.Label,
                url = z.Url,
                hash = z.UrlHash,
                head = z.Head
            });
        }
    }
}

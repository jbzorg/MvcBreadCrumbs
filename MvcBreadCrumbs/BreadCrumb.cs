using System;
using System.Linq;
using System.Text;

namespace MvcBreadCrumbs
{
    public static class BreadCrumb
    {
        static IProvideBreadCrumbsSession defaultSessionProvider = new HttpSessionProvider();

        public static void Add(string url, string label, int level = 0, bool onlyInsert = false, IProvideBreadCrumbsSession sessionProvider = null)
        {
            var state = StateManager.GetState((sessionProvider ?? defaultSessionProvider).SessionId);
            if (state == null) throw new Exception("Could not find SessionId");

            state.Add(url, label, level, onlyInsert);
        }

        public static string Display(string addClasses = null, IProvideBreadCrumbsSession sessionProvider = null)
        {
            var state = StateManager.GetState((sessionProvider ?? defaultSessionProvider).SessionId);
            if (state == null) throw new Exception("Could not find SessionId");

            if (state.Count() == 0) return "<!-- BreadCrumbs stack is empty -->";

            StringBuilder sb = new StringBuilder($"<ul class=\"breadcrumb {addClasses}\">");
            foreach (var item in state)
            { sb.Append($"<li><a href=\"{item.Value.Url}\">{item.Value.Label}</a></li>"); }
            sb.Append("</ul>");
            return sb.ToString();
        }

        public static string DisplayRaw(IProvideBreadCrumbsSession sessionProvider = null)
        {
            var state = StateManager.GetState((sessionProvider ?? defaultSessionProvider).SessionId);
            if (state == null) throw new Exception("Could not find SessionId");

            if (state.Count() == 0) return "<!-- BreadCrumbs stack is empty -->";

            return string.Join(" > ", state.Select(x => $"<a href=\"{x.Value.Url}\">{x.Value.Label}</a>"));
        }
    }
}

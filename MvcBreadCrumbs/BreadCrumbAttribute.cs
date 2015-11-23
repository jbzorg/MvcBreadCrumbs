using System;
using System.Web.Mvc;

namespace MvcBreadCrumbs
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = false)]
    public class BreadCrumbAttribute : ActionFilterAttribute
    {
        IProvideBreadCrumbsSession _sessionProvider;

        public bool Clear { get; set; }
        public string Label { get; set; }
        public int Level { get; set; }
        public IProvideBreadCrumbsSession SessionProvider
        {
            get
            { return _sessionProvider ?? new HttpSessionProvider(); }
            set
            { _sessionProvider = value; }
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.IsChildAction) return;
            if (filterContext.HttpContext.Request.HttpMethod != "GET") return;
            if (Clear) StateManager.RemoveState(SessionProvider.SessionId);

            var state = StateManager.GetState(SessionProvider.SessionId);
            if (state == null) throw new Exception("Could not find SessionId");

            state.Add(filterContext.HttpContext.Request.Url.ToString(),
                Label ?? (filterContext.RouteData.Values["action"] as string) ?? "No Label", Level);
        }
    }
}
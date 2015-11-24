using System;
using System.Web.Mvc;

namespace MvcBreadCrumbs
{
    //TODO: implement attribute in future
    /*[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = false)]
    public class BreadCrumbAttribute : ActionFilterAttribute
    {
        IBreadCrumbsIdGenerator _sessionProvider;

        public bool Clear { get; set; }
        public string Label { get; set; }
        public int Level { get; set; }
        public IBreadCrumbsIdGenerator SessionProvider
        {
            get
            { return _sessionProvider ?? new DefaultBreadCrumbsIdGenerator(); }
            set
            { _sessionProvider = value; }
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.IsChildAction) return;
            if (filterContext.HttpContext.Request.HttpMethod != "GET") return;
            if (Clear) StateManager.RemoveState(SessionProvider.GetNewId);

            var state = StateManager.GetState(SessionProvider.GetNewId);
            if (state == null) throw new Exception("Could not find SessionId");

            state.Add(filterContext.HttpContext.Request.Url.ToString(),
                Label ?? (filterContext.RouteData.Values["action"] as string) ?? "No Label", Level);
        }
    }*/
}
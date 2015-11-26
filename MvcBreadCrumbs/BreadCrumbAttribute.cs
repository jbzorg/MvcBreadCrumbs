using System;
using System.Web.Mvc;

namespace MvcBreadCrumbs
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = false)]
    public class BreadCrumbAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// Name of crumb
        /// </summary>
        public string Label { get; }

        /// <summary>
        /// Group of crumb
        /// </summary>
        public int Level { get; set; } = -1;

        /// <summary>
        /// This crumb is top of queue
        /// </summary>
        public bool Head { get; set; } = false;

        /// <summary>
        /// This crumb is link or simple text
        /// </summary>
        public bool Link { get; set; } = true;

        /// <summary>
        /// Self destory timeout
        /// </summary>
        public int TimeOut { get; set; } = 60000;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="url">URL</param>
        /// <param name="label">name of crumb</param>
        public BreadCrumbAttribute(string label)
        {
            if (string.IsNullOrEmpty(label)) throw new ArgumentNullException(nameof(label));
            Label = label;
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (filterContext.IsChildAction) return;
            if (filterContext.HttpContext.Request.HttpMethod != "GET") return;

            Action?.Invoke(filterContext, Label, Level, Head, Link, TimeOut);
        }

        /// <summary>
        /// Callback adding crumb to queue
        /// </summary>
        public static Action<ActionExecutedContext, string, int, bool, bool, int> Action { get; set; }
    }
}
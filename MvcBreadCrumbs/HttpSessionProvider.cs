using System.Web;

namespace MvcBreadCrumbs
{
    public class HttpSessionProvider : IProvideBreadCrumbsSession
    {
        public string SessionId
        {
            get
            { return HttpContext.Current.Session.SessionID; }
        }
    }
}

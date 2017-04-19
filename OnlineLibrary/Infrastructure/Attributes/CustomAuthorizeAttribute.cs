using System.Web;
using System.Web.Mvc;

namespace OnlineLibrary.Infrastructure.Attributes
{
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var authorized = base.AuthorizeCore(httpContext);

            if(!authorized)
            {
                // The user is not authenticated or forms authentication cookie expired
                return false;
            }

            var sessionReaderVar = httpContext.Session["Reader"];

            if (sessionReaderVar == null)
            {
                // The session has expired
                return false;
            }

            return true;
        }
    }
}
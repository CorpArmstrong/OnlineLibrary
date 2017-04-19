using System.Web;
using System.Web.Mvc;
using OnlineLibrary.Models;

namespace OnlineLibrary.Infrastructure.Attributes
{
    public class AllowAdminAttribute : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var authorized = base.AuthorizeCore(httpContext);
            Reader sessionVarReader = httpContext.Session["Reader"] as Reader;

            if (sessionVarReader != null && sessionVarReader.RoleId == 1)
            {
                return true;
            }

            return false;
        }
    }
}
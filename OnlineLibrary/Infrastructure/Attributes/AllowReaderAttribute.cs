using System.Web;
using System.Web.Mvc;
using OnlineLibrary.Models;

namespace OnlineLibrary.Infrastructure.Attributes
{
    public class AllowReaderAttribute : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var authorized = base.AuthorizeCore(httpContext);
            Reader sessionVarReader = httpContext.Session["Reader"] as Reader;

            if (sessionVarReader != null && (sessionVarReader.RoleId < 3 && sessionVarReader.RoleId >= 0))
            {
                return true;
            }

            return false;
        }
    }
}
using OnlineLibrary.Infrastructure.Authentication;
using OnlineLibrary.Models;
using System.Web.Security;
using System.Web.Mvc;

namespace OnlineLibrary.Controllers
{
    public class AccountController : Controller
    {
        ReaderAuthentication authentication = new ReaderAuthentication();

        [HttpGet]
        public ActionResult Login()
        {
            return View(new LoginViewModel());
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                Reader reader = null;

                if (authentication.CheckLogin(model.Nickname, model.Password, out reader))
                {
                    if (reader != null)
                    {
                        Session["Reader"] = reader;
                        Session["Nickname"] = reader.NickName;
                        FormsAuthentication.SetAuthCookie(reader.NickName, false);
                        return Redirect(returnUrl ?? Url.Action("Index", "Home"));
                    }
                }
                else
                {
                    return View();
                }
            }

            ModelState.AddModelError("", "Incorrect username or password!");
            return View();
        }

        public ActionResult Logout()
        {
            Session["Reader"] = null;
            Session["Nickname"] = null;
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View(new Reader());
        }

        [HttpPost]
        public ActionResult Register(Reader reader)
        {
            if (ModelState.IsValid)
            {
                if(authentication.CreateReader(reader))
                {
                    return RedirectToAction("Login", "Account");
                }
            }
            return View();
        }
    }
}
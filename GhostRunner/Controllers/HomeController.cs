using GhostRunner.SL;
using GhostRunner.ViewModels.Home;
using GhostRunner.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace GhostRunner.Controllers
{
    public class HomeController : Controller
    {
        private UserService _userService;

        public HomeController()
        {
            _userService = new UserService();
        }

        [NoCache]
        [CheckAuthenticated]
        public ActionResult Index()
        {
            IndexModel indexModel = new IndexModel();

            return View(indexModel);
        }

        [NoCache]
        [CheckAuthenticated]
        public ActionResult SignIn()
        {
            SignInModel signInModel = new SignInModel();

            if (!String.IsNullOrEmpty(Request.QueryString["errorCode"])) signInModel.ErrorMessage = Properties.Resources.ResourceManager.GetString(Request.QueryString["errorCode"]);

            return View(signInModel);
        }

        [NoCache]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult SignIn(SignInModel signInModel)
        {
            User user = _userService.Authenticate(signInModel.User.Email, signInModel.Password);

            if (user != null)
            {
                String sessionId = _userService.UpdateSessionId(user.ID);

                FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, "DM", DateTime.UtcNow, DateTime.UtcNow.AddMinutes(FormsAuthentication.Timeout.TotalMinutes), false, sessionId);

                String hashedTicket = FormsAuthentication.Encrypt(ticket);
                HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, hashedTicket);
                Response.Cookies.Add(cookie);

                return RedirectToAction("Index", "Main");
            }
            else
            {
                return RedirectToAction("SignIn", new { @errorCode = "INVALID_CREDENTIALS" });
            }
        }

        [NoCache]
        [CheckAuthenticated]
        public ActionResult SignUp()
        {
            SignUpModel signUpModel = new SignUpModel();

            if (!String.IsNullOrEmpty(Request.QueryString["errorCode"])) signUpModel.ErrorMessage = Properties.Resources.ResourceManager.GetString(Request.QueryString["errorCode"]);

            return View(signUpModel);
        }

        [NoCache]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult SignUp(SignUpModel signUpModel)
        {
            if (_userService.GetUserByEmail(signUpModel.User.Email) == null)
            {
                User newUser = _userService.InsertUser(signUpModel.User.Name, signUpModel.User.Email, signUpModel.Password);

                if (newUser != null)
                {
                    String sessionId = _userService.UpdateSessionId(newUser.ID);

                    FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, "DM", DateTime.UtcNow, DateTime.UtcNow.AddMinutes(FormsAuthentication.Timeout.TotalMinutes), false, sessionId);

                    String hashedTicket = FormsAuthentication.Encrypt(ticket);
                    HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, hashedTicket);
                    Response.Cookies.Add(cookie);

                    return RedirectToAction("Index", "Main");
                }
                else
                {
                    return RedirectToAction("SignUp", new { @errorCode = "ERROR_SIGNING_UP" });
                }
            }
            else
            {
                return RedirectToAction("SignUp", new { @errorCode = "EMAIL_ADDRESS_ALREADY_TAKEN" });
            }
        }

        public ActionResult Logout()
        {
            // Remove the formathentication ticket
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}

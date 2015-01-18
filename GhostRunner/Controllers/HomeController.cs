using GhostRunner.SL;
using GhostRunner.ViewModels.Home;
using GhostRunner.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Configuration;

namespace GhostRunner.Controllers
{
    public class HomeController : Controller
    {
        #region Private Properties

        private UserService _userService;

        #endregion

        #region Constructors

        public HomeController()
        {
            _userService = new UserService(new GhostRunnerContext(ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString));
        }

        #endregion

        #region Log into GhostRunner

        [NoCache]
        [CheckAuthenticated]
        public ActionResult Index()
        {
            IndexModel indexModel = new IndexModel();
            indexModel.AllowAccountCreate = Properties.Settings.Default.AllowAccountCreate;

            if (!String.IsNullOrEmpty(Request.QueryString["errorCode"])) indexModel.ErrorMessage = Properties.Resources.ResourceManager.GetString(Request.QueryString["errorCode"]);

            return View(indexModel);
        }

        [NoCache]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Index(IndexModel indexModel)
        {
            User user = _userService.Authenticate(indexModel.User.Email, indexModel.Password);

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
                return RedirectToAction("Index", new { @errorCode = "INVALID_CREDENTIALS" });
            }
        }

        #endregion

        #region Create a GhostRunner account

        [NoCache]
        [CheckAuthenticated]
        public ActionResult SignUp()
        {
            if (Properties.Settings.Default.AllowAccountCreate)
            {
                SignUpModel signUpModel = new SignUpModel();
                signUpModel.AllowAccountCreate = Properties.Settings.Default.AllowAccountCreate;

                if (!String.IsNullOrEmpty(Request.QueryString["errorCode"])) signUpModel.ErrorMessage = Properties.Resources.ResourceManager.GetString(Request.QueryString["errorCode"]);

                return View(signUpModel);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [NoCache]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult SignUp(SignUpModel signUpModel)
        {
            if (Properties.Settings.Default.AllowAccountCreate)
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
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        #endregion

        #region Logout actions

        public ActionResult Logout()
        {
            // Remove the formathentication ticket
            FormsAuthentication.SignOut();

            return RedirectToAction("Index", "Home");
        }

        #endregion
    }
}

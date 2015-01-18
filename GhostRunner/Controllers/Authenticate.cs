using GhostRunner.Models;
using GhostRunner.SL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GhostRunner.Controllers
{
    public class Authenticate : ActionFilterAttribute
    {
        /// <summary>
        /// Basic user authentication filter, checks if the user is currently authenticated
        /// </summary>
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            String requestedUrl = HttpUtility.UrlEncode(HttpContext.Current.Request.Url.AbsolutePath.Replace("+", "%20"));

            // Check if the form authentication says the user is authenticated
            if (!filterContext.HttpContext.Request.IsAuthenticated)
            {
                if (!filterContext.HttpContext.Request.IsAjaxRequest())
                {
                    filterContext.Result = new RedirectResult("~/Home/Index?requested=" + requestedUrl);
                }
                else
                {
                    filterContext.Result = new RedirectResult("~/Home/AjaxUnauthenticated");
                }
                return;
            }

            // Retrieve the id of the user authenticated
            String sessionId = ((CustomPrincipal)HttpContext.Current.User).Sessionid;

            if (!String.IsNullOrEmpty(sessionId))
            {
                UserService userService = new UserService(new GhostRunnerContext(ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString));

                // Check the id is associated to a valid user
                User user = userService.GetUser(sessionId);

                if (user == null)
                {
                    filterContext.Result = new RedirectResult("~/Home/Index?errorCode=INVALID_USER_ACCESS");
                    return;
                }

                // Set the employee variable so it can be accessed as a globel variable for authenticated actions
                filterContext.Controller.ViewData["User"] = user;
            }
            else
            {
                filterContext.Result = new RedirectResult("~/Home/Index?errorCode=INVALID_USER_ACCESS");
                return;
            }
        }

        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            filterContext.HttpContext.Response.Cache.SetExpires(DateTime.UtcNow.AddDays(-1));
            filterContext.HttpContext.Response.Cache.SetValidUntilExpires(false);
            filterContext.HttpContext.Response.Cache.SetRevalidation(HttpCacheRevalidation.AllCaches);
            filterContext.HttpContext.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            filterContext.HttpContext.Response.Cache.SetNoStore();

            base.OnResultExecuting(filterContext);
        }
    }
}
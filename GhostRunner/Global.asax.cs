using GhostRunner.Controllers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Principal;
using System.Threading;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;

namespace GhostRunner
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            // log4net configuration
            string log4netConfig = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Log4Net.config");
            log4net.Config.XmlConfigurator.ConfigureAndWatch(new FileInfo(log4netConfig));
        }

        protected void Application_OnPostAuthenticateRequest(object sender, EventArgs e)
        {
            HttpCookie formsCookie = Request.Cookies[FormsAuthentication.FormsCookieName];

            if (formsCookie != null)
            {
                FormsAuthenticationTicket auth = FormsAuthentication.Decrypt(formsCookie.Value);

                var principal = new CustomPrincipal(new GenericIdentity(auth.Name), new String[] { }, auth.UserData);

                Context.User = Thread.CurrentPrincipal = principal;
            }
        }
    }
}
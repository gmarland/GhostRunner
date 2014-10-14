using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace GhostRunner.Controllers
{
    /// <summary>
    /// This class is used for storing a users sessionId in form authentication
    /// </summary>
    public class CustomPrincipal : GenericPrincipal
    {
        public CustomPrincipal(IIdentity identity, String[] roles, String sessionId)
            : base(identity, roles)
        {
            Sessionid = sessionId;
        }

        public String Sessionid { get; set; }
    }
}
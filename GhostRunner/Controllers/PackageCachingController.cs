using GhostRunner.Models;
using GhostRunner.SL;
using GhostRunner.ViewModels.PackageCaching.Partials;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.Mvc;

namespace GhostRunner.Controllers
{
    public class PackageCachingController : Controller
    {
        #region Private Properties

        private PackageCacheService _packageCacheService;

        #endregion

        #region Constructors

        public PackageCachingController()
        {
            _packageCacheService = new PackageCacheService(new GhostRunnerContext(ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString));
        }

        #endregion

        [NoCache]
        [Authenticate]
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetPackageCache(String projectId)
        {
            PackageCacheViewModel packageCacheViewModel = new PackageCacheViewModel();
            packageCacheViewModel.PackageCache = _packageCacheService.GetAllProjectPackageCache(projectId);

            return PartialView("Partials/PackageCache", packageCacheViewModel);
        }
    }
}
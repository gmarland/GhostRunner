using GhostRunner.Models;
using GhostRunner.SL;
using GhostRunner.Utils;
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

        private ProjectService _projectService;
        private PackageCacheService _packageCacheService;

        #endregion

        #region Constructors

        public PackageCachingController()
        {
            _projectService = new ProjectService(new GhostRunnerContext(ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString));
            _packageCacheService = new PackageCacheService(new GhostRunnerContext(ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString));
        }

        #endregion

        [NoCache]
        [Authenticate]
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetPackageCache(String projectId)
        {
            PackageCacheViewModel packageCacheViewModel = new PackageCacheViewModel();
            packageCacheViewModel.Project = _projectService.GetProject(projectId);
            packageCacheViewModel.PackageCache = _packageCacheService.GetAllProjectPackageCache(projectId);

            return PartialView("Partials/PackageCache", packageCacheViewModel);
        }

        [NoCache]
        [Authenticate]
        [AcceptVerbs(HttpVerbs.Put)]
        public ActionResult UpdatePackageCache(String projectId, String packageCacheId, Boolean cache)
        {
            _packageCacheService.UpdatePackageCache(packageCacheId, cache);

            return Content(JSONHelper.BuildStatusMessage("success"));
        }

        [NoCache]
        [Authenticate]
        [AcceptVerbs(HttpVerbs.Delete)]
        public ActionResult DeletePackageCache(String projectId, String packageCacheId)
        {
            _packageCacheService.DeletePackageCache(packageCacheId, Properties.Settings.Default.PackageCacheLocation);

            return PartialView("Partials/PackageCacheList", _packageCacheService.GetAllProjectPackageCache(projectId));
        }
    }
}
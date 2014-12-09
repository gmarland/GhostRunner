using GhostRunner.Models;
using GhostRunner.SL;
using GhostRunner.ViewModels.Schedule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GhostRunner.Controllers
{
    public class ScheduleController : Controller
    {
        #region Private Properties

        private ProjectService _projectService;

        #endregion

        #region Constructors

        public ScheduleController()
        {
            _projectService = new ProjectService();
        }

        #endregion

        [NoCache]
        [Authenticate]
        public ActionResult Index(String id)
        {
            IndexModel indexModel = new IndexModel();

            indexModel.User = ((User)ViewData["User"]);
            indexModel.Project = _projectService.GetProject(id);

            return View(indexModel);
        }

    }
}

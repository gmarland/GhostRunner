using GhostRunner.Models;
using GhostRunner.SL;
using GhostRunner.ViewModels.History;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GhostRunner.Controllers
{
    public class HistoryController : Controller
    {
        #region Private Properties

        private ProjectService _projectService;
        private TaskService _taskService;

        #endregion

        #region Constructors

        public HistoryController()
        {
            _projectService = new ProjectService();
            _taskService = new TaskService();
        }

        #endregion

        [NoCache]
        [Authenticate]
        public ActionResult Index(String id)
        {
            IndexModel indexModel = new IndexModel();

            indexModel.User = ((User)ViewData["User"]);
            indexModel.Project = _projectService.GetProject(id);
            indexModel.Tasks = _taskService.GetAllTasks(indexModel.Project.ID);

            return View(indexModel);
        }

    }
}

using GhostRunner.Models;
using GhostRunner.SL;
using GhostRunner.ViewModels.Main;
using GhostRunner.ViewModels.Main.Partials;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GhostRunner.Controllers
{
    public class MainController : Controller
    {
        private ProjectService _projectService;

        public MainController()
        {
            _projectService = new ProjectService();
        }

        [NoCache]
        [Authenticate]
        public ActionResult Index()
        {
            IndexModel indexModel = new IndexModel();
            indexModel.User = ((User)ViewData["User"]);
            indexModel.Projects = _projectService.GetAllProjects(((User)ViewData["User"]).ID);

            return View(indexModel);
        }

        #region Create a new project

        [Authenticate]
        public ActionResult GetNewProjectDialog()
        {
            NewProjectModel newProjectModel = new NewProjectModel();

            return PartialView("Partials/NewProject", newProjectModel);
        }
        
        [Authenticate]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult InsertNewProject(NewProjectModel newProjectModel)
        {
            Project project = _projectService.InsertProject(((User)ViewData["User"]).ID, newProjectModel.Project.Name);
            
            if (project != null) return RedirectToAction("Index/" + project.ExternalId, "Projects");
            else return RedirectToAction("Index", "Main");
        }

        #endregion
    }
}

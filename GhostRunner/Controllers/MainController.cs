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

        [NoCache]
        [Authenticate]
        public ActionResult GetNewProjectDialog()
        {
            NewProjectModel newProjectModel = new NewProjectModel();

            return PartialView("Partials/NewProject", newProjectModel);
        }

        [NoCache]
        [Authenticate]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult InsertNewProject(NewProjectModel newProjectModel)
        {
            Project project = _projectService.InsertProject(((User)ViewData["User"]).ID, newProjectModel.Project.Name);

            if (project != null) return RedirectToAction("Index/" + project.ExternalId, "Scripts");
            else return RedirectToAction("Index", "Main");
        }

        #endregion

        #region Edit a project

        [NoCache]
        [Authenticate]
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetEditProjectDialog(String projectId)
        {
            EditProjectModel editProjectModel = new EditProjectModel();
            editProjectModel.Project = _projectService.GetProject(projectId);

            return PartialView("Partials/EditProject", editProjectModel);
        }

        [NoCache]
        [Authenticate]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult UpdateProject(String id, EditProjectModel editProjectModel)
        {
            Boolean updateSuccessful = _projectService.UpdateProject(id, editProjectModel.Project.Name);

            return RedirectToAction("Index", "Main");
        }

        #endregion

        #region Delete a project

        [NoCache]
        [Authenticate]
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult ConfirmDeleteProject(String projectId)
        {
            ConfirmDeleteProjectModel confirmDeleteModel = new ConfirmDeleteProjectModel();
            confirmDeleteModel.Project = _projectService.GetProject(projectId);

            return PartialView("Partials/ConfirmDeleteProject", confirmDeleteModel);
        }

        [NoCache]
        [Authenticate]
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult DeleteProject(String id)
        {
            return RedirectToAction("Index", "Main");
        }

        [NoCache]
        [Authenticate]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult DeleteProject(String id, ConfirmDeleteProjectModel confirmDeleteProjectModel)
        {
            _projectService.DeleteProject(id);
                
            return RedirectToAction("Index", "Main");
        }

        #endregion
    }
}

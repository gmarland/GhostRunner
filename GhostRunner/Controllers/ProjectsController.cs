using GhostRunner.Models;
using GhostRunner.SL;
using GhostRunner.Utils;
using GhostRunner.ViewModels.Main.Partials;
using GhostRunner.ViewModels.Projects;
using GhostRunner.ViewModels.Projects.Partials;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GhostRunner.Controllers
{
    public class ProjectsController : Controller
    {
        private ProjectService _projectService;

        public ProjectsController()
        {
            _projectService = new ProjectService();
        }
        
        [NoCache]
        [Authenticate]
        public ActionResult Index(String id)
        {
            IndexModel indexModel = new IndexModel();
            
            indexModel.CurrentView = "demos";
            if (!String.IsNullOrEmpty(Request.QueryString["view"])) indexModel.CurrentView = Request.QueryString["view"];

            indexModel.User = ((User)ViewData["User"]);
            indexModel.Project = _projectService.GetProject(id);
            indexModel.Scripts = _projectService.GetAllProjectScripts(indexModel.Project.ID);

            foreach (Script script in indexModel.Scripts)
            {
                if (!indexModel.ScriptTasks.ContainsKey(script.ExternalId)) indexModel.ScriptTasks.Add(script.ExternalId, new List<Task>());
            }

            foreach (Task scriptTask in _projectService.GetAllTasks(indexModel.Project.ID))
            {
                indexModel.ScriptTasks[scriptTask.Script.ExternalId].Add(scriptTask);
            }

            return View(indexModel);
        }

        #region Create a new script
        
        [NoCache]
        [Authenticate]
        public ActionResult GetCreateScriptDialog(String projectId)
        {
            CreateScriptModel createScriptModel = new CreateScriptModel();
            createScriptModel.Project = _projectService.GetProject(projectId);
            createScriptModel.Script = new Script();

            return PartialView("Partials/CreateScript", createScriptModel);
        }
        
        [NoCache]
        [Authenticate]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult InsertNewScript(String id, CreateScriptModel createScriptModel)
        {
            Script script = _projectService.InsertScript(id, createScriptModel.Script.Name, createScriptModel.Script.Description, createScriptModel.Script.Content);

            return RedirectToAction("Index/" + id, "Projects");
        }

        #endregion

        #region Edit a current script

        [NoCache]
        [Authenticate]
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult EditScript(String scriptId)
        {
            EditScriptModel editScriptModel = new EditScriptModel();
            editScriptModel.Script = _projectService.GetScript(scriptId);
            editScriptModel.User = ((User)ViewData["User"]);
            editScriptModel.Project = editScriptModel.Script.Project;

            return PartialView("Partials/EditScript", editScriptModel);
        }

        [NoCache]
        [Authenticate]
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Update(String id)
        {
            return RedirectToAction("Index/" + id, "Scripts");
        }

        [NoCache]
        [Authenticate]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Update(String id, EditScriptModel editScriptModel)
        {
            _projectService.UpdateScript(id, editScriptModel.Script.Name, editScriptModel.Script.Description, editScriptModel.Script.Content);

            Script script = _projectService.GetScript(id);

            return RedirectToAction("Index/" + script.Project.ExternalId, "Projects", new { view = "scripts" });
        }

        #endregion

        #region Delete a script

        [NoCache]
        [Authenticate]
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult ConfirmDeleteScript(String scriptId)
        {
            ConfirmDeleteScriptModel confirmDeleteModel = new ConfirmDeleteScriptModel();
            confirmDeleteModel.Script = _projectService.GetScript(scriptId);

            return PartialView("Partials/ConfirmDeleteScript", confirmDeleteModel);
        }

        [NoCache]
        [Authenticate]
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult DeleteScript(String id)
        {
            Script script = _projectService.GetScript(id);

            if (script != null)
            {
                return RedirectToAction("Index/" + script.Project.ID, "Projects");
            }
            else
            {
                return RedirectToAction("Index", "Main");
            }
        }

        [NoCache]
        [Authenticate]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult DeleteScript(String id, ConfirmDeleteScriptModel confirmDeleteScriptModel)
        {
            Script script = _projectService.GetScript(id);

            if (script != null)
            {
                String projectId = script.Project.ExternalId;

                _projectService.DeleteScript(id);

                return RedirectToAction("Index/" + projectId, "Projects");
            }
            else
            {
                return RedirectToAction("Index", "Main");
            }
        }

        #endregion

        #region Check task status

        [NoCache]
        [Authenticate]
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetScriptTaskStatus(String id)
        {
            Status status = _projectService.GetScriptTaskStatus(id);

            if (status == Status.Unprocessed) return Content(JSONHelper.BuildStatusMessage("success", "unprocessed"), "text/json");
            if (status == Status.Processing) return Content(JSONHelper.BuildStatusMessage("success", "processing"), "text/json");
            else return Content(JSONHelper.BuildStatusMessage("success", "idle"), "text/json");
        }

        #endregion

        #region Create a new script task

        [NoCache]
        [Authenticate]
        public ActionResult GetRunScriptDialog(String scriptId)
        {
            RunScriptModel runScriptModel = new RunScriptModel();
            runScriptModel.User = ((User)ViewData["User"]);
            runScriptModel.Script = _projectService.GetScript(scriptId);
            runScriptModel.Project = runScriptModel.Script.Project;

            runScriptModel.Task = new Task();
            runScriptModel.Task.Name = runScriptModel.Script.Name;

            runScriptModel.TaskParameters = new List<TaskParameter>();

            foreach (String parameter in runScriptModel.Script.GetAllParameters())
            {
                TaskParameter taskParameter = new TaskParameter();
                taskParameter.Name = parameter;
                taskParameter.Value = String.Empty;

                runScriptModel.TaskParameters.Add(taskParameter);
            }

            return PartialView("Partials/RunScript", runScriptModel);
        }

        [NoCache]
        [Authenticate]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult CreateTask(String id, RunScriptModel runScriptModel)
        {
            Script script = _projectService.GetScript(id);

            Task scriptTask = _projectService.InsertTask(((User)ViewData["User"]).ID, id, runScriptModel.Task.Name);

            if (scriptTask != null)
            {
                if (runScriptModel.TaskParameters != null)
                {
                    foreach (TaskParameter scriptTaskParameter in runScriptModel.TaskParameters)
                    {
                        _projectService.InsertTaskParameter(scriptTask.ExternalId, scriptTaskParameter.Name, scriptTaskParameter.Value);
                    }
                }
            }

            return RedirectToAction("Index/" + script.Project.ExternalId, "Projects", new { view = "scripts" });
        }

        #endregion
    }
}

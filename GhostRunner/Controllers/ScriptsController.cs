using GhostRunner.Models;
using GhostRunner.SL;
using GhostRunner.Utils;
using GhostRunner.ViewModels.Main.Partials;
using GhostRunner.ViewModels.Scripts;
using GhostRunner.ViewModels.Scripts.Partials;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GhostRunner.Controllers
{
    public class ScriptsController : Controller
    {
        #region Private Properties

        private ProjectService _projectService;
        private ScriptService _scriptService;
        private TaskService _taskService;

        #endregion

        #region Constructors

        public ScriptsController()
        {
            _projectService = new ProjectService(new GhostRunnerContext(ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString));
            _scriptService = new ScriptService(new GhostRunnerContext(ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString));
            _taskService = new TaskService(new GhostRunnerContext(ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString));
        }

        #endregion

        #region List all sequences
        
        [NoCache]
        [Authenticate]
        public ActionResult Index(String id)
        {
            IndexModel indexModel = new IndexModel();
            
            indexModel.User = ((User)ViewData["User"]);
            indexModel.Project = _projectService.GetProject(id);
            indexModel.Scripts = _scriptService.GetAllProjectGhostRunnerScripts(indexModel.Project.ID);

            return View(indexModel);
        }

        #endregion

        #region Create a new script

        [NoCache]
        [Authenticate]
        public ActionResult GetCreateScriptSelectDialog(String projectId)
        {
            CreateScriptSelectModel createScriptSelectModel = new CreateScriptSelectModel();
            createScriptSelectModel.Project = _projectService.GetProject(projectId);

            return PartialView("Partials/CreateScriptSelect", createScriptSelectModel);
        }

        [NoCache]
        [Authenticate]
        public ActionResult GetCreateScriptDialog(String projectId, String scriptType)
        {
            CreateScriptModel createScriptModel = new CreateScriptModel();
            createScriptModel.Project = _projectService.GetProject(projectId);
            createScriptModel.ScriptType = scriptType;
            createScriptModel.GhostRunnerScript = ScriptHelper.GetNewScriptObject(scriptType);

            return PartialView("Partials/CreateScript", createScriptModel);
        }
        
        [NoCache]
        [Authenticate]
        [ValidateInput(false)]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult InsertNewScript(String id, FormCollection formCollection)
        {
            Script script = null;

            switch (formCollection["Type"].ToString().Trim().ToLower())
            {
                case "git": 
                    Dictionary<String, String> gitAuthentication = new Dictionary<String, String>();
                    gitAuthentication.Add("Location", formCollection["Location"]);
                    gitAuthentication.Add("Username", formCollection["Username"]);
                    gitAuthentication.Add("Password", formCollection["Password"]);

                    script = _scriptService.InsertScript(id, formCollection["Type"], formCollection["GhostRunnerScript.Name"], formCollection["GhostRunnerScript.Description"], JsonConvert.SerializeObject(gitAuthentication, new KeyValuePairConverter()));
                    break;
                default:
                    script = _scriptService.InsertScript(id, formCollection["Type"], formCollection["GhostRunnerScript.Name"], formCollection["GhostRunnerScript.Description"], formCollection["Content"]);
                    break;
            }

            return RedirectToAction("Index/" + id, "Scripts");
        }

        #endregion

        #region Edit a current script

        [NoCache]
        [Authenticate]
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetEditScriptDialog(String scriptId)
        {
            EditScriptModel editScriptModel = new EditScriptModel();
            editScriptModel.GhostRunnerScript = ScriptHelper.GetGhostRunnerScript(_scriptService.GetScript(scriptId));
            editScriptModel.User = ((User)ViewData["User"]);
            editScriptModel.Project = editScriptModel.GhostRunnerScript.Project;

            return PartialView("Partials/EditScript", editScriptModel);
        }

        [NoCache]
        [Authenticate]
        [ValidateInput(false)]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Update(String id, FormCollection formCollection)
        {
            switch (formCollection["Type"].ToString().Trim().ToLower())
            {
                case "git":
                    Dictionary<String, String> gitAuthentication = new Dictionary<String, String>();
                    gitAuthentication.Add("Location", formCollection["Location"]);
                    gitAuthentication.Add("Username", formCollection["Username"]);
                    gitAuthentication.Add("Password", formCollection["Password"]);

                    _scriptService.UpdateScript(id, formCollection["GhostRunnerScript.Name"], formCollection["GhostRunnerScript.Description"], JsonConvert.SerializeObject(gitAuthentication, new KeyValuePairConverter()));
                    break;
                default:
                    _scriptService.UpdateScript(id, formCollection["GhostRunnerScript.Name"], formCollection["GhostRunnerScript.Description"], formCollection["Content"]);
                    break;
            }

            Script script = _scriptService.GetScript(id);

            return RedirectToAction("Index/" + script.Project.ExternalId, "Scripts", new { view = "scripts" });
        }

        #endregion

        #region Delete a script

        [NoCache]
        [Authenticate]
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult ConfirmDeleteScript(String scriptId)
        {
            ConfirmDeleteScriptModel confirmDeleteModel = new ConfirmDeleteScriptModel();
            confirmDeleteModel.Script = _scriptService.GetScript(scriptId);

            return PartialView("Partials/ConfirmDeleteScript", confirmDeleteModel);
        }

        [NoCache]
        [Authenticate]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult DeleteScript(String id, ConfirmDeleteScriptModel confirmDeleteScriptModel)
        {
            Script script = _scriptService.GetScript(id);

            if (script != null)
            {
                String projectId = script.Project.ExternalId;

                _scriptService.DeleteScript(id);

                return RedirectToAction("Index/" + projectId, "Scripts");
            }
            else
            {
                return RedirectToAction("Index", "Main");
            }
        }

        #endregion

        #region Create a new script task

        [NoCache]
        [Authenticate]
        public ActionResult GetRunScriptDialog(String scriptId)
        {
            RunScriptModel runScriptModel = new RunScriptModel();
            runScriptModel.User = ((User)ViewData["User"]);
            runScriptModel.Script = ScriptHelper.GetGhostRunnerScript(_scriptService.GetScript(scriptId));
            runScriptModel.Project = runScriptModel.Script.Project;

            runScriptModel.Task = new Task();
            runScriptModel.Task.Name = runScriptModel.Script.Name;

            runScriptModel.TaskParameters = new List<TaskScriptParameter>();

            foreach (String parameter in runScriptModel.Script.GetAllParameters())
            {
                TaskScriptParameter taskParameter = new TaskScriptParameter();
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
            Script script = _scriptService.GetScript(id);

            Task scriptTask = _taskService.InsertScriptTask(id, runScriptModel.Task.Name, runScriptModel.TaskParameters);

            return RedirectToAction("Index/" + script.Project.ExternalId, "Scripts", new { view = "scripts" });
        }

        #endregion
    }
}

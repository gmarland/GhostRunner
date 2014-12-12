using GhostRunner.Models;
using GhostRunner.SL;
using GhostRunner.Utils;
using GhostRunner.ViewModels.Sequences;
using GhostRunner.ViewModels.Sequences.Partials;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace GhostRunner.Controllers
{
    public class SequencesController : Controller
    {
        #region Private Properties

        private ProjectService _projectService;
        private SequenceService _sequenceService;
        private SequenceScriptService _sequenceScriptService;
        private ScriptService _scriptService;
        private TaskService _taskService;

        #endregion

        #region Constructors

        public SequencesController()
        {
            _projectService = new ProjectService();
            _sequenceService = new SequenceService();
            _sequenceScriptService = new SequenceScriptService();
            _scriptService = new ScriptService();
            _taskService = new TaskService();
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
            indexModel.Sequences = _sequenceService.GetAllSequences(indexModel.Project.ID);

            return View(indexModel);
        }

        #endregion

        #region Create a new sequence

        [NoCache]
        [Authenticate]
        public ActionResult ViewCreateSequenceDialog(String projectId)
        {
            CreateSequenceModel createScriptModel = new CreateSequenceModel();
            createScriptModel.Project = _projectService.GetProject(projectId);
            createScriptModel.Sequence = new Sequence();

            return PartialView("Partials/CreateSequence", createScriptModel);
        }

        [NoCache]
        [Authenticate]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult InsertNewSequence(String id, CreateSequenceModel createSequenceModel)
        {
            Sequence sequence = _sequenceService.InsertSequence(id, createSequenceModel.Sequence.Name, createSequenceModel.Sequence.Description);

            return RedirectToAction("Sequence/" + id + "/" + sequence.ExternalId, "Sequences");
        }

        #endregion

        #region Edit a current sequence

        [NoCache]
        [Authenticate]
        public ActionResult ViewEditSequenceDialog(String sequenceId)
        {
            EditSequenceModel editScriptModel = new EditSequenceModel();
            editScriptModel.Sequence = _sequenceService.GetSequence(sequenceId);

            return PartialView("Partials/EditSequence", editScriptModel);
        }

        [NoCache]
        [Authenticate]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult UpdateSequence(String id, EditSequenceModel editSequenceModel)
        {
            Sequence sequence = _sequenceService.GetSequence(id);

            String projectId = sequence.Project.ExternalId;

            _sequenceService.UpdateSequence(id, editSequenceModel.Sequence.Name, editSequenceModel.Sequence.Description);

            return RedirectToAction("Index/" + projectId, "Sequences");
        }

        #endregion

        #region Create a new sequence task

        [NoCache]
        [Authenticate]
        public ActionResult ViewRunSequenceDialog(String sequenceId)
        {
            RunSequenceModel runSequenceModel = new RunSequenceModel();
            runSequenceModel.Sequence = _sequenceService.GetSequence(sequenceId);

            runSequenceModel.Task = new Task();
            runSequenceModel.Task.Name = runSequenceModel.Sequence.Name;

            return PartialView("Partials/RunSequence", runSequenceModel);
        }

        [NoCache]
        [Authenticate]
        [HttpPost]
        public ActionResult RunSequence(String id, RunSequenceModel runSequenceModel)
        {
            Sequence sequence = _sequenceService.GetSequence(id);

            _taskService.InsertSequenceTask(((User)ViewData["User"]).ID, id, runSequenceModel.Task.Name);

            return RedirectToAction("Index/" + sequence.Project.ExternalId, "Sequences");
        }

        #endregion

        #region Delete a sequence

        [NoCache]
        [Authenticate]
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult ConfirmDeleteSequence(String sequenceId)
        {
            ConfirmDeleteSequenceModel confirmDeleteSequenceModel = new ConfirmDeleteSequenceModel();
            confirmDeleteSequenceModel.Sequence = _sequenceService.GetSequence(sequenceId);

            return PartialView("Partials/ConfirmDeleteSequence", confirmDeleteSequenceModel);
        }

        [NoCache]
        [Authenticate]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult DeleteSequence(String id, ConfirmDeleteSequenceModel confirmDeleteScriptModel)
        {
            Sequence sequence = _sequenceService.GetSequence(id);

            if (sequence != null)
            {
                String projectId = sequence.Project.ExternalId;

                _sequenceService.DeleteSequence(id);

                return RedirectToAction("Index/" + projectId, "Sequences");
            }
            else
            {
                return RedirectToAction("Index", "Main");
            }
        }

        #endregion

        #region View a sequence

        [NoCache]
        [Authenticate]
        public ActionResult Sequence(String projectId, String id)
        {
            SequenceModel sequenceModel = new SequenceModel();

            sequenceModel.User = ((User)ViewData["User"]);
            sequenceModel.Project = _projectService.GetProject(projectId);
            sequenceModel.Sequence = _sequenceService.GetSequence(id);
            sequenceModel.SequenceScripts = _sequenceScriptService.GetAllSequenceScripts(id);
            sequenceModel.Scripts = _scriptService.GetAllProjectScripts(sequenceModel.Project.ID);

            return View(sequenceModel);
        }

        [NoCache]
        [Authenticate]
        public ActionResult ViewScriptDialog(String scriptId)
        {
            ScriptModel ScriptModel = new ScriptModel();
            ScriptModel.Script = _scriptService.GetScript(scriptId);

            return PartialView("Partials/Script", ScriptModel);
        }

        [NoCache]
        [Authenticate]
        public ActionResult ViewSequencedScriptDialog(String sequenceScriptId)
        {
            SequencedScriptModel sequencedScriptModel = new SequencedScriptModel();
            sequencedScriptModel.SequenceScript = _sequenceScriptService.GetSequenceScript(sequenceScriptId);

            return PartialView("Partials/SequencedScript", sequencedScriptModel);
        }

        #endregion

        #region Queue a sequence for running

        [NoCache]
        [Authenticate]
        [HttpGet]
        public ActionResult RunSequencedScript(String id)
        {
            SequenceScript sequenceScript = _sequenceScriptService.GetSequenceScript(id);

            String sequenceId = sequenceScript.Sequence.ExternalId;
            String projectId = sequenceScript.Sequence.Project.ExternalId;

            _taskService.InsertSequenceScriptTask(((User)ViewData["User"]).ID, id);

            return RedirectToAction("Sequence/" + projectId + "/" + sequenceId, "Sequences");
        }

        #endregion

        #region Managing sequence scripts

        [NoCache]
        [Authenticate]
        [HttpPost]
        public ActionResult UpdateSequencedScript(String id, SequencedScriptModel sequencedScript)
        {
            SequenceScript sequenceScript = _sequenceScriptService.GetSequenceScript(id);

            String sequenceId = sequenceScript.Sequence.ExternalId;
            String projectId = sequenceScript.Sequence.Project.ExternalId;

            _sequenceScriptService.UpdateSequenceScript(id, sequencedScript.SequenceScript.Name, sequencedScript.SequenceScript.Content);

            return RedirectToAction("Sequence/" + projectId + "/" + sequenceId, "Sequences");
        }

        [NoCache]
        [Authenticate]
        [HttpPost]
        public ActionResult DeleteSequencedScript(String id)
        {
            SequenceScript sequenceScript = _sequenceScriptService.GetSequenceScript(id);

            String sequenceId = sequenceScript.Sequence.ExternalId;
            String projectId = sequenceScript.Sequence.Project.ExternalId;

            _sequenceScriptService.DeleteSequenceScript(id);

            return RedirectToAction("Sequence/" + projectId + "/" + sequenceId, "Sequences");
        }

        [NoCache]
        [Authenticate]
        public ActionResult GetSequenceScriptParameters(String sequenceId, String scriptId)
        {
            SequenceScriptParametersModel sequenceScriptParametersModel = new SequenceScriptParametersModel();

            sequenceScriptParametersModel.Sequence = _sequenceService.GetSequence(sequenceId);
            sequenceScriptParametersModel.Script = _scriptService.GetScript(scriptId);

            sequenceScriptParametersModel.TaskParameters = new List<TaskScriptParameter>();

            foreach (String parameter in sequenceScriptParametersModel.Script.GetAllParameters())
            {
                TaskScriptParameter taskParameter = new TaskScriptParameter();
                taskParameter.Name = parameter;
                taskParameter.Value = String.Empty;

                sequenceScriptParametersModel.TaskParameters.Add(taskParameter);
            }

            return PartialView("Partials/SequenceScriptParameters", sequenceScriptParametersModel);
        }

        [NoCache]
        [Authenticate]
        public ActionResult InsertSequenceScript(String sequenceId, String scriptId, String name, String sequenceParameters)
        {
            Script script = _scriptService.GetScript(scriptId);

            SequenceScriptsModel sequenceScriptsModel = new SequenceScriptsModel();

            _sequenceService.AddScriptToSequence(sequenceId, scriptId, name, JsonConvert.DeserializeObject<Dictionary<String, String>>(sequenceParameters));

            sequenceScriptsModel.SequenceScripts = _sequenceScriptService.GetAllSequenceScripts(sequenceId);

            return PartialView("Partials/SequenceScripts", sequenceScriptsModel);
        }

        [NoCache]
        [Authenticate]
        [HttpPut]
        public ActionResult UpdateScriptSequences(String sequenceId, String scriptSequence)
        {
            String[] sequenceArray = JsonConvert.DeserializeObject<String[]>(scriptSequence);

            _sequenceService.UpdateScriptOrderInSequence(sequenceId, sequenceArray);

            return Content(JSONHelper.BuildStatusMessage("success"));
        }

        #endregion
    }
}

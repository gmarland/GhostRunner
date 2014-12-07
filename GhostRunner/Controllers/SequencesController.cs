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
        private ProjectService _projectService;

        public SequencesController()
        {
            _projectService = new ProjectService();
        }

        [NoCache]
        [Authenticate]
        public ActionResult Index(String id)
        {
            IndexModel indexModel = new IndexModel();

            indexModel.User = ((User)ViewData["User"]);
            indexModel.Project = _projectService.GetProject(id);
            indexModel.Sequences = _projectService.GetAllProjectSequences(indexModel.Project.ID);

            return View(indexModel);
        }

        [NoCache]
        [Authenticate]
        public ActionResult GetCreateSequenceDialog(String projectId)
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
            Sequence sequence = _projectService.InsertProjectSequence(id, createSequenceModel.Sequence.Name, createSequenceModel.Sequence.Description);

            return RedirectToAction("Sequence/" + id + "/" + sequence.ExternalId, "Sequences");
        }

        [NoCache]
        [Authenticate]
        public ActionResult Sequence(String projectId, String id)
        {
            SequenceModel sequenceModel = new SequenceModel();

            sequenceModel.User = ((User)ViewData["User"]);
            sequenceModel.Project = _projectService.GetProject(projectId);
            sequenceModel.Sequence = _projectService.GetProjectSequence(id);
            sequenceModel.SequenceScripts = _projectService.GetAllProjectSequenceScripts(id);
            sequenceModel.Scripts = _projectService.GetAllProjectScripts(sequenceModel.Project.ID);

            return View(sequenceModel);
        }

        [NoCache]
        [Authenticate]
        public ActionResult AddScriptToSequence(String projectId, String sequenceId, String scriptId)
        {
            return View();
        }

        [NoCache]
        [Authenticate]
        public ActionResult InsertSequenceScript(String sequenceId, String scriptId)
        {
            _projectService.AddScriptToProjectSequence(sequenceId, scriptId);

            SequenceScriptsModel sequenceScriptsModel = new SequenceScriptsModel();
            sequenceScriptsModel.SequenceScripts = _projectService.GetAllProjectSequenceScripts(sequenceId);

            return PartialView("Partials/SequenceScripts", sequenceScriptsModel);
        }

        [NoCache]
        [Authenticate]
        [HttpPut]
        public ActionResult UpdateScriptSequences(String sequenceId, String scriptSequence)
        {
            String[] sequenceArray = JsonConvert.DeserializeObject<String[]>(scriptSequence);

            _projectService.UpdateScriptOrderInProjectSequence(sequenceId, sequenceArray);

            return Content(JSONHelper.BuildStatusMessage("success"));
        }
    }
}

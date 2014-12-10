using GhostRunner.Models;
using GhostRunner.SL;
using GhostRunner.ViewModels.Schedule;
using GhostRunner.ViewModels.Schedule.Partials;
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
        private SequenceService _sequenceService;
        private ScriptService _scriptService;

        #endregion

        #region Constructors

        public ScheduleController()
        {
            _projectService = new ProjectService();
            _sequenceService = new SequenceService();
            _scriptService = new ScriptService();
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

        [NoCache]
        [Authenticate]
        public ActionResult GetAddScheduledItemDialog(String projectId)
        {
            AddScheduledItemModel addScheduledItemModel = new AddScheduledItemModel();
            addScheduledItemModel.Project = _projectService.GetProject(projectId);
            addScheduledItemModel.Sequences = _sequenceService.GetAllSequences(addScheduledItemModel.Project.ID);
            addScheduledItemModel.Scripts = _scriptService.GetAllProjectScripts(addScheduledItemModel.Project.ID);

            return View("Partials/AddScheduledItem", addScheduledItemModel);
        }
    }
}

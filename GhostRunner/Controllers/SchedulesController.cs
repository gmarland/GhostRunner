using GhostRunner.Models;
using GhostRunner.SL;
using GhostRunner.Utils;
using GhostRunner.ViewModels.Schedules;
using GhostRunner.ViewModels.Schedules.Partials;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GhostRunner.Controllers
{
    public class SchedulesController : Controller
    {
        #region Private Properties

        private ProjectService _projectService;
        private ScheduleService _scheduleService;
        private SequenceService _sequenceService;
        private ScriptService _scriptService;

        #endregion

        #region Constructors

        public SchedulesController()
        {
            _projectService = new ProjectService();
            _scheduleService = new ScheduleService();
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
            indexModel.ScheduleItems = _scheduleService.GetAllScheduleItems(id);

            return View(indexModel);
        }

        #region Create a schedule

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

        [NoCache]
        [Authenticate]
        public ActionResult InsertScheduledItem(String projectId, String itemId, String itemType, String scriptParameters, String when, String whenParameters)
        {
            Schedule schedule = _scheduleService.InsertSchedule(projectId, when, itemId, itemType);

            if (schedule != null)
            {
                List<Dictionary<String, String>> jsonScriptParameters = JsonConvert.DeserializeObject<List<Dictionary<String, String>>>(scriptParameters);

                foreach (Dictionary<String, String> scriptParameter in jsonScriptParameters)
                {
                    _scheduleService.InsertScheduleParameter(schedule.ExternalId, scriptParameter["name"], scriptParameter["value"]);
                }

                List<Dictionary<String, String>> jsonWhenParameters = JsonConvert.DeserializeObject<List<Dictionary<String, String>>>(whenParameters);

                foreach (Dictionary<String, String> whenParameter in jsonWhenParameters)
                {
                    _scheduleService.InsertScheduleDetail(schedule.ExternalId, whenParameter["name"], whenParameter["value"]);
                }

                return Content(JSONHelper.BuildStatusMessage("success"));
            }
            else return Content(JSONHelper.BuildStatusMessage("failed", "Unable to create schedule"));
        }

        #endregion

        #region Edit a current schedule

        [NoCache]
        [Authenticate]
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetEditScheduleDialog(String scheduleId)
        {
            EditScheduledItemModel editScheduledItemModel = new EditScheduledItemModel();
            editScheduledItemModel.ScheduleItem = _scheduleService.GetScheduleItem(scheduleId);
            editScheduledItemModel.User = ((User)ViewData["User"]);
            editScheduledItemModel.Project = editScheduledItemModel.ScheduleItem.Project;

            return PartialView("Partials/editScheduledItem", editScheduledItemModel);
        }

        [NoCache]
        [Authenticate]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Update(String id, EditScheduledItemModel editScheduledItemModel)
        {
            Schedule schedule = _scheduleService.GetSchedule(id);

            return RedirectToAction("Index/" + schedule.Project.ExternalId, "Scripts", new { view = "scripts" });
        }

        #endregion

        #region Delete a schedule

        [NoCache]
        [Authenticate]
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult ConfirmDeleteSchedule(String scheduleId)
        {
            ConfirmDeleteScheduleModel confirmDeleteModel = new ConfirmDeleteScheduleModel();
            confirmDeleteModel.Schedule = _scheduleService.GetSchedule(scheduleId);

            return PartialView("Partials/ConfirmDeleteSchedule", confirmDeleteModel);
        }

        [NoCache]
        [Authenticate]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult DeleteSchedule(String id, ConfirmDeleteScheduleModel confirmDeleteScheduleModel)
        {
            Schedule schedule = _scheduleService.GetSchedule(id);

            if (schedule != null)
            {
                String projectId = schedule.Project.ExternalId;

                _scheduleService.DeleteSchedule(id);

                return RedirectToAction("Index/" + projectId, "Schedules");
            }
            else
            {
                return RedirectToAction("Index", "Main");
            }
        }

        #endregion

    }
}

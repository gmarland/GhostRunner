using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GhostRunner.Controllers
{
    public class HistoryController : Controller
    {
        [NoCache]
        [Authenticate]
        public ActionResult Index()
        {
            /*foreach (Task scriptTask in _projectService.GetAllTasks(indexModel.Project.ID))
            {
                indexModel.ScriptTasks[scriptTask.Script.ExternalId].Add(scriptTask);
            }*/

            return View();
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GhostRunner.Models;

namespace GhostRunner.ViewModels.Scripts
{
    public class IndexModel : ViewModel
    {
        public IndexModel()
        {
            Scripts = new List<Script>();
            ScriptTasks = new Dictionary<String, IList<Task>>();
        }

        public Project Project { get; set; }

        public IList<Script> Scripts { get; set; }

        public IDictionary<String, IList<Task>> ScriptTasks { get; set; }

        public Boolean HasUnProcessedTasks(String ScriptId)
        {
            if (ScriptTasks.ContainsKey(ScriptId))
            {
                return ScriptTasks[ScriptId].Where(itp => itp.Status == Status.Unprocessed).Count() > 0;
            }
            else return false;
        }

        public Boolean HasProcessingTasks(String ScriptId)
        {
            if (ScriptTasks.ContainsKey(ScriptId))
            {
                return ScriptTasks[ScriptId].Where(itp => itp.Status == Status.Processing).Count() > 0;
            }
            else return false;
        }
    }
}
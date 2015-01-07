using GhostRunner.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GhostRunner.ViewModels.Scripts.Partials
{
    public class EditScriptModel
    {
        public User User { get; set; }

        public Project Project { get; set; }

        public String ScriptType
        { 
            get
            {
                return GhostRunnerScript.Type.ToString();
            }
        }

        public IGhostRunnerScript GhostRunnerScript { get; set; }
    }
}
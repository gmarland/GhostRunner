using GhostRunner.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GhostRunner.ViewModels.Scripts.Partials
{
    public class CreateScriptModel
    {
        public Project Project { get; set; }

        public String ScriptType { get; set; }

        public IGhostRunnerScript GhostRunnerScript { get; set; }
    }
}
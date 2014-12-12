using GhostRunner.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GhostRunner.ViewModels.Scripts.Partials
{
    public class RunScriptModel
    {
        public Project Project { get; set; }

        public User User { get; set; }

        public Script Script { get; set; }

        public Task Task { get; set; }

        public IList<TaskScriptParameter> TaskParameters { get; set; }
    }
}
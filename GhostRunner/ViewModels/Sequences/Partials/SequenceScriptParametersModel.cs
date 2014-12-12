using GhostRunner.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GhostRunner.ViewModels.Sequences.Partials
{
    public class SequenceScriptParametersModel
    {
        public SequenceScriptParametersModel()
        {
            TaskParameters = new List<TaskScriptParameter>();
        }

        public Sequence Sequence { get; set; }

        public Script Script { get; set; }

        public IList<TaskScriptParameter> TaskParameters { get; set; }
    }
}
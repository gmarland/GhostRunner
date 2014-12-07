using GhostRunner.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GhostRunner.ViewModels.Sequences.Partials
{
    public class SequenceScriptsModel
    {
        public SequenceScriptsModel()
        {
            SequenceScripts = new List<SequenceScript>();
            ScriptTaskParameters = false;
        }

        public IList<SequenceScript> SequenceScripts { get; set; }

        public Boolean ScriptTaskParameters { get; set; }
    }
}
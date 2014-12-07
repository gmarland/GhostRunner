using GhostRunner.Models;
using GhostRunner.ViewModels.Sequences.Partials;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GhostRunner.ViewModels.Sequences
{
    public class SequenceModel : ViewModel
    {
        public SequenceModel()
        {
            SequenceScripts = new List<SequenceScript>();
        }

        public Project Project { get; set; }

        public Sequence Sequence { get; set; }

        public IList<SequenceScript> SequenceScripts { get; set; }

        public IList<Script> Scripts { get; set; }

        public SequenceScriptsModel GetSequenceScriptsModel()
        {
            SequenceScriptsModel sequenceScriptsModel = new SequenceScriptsModel();
            sequenceScriptsModel.SequenceScripts = SequenceScripts;

            return sequenceScriptsModel;
        }
    }
}
using GhostRunner.Models;
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
            Scripts = new List<Script>();
        }

        public Project Project { get; set; }

        public Sequence Sequence { get; set; }

        public IList<Script> Scripts { get; set; }
    }
}
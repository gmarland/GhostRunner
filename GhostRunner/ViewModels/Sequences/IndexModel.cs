using GhostRunner.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GhostRunner.ViewModels.Sequences
{
    public class IndexModel : ViewModel
    {
        public IndexModel()
        {
            Sequences = new List<Sequence>();
        }

        public Project Project { get; set; }

        public IList<Sequence> Sequences { get; set; }
    }
}
using GhostRunner.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GhostRunner.ViewModels.Sequences.Partials
{
    public class CreateSequenceModel
    {
        public Project Project { get; set; }

        public Sequence Sequence { get; set; }
    }
}
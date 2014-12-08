using GhostRunner.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GhostRunner.ViewModels.Sequences.Partials
{
    public class RunSequenceModel
    {
        public Sequence Sequence { get; set; }

        public Task Task { get; set; }
    }
}
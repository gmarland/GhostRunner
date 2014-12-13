using GhostRunner.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GhostRunner.ViewModels.Sequences.Partials
{
    public class SequencedScriptModel
    {
        public Boolean ReadOnly { get; set; }

        public SequenceScript SequenceScript { get; set; }
    }
}
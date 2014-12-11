using GhostRunner.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GhostRunner.ViewModels.Schedules.Partials
{
    public class AddScheduledItemModel
    {
        public AddScheduledItemModel()
        {
            Sequences = new List<Sequence>();
            Scripts = new List<Script>();
        }

        public Project Project { get; set; }

        public IList<Sequence> Sequences { get; set; }

        public IList<Script> Scripts { get; set; }

        public String GetParameterName(String scriptId, String parameterName)
        {
            return scriptId + "_parameter_" + parameterName.Replace(" ", String.Empty);
        }
    }
}
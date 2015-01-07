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
            Scripts = new List<IGhostRunnerScript>();
        }

        public Project Project { get; set; }

        public IList<Sequence> Sequences { get; set; }

        public IList<IGhostRunnerScript> Scripts { get; set; }

        public String GetParameterName(String scriptId, String parameterName)
        {
            return scriptId + "_parameter_" + parameterName.Replace(" ", String.Empty);
        }
    }
}
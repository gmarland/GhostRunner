using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GhostRunner.Models;

namespace GhostRunner.ViewModels.Scripts
{
    public class IndexModel : ViewModel
    {
        public IndexModel()
        {
            Scripts = new List<IGhostRunnerScript>();
        }

        public Project Project { get; set; }

        public IList<IGhostRunnerScript> Scripts { get; set; }
    }
}
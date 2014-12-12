using GhostRunner.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GhostRunner.ViewModels.History
{
    public class IndexModel : ViewModel
    {
        public IndexModel()
        {
            Tasks = new List<Task>();
        }

        public Project Project { get; set; }

        public IList<Task> Tasks { get; set; }
    }
}
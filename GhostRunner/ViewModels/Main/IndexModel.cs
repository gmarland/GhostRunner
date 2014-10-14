using GhostRunner.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GhostRunner.ViewModels.Main
{
    public class IndexModel : ViewModel
    {
        public IndexModel()
        {
            Projects = new List<Project>();
        }

        public IList<Project> Projects { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GhostRunner.Models;

namespace GhostRunner.ViewModels.PackageCaching.Partials
{
    public class PackageCacheViewModel
    {
        public PackageCacheViewModel()
        {
            PackageCache = new List<PackageCache>();
        }

        public IList<PackageCache> PackageCache { get; set; }
    }
}
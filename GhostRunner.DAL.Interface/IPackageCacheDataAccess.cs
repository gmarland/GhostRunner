using GhostRunner.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GhostRunner.DAL.Interface
{
    public interface IPackageCacheDataAccess
    {
        IList<PackageCache> GetAll(String projectId);

        PackageCache Get(String packageCacheId);

        Boolean Update(String packageCacheId, Boolean store);

        Boolean Delete(String packageCacheId);
    }
}

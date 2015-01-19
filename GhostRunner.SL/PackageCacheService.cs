using GhostRunner.DAL;
using GhostRunner.DAL.Interface;
using GhostRunner.Models;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GhostRunner.SL
{
    public class PackageCacheService
    {
        #region Private Properties

        private static readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private IPackageCacheDataAccess _packageCacheDataAccess;

        #endregion

        #region Constructors

        public PackageCacheService(IContext context)
        {
            InitializeDataAccess(context);
        }

        #endregion

        #region Public Methods

        public IList<PackageCache> GetAllProjectPackageCache(String projectId)
        {
            IList<PackageCache> ghostRunnerPackageCache = _packageCacheDataAccess.GetAll(projectId);

            return ghostRunnerPackageCache.OrderBy(pc => pc.Name).ToList();
        }

        public Boolean UpdatePackageCache(String packageCacheId, Boolean store)
        {
            return _packageCacheDataAccess.Update(packageCacheId, store);
        }

        public Boolean DeletePackageCache(String packageCacheId)
        {
            return _packageCacheDataAccess.Delete(packageCacheId);
        }

        #endregion

        #region Private Methods

        private void InitializeDataAccess(IContext context)
        {
            _packageCacheDataAccess = new PackageCacheDataAccess(context);
        }

        #endregion
    }
}

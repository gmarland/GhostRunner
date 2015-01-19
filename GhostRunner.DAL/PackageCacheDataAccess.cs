using GhostRunner.DAL.Interface;
using GhostRunner.Models;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GhostRunner.DAL
{
    public class PackageCacheDataAccess : IPackageCacheDataAccess
    {
        protected IContext _context;

        private static readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public PackageCacheDataAccess(IContext context)
        {
            _context = context;
        }

        public IList<PackageCache> GetAll(String projectId)
        {
            try
            {
                return _context.PackageCaches.Where(s => s.Project.ExternalId == projectId).ToList();
            }
            catch (Exception ex)
            {
                _log.Error("GetAll(" + projectId + "): Error retrieving all packageCache", ex);

                return new List<PackageCache>();
            }
        }

        public Boolean Update(String packageCacheId, bool store)
        {
            PackageCache packageCache = null;

            try
            {
                packageCache = _context.PackageCaches.SingleOrDefault(i => i.ExternalId == packageCacheId);
            }
            catch (Exception ex)
            {
                _log.Error("Update(" + packageCacheId + "): Unable to get package cache", ex);

                return false;
            }

            if (packageCache != null)
            {
                packageCache.Store = store;

                try
                {
                    Save();

                    return true;
                }
                catch (Exception ex)
                {
                    _log.Error("Update(" + packageCacheId + "): Unable to save package cache", ex);

                    return false;
                }
            }
            else
            {
                _log.Error("Update(" + packageCacheId + "): Unable to get find package cache");

                return false;
            }
        }

        public Boolean Delete(String packageCacheId)
        {
            PackageCache packageCache = null;

            try
            {
                packageCache = _context.PackageCaches.SingleOrDefault(i => i.ExternalId == packageCacheId);
            }
            catch (Exception ex)
            {
                _log.Error("Delete(" + packageCacheId + "): Unable to get package cache", ex);

                return false;
            }

            if (packageCache != null)
            {
                _context.PackageCaches.Remove(packageCache);

                try
                {
                    Save();

                    return true;
                }
                catch (Exception ex)
                {
                    _log.Error("Delete(): An error occured deleting the package cache", ex);

                    return false;
                }
            }
            else
            {
                _log.Info("Unable to find package cache");

                return false;
            }
        }

        private void Save()
        {
            try
            {
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

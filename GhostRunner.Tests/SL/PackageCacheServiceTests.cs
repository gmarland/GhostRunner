using GhostRunner.Models;
using GhostRunner.SL;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GhostRunner.Tests.SL
{
    [TestClass]
    public class PackageCacheServiceTests
    {
        private PackageCacheService _packageCacheService;

        [TestInitialize]
        public void Initialize()
        {
            _packageCacheService = new PackageCacheService(new TestContext());
        }

        [TestMethod]
        public void GetAllProjectPackageCache()
        {
            IList<PackageCache> ghostRunnerPackageCache = _packageCacheService.GetAllProjectPackageCache("d4708c0d-721e-426e-b49e-35990687db22");

            Assert.AreEqual(2, ghostRunnerPackageCache.Count);
        }

        [TestMethod]
        public void UpdatePackageCache()
        {
            IList<PackageCache> ghostRunnerPackageCache = _packageCacheService.GetAllProjectPackageCache("d4708c0d-721e-426e-b49e-35990687db22");

            Assert.AreEqual(2, ghostRunnerPackageCache.Count);

            Assert.IsTrue(ghostRunnerPackageCache[0].Store);

            _packageCacheService.UpdatePackageCache(ghostRunnerPackageCache[0].ExternalId, false);

            IList<PackageCache> updatedGhostRunnerPackageCache = _packageCacheService.GetAllProjectPackageCache("d4708c0d-721e-426e-b49e-35990687db22");

            Assert.AreEqual(2, updatedGhostRunnerPackageCache.Count);

            Assert.IsFalse(updatedGhostRunnerPackageCache[0].Store);
        }

        [TestMethod]
        public void DeletePackageCache()
        {
            IList<PackageCache> ghostRunnerPackageCache = _packageCacheService.GetAllProjectPackageCache("d4708c0d-721e-426e-b49e-35990687db22");

            Assert.AreEqual(2, ghostRunnerPackageCache.Count);

            _packageCacheService.DeletePackageCache(ghostRunnerPackageCache[0].ExternalId);

            IList<PackageCache> updatedGhostRunnerPackageCache = _packageCacheService.GetAllProjectPackageCache("d4708c0d-721e-426e-b49e-35990687db22");

            Assert.AreEqual(1, updatedGhostRunnerPackageCache.Count);
        }
    }
}

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
    public class ScheduleServiceTests
    {
        private ScheduleService _scheduleService;

        [TestInitialize]
        public void Initialize()
        {
            _scheduleService = new ScheduleService(new TestContext());
        }

        [TestMethod]
        public void GetAllScheduleItems()
        {
            IList<IScheduleItem> schedules = _scheduleService.GetAllScheduleItems("d4708c0d-721e-426e-b49e-35990687db22");
            Assert.AreEqual(3, schedules.Count);

            IList<IScheduleItem> failingSchedules = _scheduleService.GetAllScheduleItems("99");
            Assert.AreEqual(0, failingSchedules.Count);
        }

        [TestMethod]
        public void GetScheduleItem()
        {
            IScheduleItem scheduleItem =  _scheduleService.GetScheduleItem("2826018c-69cc-4ead-946b-70bb5a47ab02");
            Assert.IsNotNull(scheduleItem);
            Assert.AreEqual(scheduleItem.ScheduleType, ScheduleType.Daily);
            Assert.AreEqual(scheduleItem.Type, ItemType.Script);

            IScheduleItem failingScheduleItem = _scheduleService.GetScheduleItem("99");
            Assert.IsNull(failingScheduleItem);
        }

        [TestMethod]
        public void GetSchedule()
        {
        }

        [TestMethod]
        public void InsertSchedule()
        {
        }

        [TestMethod]
        public void UpdateSchedule()
        {
        }

        [TestMethod]
        public void DeleteSchedule()
        {
        }

        [TestMethod]
        public void InsertScheduleParameter()
        {
        }

        [TestMethod]
        public void InsertScheduleDetail()
        {
        }

        [TestMethod]
        public void DeleteScheduleDetails()
        {
        }
    }
}

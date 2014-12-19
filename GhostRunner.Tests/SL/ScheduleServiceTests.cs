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
            Schedule schedule = _scheduleService.GetSchedule("2826018c-69cc-4ead-946b-70bb5a47ab02");
            Assert.IsNotNull(schedule);
            Assert.AreEqual(schedule.ScheduleType, ScheduleType.Daily);
            Assert.AreEqual(schedule.ScheduleItemType, ItemType.Script);

            Schedule failingSchedule = _scheduleService.GetSchedule("99");
            Assert.IsNull(failingSchedule);
        }

        [TestMethod]
        public void InsertSchedule()
        {
            IList<IScheduleItem> beforeSchedules = _scheduleService.GetAllScheduleItems("d4708c0d-721e-426e-b49e-35990687db22");
            Assert.AreEqual(3, beforeSchedules.Count);

            Schedule newSchedule = _scheduleService.InsertSchedule("d4708c0d-721e-426e-b49e-35990687db22", "daily", "8ebb4cd0-8e36-4778-9b0d-5ba86d9c0cce", "script");
            Assert.IsNotNull(newSchedule);

            IList<IScheduleItem> afterSchedules = _scheduleService.GetAllScheduleItems("d4708c0d-721e-426e-b49e-35990687db22");
            Assert.AreEqual(4, afterSchedules.Count);

            Schedule failingSchedule = _scheduleService.InsertSchedule("99", "daily", "8ebb4cd0-8e36-4778-9b0d-5ba86d9c0cce", "script");
            Assert.IsNull(failingSchedule);

            IList<IScheduleItem> afterFailingSchedules = _scheduleService.GetAllScheduleItems("d4708c0d-721e-426e-b49e-35990687db22");
            Assert.AreEqual(4, afterFailingSchedules.Count);
        }

        [TestMethod]
        public void UpdateSchedule()
        {
            Schedule beforeSchedule = _scheduleService.GetSchedule("2826018c-69cc-4ead-946b-70bb5a47ab02");
            Assert.IsNotNull(beforeSchedule);
            Assert.AreEqual(ScheduleType.Daily, beforeSchedule.ScheduleType);

            Boolean updateSuccessful = _scheduleService.UpdateSchedule("2826018c-69cc-4ead-946b-70bb5a47ab02", "weekly");
            Assert.IsTrue(updateSuccessful);

            Schedule afterSchedule = _scheduleService.GetSchedule("2826018c-69cc-4ead-946b-70bb5a47ab02");
            Assert.IsNotNull(afterSchedule);
            Assert.AreEqual(ScheduleType.Weekly, afterSchedule.ScheduleType);
        }

        [TestMethod]
        public void DeleteSchedule()
        {
            Schedule beforeSchedule = _scheduleService.GetSchedule("2826018c-69cc-4ead-946b-70bb5a47ab02");
            Assert.IsNotNull(beforeSchedule);
            Assert.AreEqual(beforeSchedule.ScheduleType, ScheduleType.Daily);
            Assert.AreEqual(beforeSchedule.ScheduleItemType, ItemType.Script);

            _scheduleService.DeleteSchedule("2826018c-69cc-4ead-946b-70bb5a47ab02");

            Schedule failingSchedule = _scheduleService.GetSchedule("2826018c-69cc-4ead-946b-70bb5a47ab02");
            Assert.IsNull(failingSchedule);
        }

        [TestMethod]
        public void InsertScheduleParameter()
        {
            ScheduleParameter beforeScheduleParameter = _scheduleService.InsertScheduleParameter("2826018c-69cc-4ead-946b-70bb5a47ab02", "test1", "value1");
            Assert.IsNotNull(beforeScheduleParameter);

            ScheduleParameter failingScheduleParameter = _scheduleService.InsertScheduleParameter("99", "test2", "value2");
            Assert.IsNull(failingScheduleParameter);
        }

        [TestMethod]
        public void InsertScheduleDetail()
        {
            ScheduleDetail beforeScheduleDetail = _scheduleService.InsertScheduleDetail("2826018c-69cc-4ead-946b-70bb5a47ab02", "test1", "value1");
            Assert.IsNotNull(beforeScheduleDetail);

            ScheduleDetail failingScheduleDetail = _scheduleService.InsertScheduleDetail("99", "test2", "value2");
            Assert.IsNull(failingScheduleDetail);
        }

        [TestMethod]
        public void DeleteScheduleDetails()
        {
            Boolean deleteSuccessfull = _scheduleService.DeleteScheduleDetails("2826018c-69cc-4ead-946b-70bb5a47ab02");
            Assert.IsTrue(deleteSuccessfull);

            Boolean failingDelete = _scheduleService.DeleteScheduleDetails("99");
            Assert.IsFalse(failingDelete);
        }
    }
}

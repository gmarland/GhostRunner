using GhostRunner.Models;
using GhostRunner.SL;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GhostRunner.Tests.SL
{
    [TestClass]
    public class TaskServiceTests
    {
        private TaskService _taskService;

        [TestInitialize]
        public void Initialize()
        {
            _taskService = new TaskService(new TestContext());
        }

        #region Project Task Methods

        [TestMethod]
        public void GetAllTasks()
        {
            IList<Task> project1Tasks = _taskService.GetAllTasks(1);
            Assert.AreEqual(2, project1Tasks.Count);

            IList<Task> project99Tasks = _taskService.GetAllTasks(99);
            Assert.AreEqual(0, project99Tasks.Count);
        }

        [TestMethod]
        public void InsertScriptTask()
        {
            IList<Task> project1TasksBefore = _taskService.GetAllTasks(1);
            Assert.AreEqual(2, project1TasksBefore.Count);

            List<TaskParameter> taskParameters = new List<TaskParameter>();
            TaskParameter taskParameter = new TaskParameter();
            taskParameter.Name = "parameter1";
            taskParameter.Value = "selected parameter";
            taskParameters.Add(taskParameter);

            Task successfullTask = _taskService.InsertScriptTask(1, "5a768553-052e-47ee-bf48-68f8aaf9cd05", "new task", taskParameters);
            Assert.IsNotNull(successfullTask);
            Assert.AreEqual("new task", successfullTask.Name);

            IList<Task> project1TasksAfter = _taskService.GetAllTasks(1);
            Assert.AreEqual(3, project1TasksAfter.Count);

            Task failingTask1 = _taskService.InsertScriptTask(99, "5a768553-052e-47ee-bf48-68f8aaf9cd05", "new task", new List<TaskParameter>());
            Assert.IsNull(failingTask1);

            IList<Task> project1TasksAfterFailing1 = _taskService.GetAllTasks(1);
            Assert.AreEqual(3, project1TasksAfterFailing1.Count);

            Task failingTask2 = _taskService.InsertScriptTask(1, "99", "new task", new List<TaskParameter>());
            Assert.IsNull(failingTask2);

            IList<Task> project1TasksAfterFailing2 = _taskService.GetAllTasks(1);
            Assert.AreEqual(3, project1TasksAfterFailing2.Count);
        }

        [TestMethod]
        public void InsertSequenceTask()
        {
            Task successfullTask = _taskService.InsertSequenceTask(1, "c2f5f76a-1ee7-4f92-9150-55de4cefa76f", "new sequence task");
            Assert.IsNotNull(successfullTask);
            Assert.AreEqual("new sequence task", successfullTask.Name);

            Task failingTask1 = _taskService.InsertSequenceTask(99, "c2f5f76a-1ee7-4f92-9150-55de4cefa76f", "new sequence task");
            Assert.IsNull(failingTask1);

            Task failingTask2 = _taskService.InsertSequenceTask(1, "99", "new sequence task");
            Assert.IsNull(failingTask2);
        }

        [TestMethod]
        public void InsertSequenceScriptTask()
        {
            Task successfullTask = _taskService.InsertSequenceScriptTask(1, "0cec8cba-3249-44e6-96bb-ff49ac31cdde");
            Assert.IsNotNull(successfullTask);
            Assert.AreEqual("Test Sequence Script 1", successfullTask.Name);

            Task failingTask1 = _taskService.InsertSequenceScriptTask(99, "0cec8cba-3249-44e6-96bb-ff49ac31cdde");
            Assert.IsNull(failingTask1);

            Task failingTask2 = _taskService.InsertSequenceScriptTask(1, "99");
            Assert.IsNull(failingTask2);
        }

        [TestMethod]
        public void InsertTaskParameter()
        {
            TaskParameter newTaskParameter = _taskService.InsertTaskParameter("352e3cf8-480b-4568-80b5-d0cba95dae04", "new pameter", "new value");
            Assert.IsNotNull(newTaskParameter);
            Assert.AreEqual("new pameter", newTaskParameter.Name);
            Assert.AreEqual("new value", newTaskParameter.Value);

            TaskParameter failingTaskParameter = _taskService.InsertTaskParameter("99", "new pameter", "new value");
            Assert.IsNull(failingTaskParameter);
        }

        #endregion
    }
}

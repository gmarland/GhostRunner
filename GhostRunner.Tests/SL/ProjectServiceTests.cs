using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GhostRunner.SL;
using GhostRunner.Models;
using System.Collections.Generic;

namespace GhostRunner.Tests.SL
{
    [TestClass]
    public class ProjectServiceTests
    {
        ProjectService _projectService;

        [TestInitialize]
        public void Initialize()
        {
            _projectService = new ProjectService(new TestContext());
        }

        #region Project Methods

        [TestMethod]
        public void GetAllProjects()
        {
            IList<Project> user1Projects = _projectService.GetAllProjects(1);
            Assert.AreEqual(2, user1Projects.Count);

            IList<Project> user2Projects = _projectService.GetAllProjects(2);
            Assert.AreEqual(1, user2Projects.Count);

            IList<Project> user99Projects = _projectService.GetAllProjects(99);
            Assert.AreEqual(0, user99Projects.Count);
        }

        [TestMethod]
        public void GetProject()
        {
            Project project1 = _projectService.GetProject(1);
            Assert.IsNotNull(project1);
            Assert.AreEqual(1, project1.ID);

            Project project99 = _projectService.GetProject(99);
            Assert.IsNull(project99);
        }
        
        [TestMethod]
        public void InsertProject()
        {
            IList<Project> user1ProjectsBefore = _projectService.GetAllProjects(1);
            Assert.AreEqual(2, user1ProjectsBefore.Count);

            Project newProject = _projectService.InsertProject(1, "New Test Project");
            Assert.IsNotNull(newProject);
            Assert.AreEqual("New Test Project", newProject.Name);

            IList<Project> user1ProjectsAfter = _projectService.GetAllProjects(1);
            Assert.AreEqual(3, user1ProjectsAfter.Count);

            Project newProject99 = _projectService.InsertProject(99, "New Test Project");
            Assert.IsNull(newProject99);
        }

        [TestMethod]
        public void UpdateProject()
        {
            Project project1 = _projectService.GetProject(1);
            Assert.IsNotNull(project1);
            Assert.AreEqual("Test Project 1", project1.Name);

            Boolean updateSuccessful = _projectService.UpdateProject(project1.ExternalId, "New Project Name");
            Assert.IsTrue(updateSuccessful);

            Project updatedProject1 = _projectService.GetProject(1);
            Assert.IsNotNull(updatedProject1);
            Assert.AreEqual("New Project Name", updatedProject1.Name);
        }

        [TestMethod]
        public void DeleteteProject()
        {
            Boolean deleteSuccessful = _projectService.DeleteProject("d4708c0d-721e-426e-b49e-35990687db22");
            Assert.IsTrue(deleteSuccessful);

            Project project1 = _projectService.GetProject(1);
            Assert.IsNull(project1);
        }

        #endregion

        #region Project Sequence Methods

        [TestMethod]
        public void GetAllSequences()
        {
            IList<Sequence> project1Sequences = _projectService.GetAllSequences(1);
            Assert.AreEqual(2, project1Sequences.Count);

            IList<Sequence> project99Sequences = _projectService.GetAllSequences(99);
            Assert.AreEqual(0, project99Sequences.Count);
        }

        [TestMethod]
        public void GetSequence()
        {
            Sequence sequence1 = _projectService.GetSequence("c2f5f76a-1ee7-4f92-9150-55de4cefa76f");
            Assert.IsNotNull(sequence1);

            Sequence sequence99 = _projectService.GetSequence("99");
            Assert.IsNull(sequence99);
        }

        [TestMethod]
        public void InsertSequence()
        {
            IList<Sequence> projectSequencesBefore = _projectService.GetAllSequences(1);
            Assert.AreEqual(2, projectSequencesBefore.Count);

            Sequence newSequence = _projectService.InsertSequence("d4708c0d-721e-426e-b49e-35990687db22", "New test sequence", "Description of new test sequence");
            Assert.IsNotNull(newSequence);
            Assert.AreEqual("New test sequence", newSequence.Name);
            Assert.AreEqual("Description of new test sequence", newSequence.Description);

            IList<Sequence> projectSequencesAfter = _projectService.GetAllSequences(1);
            Assert.AreEqual(3, projectSequencesAfter.Count);

            Sequence failingSequence = _projectService.InsertSequence("99", "Failing test sequence", "Description of failing test sequence");
            Assert.IsNull(failingSequence);

            IList<Sequence> projectSequencesAfterFailing = _projectService.GetAllSequences(1);
            Assert.AreEqual(3, projectSequencesAfterFailing.Count);
        }

        [TestMethod]
        public void GetAllSequencescripts()
        {
            IList<SequenceScript> sequenceScripts = _projectService.GetAllSequenceScripts("c2f5f76a-1ee7-4f92-9150-55de4cefa76f");
            Assert.AreEqual(2, sequenceScripts.Count);

            IList<SequenceScript> failingSequenceScripts = _projectService.GetAllSequenceScripts("99");
            Assert.AreEqual(0, failingSequenceScripts.Count);
        }

        [TestMethod]
        public void AddScriptToSequence()
        {
            IList<SequenceScript> sequenceScriptsBefore = _projectService.GetAllSequenceScripts("c2f5f76a-1ee7-4f92-9150-55de4cefa76f");
            Assert.AreEqual(2, sequenceScriptsBefore.Count);

            SequenceScript sequenceScript = _projectService.AddScriptToSequence("c2f5f76a-1ee7-4f92-9150-55de4cefa76f", "8ebb4cd0-8e36-4778-9b0d-5ba86d9c0cce", "Test Script 1", new Dictionary<String,String>());
            Assert.IsNotNull(sequenceScript);

            IList<SequenceScript> sequenceScriptsAfter = _projectService.GetAllSequenceScripts("c2f5f76a-1ee7-4f92-9150-55de4cefa76f");
            Assert.AreEqual(3, sequenceScriptsAfter.Count);

            SequenceScript failingSequenceScript = _projectService.AddScriptToSequence("99", "8ebb4cd0-8e36-4778-9b0d-5ba86d9c0cce", "Test Script 99", new Dictionary<String, String>());
            Assert.IsNull(failingSequenceScript);

            IList<SequenceScript> sequenceScriptsAfterFailing = _projectService.GetAllSequenceScripts("c2f5f76a-1ee7-4f92-9150-55de4cefa76f");
            Assert.AreEqual(3, sequenceScriptsAfterFailing.Count);

            SequenceScript failingAgainSequenceScript = _projectService.AddScriptToSequence("c2f5f76a-1ee7-4f92-9150-55de4cefa76f", "99", "Test Script 99", new Dictionary<String, String>());
            Assert.IsNull(failingAgainSequenceScript);

            IList<SequenceScript> sequenceScriptsAfterFailingAgain = _projectService.GetAllSequenceScripts("c2f5f76a-1ee7-4f92-9150-55de4cefa76f");
            Assert.AreEqual(3, sequenceScriptsAfterFailingAgain.Count);
        }

        [TestMethod]
        public void UpdateScriptOrderInSequence()
        {
            /*IList<SequenceScript> sequenceScriptsBefore = _projectService.GetAllSequenceScripts("c2f5f76a-1ee7-4f92-9150-55de4cefa76f");
            Assert.AreEqual(2, sequenceScriptsBefore.Count);

            foreach (SequenceScript sequenceScript in sequenceScriptsBefore)
            {
                if (sequenceScript.Par != "5a768553-052e-47ee-bf48-68f8aaf9cd05") Assert.AreEqual(2, sequenceScript.Position);
                else Assert.AreEqual(1, sequenceScript.Position);
            }

            Boolean positionUpdated = _projectService.UpdateScriptOrderInSequence("c2f5f76a-1ee7-4f92-9150-55de4cefa76f", "5a768553-052e-47ee-bf48-68f8aaf9cd05", 2);
            Assert.IsTrue(positionUpdated);

            IList<SequenceScript> sequenceScriptsAfter = _projectService.GetAllSequenceScripts("c2f5f76a-1ee7-4f92-9150-55de4cefa76f");
            Assert.AreEqual(2, sequenceScriptsAfter.Count);*/
        }

        [TestMethod]
        public void RemoveScriptFromSequence()
        {
            IList<SequenceScript> sequenceScriptsBefore = _projectService.GetAllSequenceScripts("c2f5f76a-1ee7-4f92-9150-55de4cefa76f");
            Assert.AreEqual(2, sequenceScriptsBefore.Count);

            Boolean scriptDeleted = _projectService.RemoveScriptFromSequence("c2f5f76a-1ee7-4f92-9150-55de4cefa76f", "0cec8cba-3249-44e6-96bb-ff49ac31cdde");
            Assert.IsTrue(scriptDeleted);

            IList<SequenceScript> sequenceScriptsAfter = _projectService.GetAllSequenceScripts("c2f5f76a-1ee7-4f92-9150-55de4cefa76f");
            Assert.AreEqual(1, sequenceScriptsAfter.Count);
        }

        #endregion

        #region Project Script Methods

        [TestMethod]
        public void GetAllProjectScripts()
        {
            IList<Script> project1Scripts = _projectService.GetAllProjectScripts(1);
            Assert.AreEqual(2, project1Scripts.Count);

            IList<Script> project99Scripts = _projectService.GetAllProjectScripts(99);
            Assert.AreEqual(0, project99Scripts.Count);
        }

        [TestMethod]
        public void GetScript()
        {
            Script script1 = _projectService.GetScript("5a768553-052e-47ee-bf48-68f8aaf9cd05");
            Assert.IsNotNull(script1);
            Assert.AreEqual(1, script1.ID);
            Assert.AreEqual("Test Script 1", script1.Name);

            Script script99 = _projectService.GetScript("99");
            Assert.IsNull(script99);
        }

        [TestMethod]
        public void GetScriptTaskStatus()
        {
            Status script1Status = _projectService.GetScriptTaskStatus("5a768553-052e-47ee-bf48-68f8aaf9cd05");
            Assert.IsNotNull(script1Status);
            Assert.AreEqual(Status.Unprocessed, script1Status);

            Status script99Status = _projectService.GetScriptTaskStatus("99");
            Assert.IsNotNull(script99Status);
            Assert.AreEqual(Status.Unknown, script99Status);
        }

        [TestMethod]
        public void InsertScript()
        {
            IList<Script> project1ScriptsBefore = _projectService.GetAllProjectScripts(1);
            Assert.AreEqual(2, project1ScriptsBefore.Count);

            Script newScript = _projectService.InsertScript("d4708c0d-721e-426e-b49e-35990687db22", "New Test Script", "New Test Script Desc", "Script Content");
            Assert.IsNotNull(newScript);
            Assert.AreEqual("New Test Script", newScript.Name);
            Assert.AreEqual("New Test Script Desc", newScript.Description);
            Assert.AreEqual("Script Content", newScript.Content);

            IList<Script> project1ScriptsAfter = _projectService.GetAllProjectScripts(1);
            Assert.AreEqual(3, project1ScriptsAfter.Count);

            Script newScript99 = _projectService.InsertScript("99", "New Test Script", "New Test Script Desc", "Script Content");
            Assert.IsNull(newScript99);
        }

        [TestMethod]
        public void UpdateScript()
        {
            Script script1 = _projectService.GetScript("5a768553-052e-47ee-bf48-68f8aaf9cd05");
            Assert.IsNotNull(script1);
            Assert.AreEqual("Test Script 1", script1.Name);
            Assert.AreEqual("Script used for testing", script1.Description);
            Assert.AreEqual("Test script with [parameter1]", script1.Content);

            Boolean updateSuccessful = _projectService.UpdateScript("5a768553-052e-47ee-bf48-68f8aaf9cd05", "new name", "new description", "new content");
            Assert.IsTrue(updateSuccessful);

            Script updatedScript1 = _projectService.GetScript("5a768553-052e-47ee-bf48-68f8aaf9cd05");
            Assert.IsNotNull(updatedScript1);
            Assert.AreEqual("new name", updatedScript1.Name);
            Assert.AreEqual("new description", updatedScript1.Description);
            Assert.AreEqual("new content", updatedScript1.Content);

            Boolean updateFailing = _projectService.UpdateScript("99", "new name", "new description", "new content");
            Assert.IsFalse(updateFailing);
        }

        [TestMethod]
        public void DeleteScript()
        {
            IList<Script> project1ScriptsBefore = _projectService.GetAllProjectScripts(1);
            Assert.AreEqual(2, project1ScriptsBefore.Count);

            Boolean updateSuccessfull = _projectService.DeleteScript("5a768553-052e-47ee-bf48-68f8aaf9cd05");
            Assert.IsTrue(updateSuccessfull);

            IList<Script> project1ScriptsAfter = _projectService.GetAllProjectScripts(1);
            Assert.AreEqual(1, project1ScriptsAfter.Count);

            Boolean updateFailed = _projectService.DeleteScript("99");
            Assert.IsFalse(updateFailed);
        }

        #endregion

        #region Project Task Methods

        [TestMethod]
        public void GetAllTasks()
        {
            IList<Task> project1Tasks = _projectService.GetAllTasks(1);
            Assert.AreEqual(2, project1Tasks.Count);

            IList<Task> project99Tasks = _projectService.GetAllTasks(99);
            Assert.AreEqual(0, project99Tasks.Count);
        }

        [TestMethod]
        public void InsertScriptTask()
        {
            IList<Task> project1TasksBefore = _projectService.GetAllTasks(1);
            Assert.AreEqual(2, project1TasksBefore.Count);

            Task successfullTask = _projectService.InsertScriptTask(1, "5a768553-052e-47ee-bf48-68f8aaf9cd05", "new task", new List<TaskParameter>());
            Assert.IsNotNull(successfullTask);
            Assert.AreEqual("new task", successfullTask.Name);

            IList<Task> project1TasksAfter = _projectService.GetAllTasks(1);
            Assert.AreEqual(3, project1TasksAfter.Count);

            Task failingTask1 = _projectService.InsertScriptTask(99, "5a768553-052e-47ee-bf48-68f8aaf9cd05", "new task", new List<TaskParameter>());
            Assert.IsNull(failingTask1);

            IList<Task> project1TasksAfterFailing1 = _projectService.GetAllTasks(1);
            Assert.AreEqual(3, project1TasksAfterFailing1.Count);

            Task failingTask2 = _projectService.InsertScriptTask(1, "99", "new task", new List<TaskParameter>());
            Assert.IsNull(failingTask2);

            IList<Task> project1TasksAfterFailing2 = _projectService.GetAllTasks(1);
            Assert.AreEqual(3, project1TasksAfterFailing2.Count);
        }

        [TestMethod]
        public void InsertScriptTaskParameter()
        {
            TaskParameter newTaskParameter = _projectService.InsertTaskParameter("352e3cf8-480b-4568-80b5-d0cba95dae04", "new pameter", "new value");
            Assert.IsNotNull(newTaskParameter);
            Assert.AreEqual("new pameter", newTaskParameter.Name);
            Assert.AreEqual("new value", newTaskParameter.Value);

            TaskParameter failingTaskParameter = _projectService.InsertTaskParameter("99", "new pameter", "new value");
            Assert.IsNull(failingTaskParameter);
        }

        #endregion
    }
}

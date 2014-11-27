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
    }
}

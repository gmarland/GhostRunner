using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GhostRunner.SL;
using GhostRunner.Models;
using System.Collections.Generic;

namespace GhostRunner.Tests.SL
{
    [TestClass]
    public class ScriptServiceTests
    {
        private ScriptService _scriptService;

        [TestInitialize]
        public void Initialize()
        {
            _scriptService = new ScriptService(new TestContext());
        }

        #region Project Script Methods

        [TestMethod]
        public void GetAllProjectScripts()
        {
            IList<IGhostRunnerScript> project1Scripts = _scriptService.GetAllProjectGhostRunnerScripts(1);
            Assert.AreEqual(2, project1Scripts.Count);

            IList<IGhostRunnerScript> project99Scripts = _scriptService.GetAllProjectGhostRunnerScripts(99);
            Assert.AreEqual(0, project99Scripts.Count);
        }

        [TestMethod]
        public void GetScript()
        {
            Script script1 = _scriptService.GetScript("5a768553-052e-47ee-bf48-68f8aaf9cd05");
            Assert.IsNotNull(script1);
            Assert.AreEqual(1, script1.ID);
            Assert.AreEqual("Test Script 1", script1.Name);

            Script script99 = _scriptService.GetScript("99");
            Assert.IsNull(script99);
        }

        [TestMethod]
        public void GetScriptTaskStatus()
        {
            Status script1Status = _scriptService.GetScriptTaskStatus("5a768553-052e-47ee-bf48-68f8aaf9cd05");
            Assert.IsNotNull(script1Status);
            Assert.AreEqual(Status.Unprocessed, script1Status);

            Status script99Status = _scriptService.GetScriptTaskStatus("99");
            Assert.IsNotNull(script99Status);
            Assert.AreEqual(Status.Unknown, script99Status);
        }

        [TestMethod]
        public void InsertScript()
        {
            IList<IGhostRunnerScript> project1ScriptsBefore = _scriptService.GetAllProjectGhostRunnerScripts(1);
            Assert.AreEqual(2, project1ScriptsBefore.Count);

            Script newScript = _scriptService.InsertScript("d4708c0d-721e-426e-b49e-35990687db22", "Node", "New Test Script", "New Test Script Desc", "Script Content");
            Assert.IsNotNull(newScript);
            Assert.AreEqual("New Test Script", newScript.Name);
            Assert.AreEqual("New Test Script Desc", newScript.Description);
            Assert.AreEqual("Script Content", newScript.Content);

            IList<IGhostRunnerScript> project1ScriptsAfter = _scriptService.GetAllProjectGhostRunnerScripts(1);
            Assert.AreEqual(3, project1ScriptsAfter.Count);

            Script newScript99 = _scriptService.InsertScript("99", "Node", "New Test Script", "New Test Script Desc", "Script Content");
            Assert.IsNull(newScript99);
        }

        [TestMethod]
        public void UpdateScript()
        {
            Script script1 = _scriptService.GetScript("5a768553-052e-47ee-bf48-68f8aaf9cd05");
            Assert.IsNotNull(script1);
            Assert.AreEqual("Test Script 1", script1.Name);
            Assert.AreEqual("Script used for testing", script1.Description);
            Assert.AreEqual("Test script with [parameter1]", script1.Content);

            Boolean updateSuccessful = _scriptService.UpdateScript("5a768553-052e-47ee-bf48-68f8aaf9cd05", "new name", "new description", "new content");
            Assert.IsTrue(updateSuccessful);

            Script updatedScript1 = _scriptService.GetScript("5a768553-052e-47ee-bf48-68f8aaf9cd05");
            Assert.IsNotNull(updatedScript1);
            Assert.AreEqual("new name", updatedScript1.Name);
            Assert.AreEqual("new description", updatedScript1.Description);
            Assert.AreEqual("new content", updatedScript1.Content);

            Boolean updateFailing = _scriptService.UpdateScript("99", "new name", "new description", "new content");
            Assert.IsFalse(updateFailing);
        }

        [TestMethod]
        public void DeleteScript()
        {
            IList<IGhostRunnerScript> project1ScriptsBefore = _scriptService.GetAllProjectGhostRunnerScripts(1);
            Assert.AreEqual(2, project1ScriptsBefore.Count);

            Boolean updateSuccessfull = _scriptService.DeleteScript("5a768553-052e-47ee-bf48-68f8aaf9cd05");
            Assert.IsTrue(updateSuccessfull);

            IList<IGhostRunnerScript> project1ScriptsAfter = _scriptService.GetAllProjectGhostRunnerScripts(1);
            Assert.AreEqual(1, project1ScriptsAfter.Count);

            Boolean updateFailed = _scriptService.DeleteScript("99");
            Assert.IsFalse(updateFailed);
        }

        #endregion
    }
}

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GhostRunner.SL;
using System.Collections.Generic;
using GhostRunner.Models;

namespace GhostRunner.Tests.SL
{
    [TestClass]
    public class SequenceScriptServiceTests
    {
        private SequenceService _sequenceService;
        private SequenceScriptService _sequenceScriptService;

        [TestInitialize]
        public void Initialize()
        {
            _sequenceService = new SequenceService(new TestContext());
            _sequenceScriptService = new SequenceScriptService(new TestContext());
        }

        [TestMethod]
        public void GetAllSequenceScripts()
        {
            IList<SequenceScript> sequenceScripts = _sequenceScriptService.GetAllSequenceScripts("c2f5f76a-1ee7-4f92-9150-55de4cefa76f");
            Assert.AreEqual(2, sequenceScripts.Count);

            IList<SequenceScript> failingSequenceScripts = _sequenceScriptService.GetAllSequenceScripts("99");
            Assert.AreEqual(0, failingSequenceScripts.Count);
        }

        [TestMethod]
        public void GetSequenceScripts()
        {
            IList<SequenceScript> sequenceScripts = _sequenceScriptService.GetAllSequenceScripts("c2f5f76a-1ee7-4f92-9150-55de4cefa76f");
            Assert.AreEqual(2, sequenceScripts.Count);

            IList<SequenceScript> failingSequenceScripts = _sequenceScriptService.GetAllSequenceScripts("99");
            Assert.AreEqual(0, failingSequenceScripts.Count);
        }

        [TestMethod]
        public void GetSequenceScript()
        {
            SequenceScript sequenceScript = _sequenceScriptService.GetSequenceScript("0cec8cba-3249-44e6-96bb-ff49ac31cdde");
            Assert.IsNotNull(sequenceScript);
            Assert.AreEqual("Test Sequence Script 1", sequenceScript.Name);

            SequenceScript failingSequenceScript = _sequenceScriptService.GetSequenceScript("90");
            Assert.IsNull(failingSequenceScript);
        }

        [TestMethod]
        public void UpdateSequenceScript()
        {
            SequenceScript sequenceScriptBefore = _sequenceScriptService.GetSequenceScript("0cec8cba-3249-44e6-96bb-ff49ac31cdde");
            Assert.IsNotNull(sequenceScriptBefore);
            Assert.AreEqual("Test Sequence Script 1", sequenceScriptBefore.Name);
            Assert.AreEqual("Test script with parameter 1", sequenceScriptBefore.Content);

            Boolean updateSuccessful = _sequenceScriptService.UpdateSequenceScript("0cec8cba-3249-44e6-96bb-ff49ac31cdde", "Updated Sequence Script 1", "Updated script with parameter 99");
            Assert.IsTrue(updateSuccessful);

            SequenceScript sequenceScriptAfter = _sequenceScriptService.GetSequenceScript("0cec8cba-3249-44e6-96bb-ff49ac31cdde");
            Assert.IsNotNull(sequenceScriptAfter);
            Assert.AreEqual("Updated Sequence Script 1", sequenceScriptAfter.Name);
            Assert.AreEqual("Updated script with parameter 99", sequenceScriptAfter.Content);
        }

        [TestMethod]
        public void DeleteSequenceScript()
        {
            SequenceScript sequenceScriptBefore = _sequenceScriptService.GetSequenceScript("0cec8cba-3249-44e6-96bb-ff49ac31cdde");
            Assert.IsNotNull(sequenceScriptBefore);

            Boolean deleteSuccessful = _sequenceScriptService.DeleteSequenceScript("0cec8cba-3249-44e6-96bb-ff49ac31cdde");
            Assert.IsTrue(deleteSuccessful);

            SequenceScript sequenceScriptAfter = _sequenceScriptService.GetSequenceScript("0cec8cba-3249-44e6-96bb-ff49ac31cdde");
            Assert.IsNull(sequenceScriptAfter);
        }
    }
}

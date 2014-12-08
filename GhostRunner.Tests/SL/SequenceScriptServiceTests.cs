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
    }
}

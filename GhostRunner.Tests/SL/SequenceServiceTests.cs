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
    public class SequenceServiceTests
    {
        private SequenceService _sequenceService;
        private SequenceScriptService _sequenceScriptService;

        [TestInitialize]
        public void Initialize()
        {
            _sequenceService = new SequenceService(new TestContext());
            _sequenceScriptService = new SequenceScriptService(new TestContext());
        }

        #region Project Sequence Methods

        [TestMethod]
        public void GetAllSequences()
        {
            IList<Sequence> project1Sequences = _sequenceService.GetAllSequences(1);
            Assert.AreEqual(2, project1Sequences.Count);

            IList<Sequence> project99Sequences = _sequenceService.GetAllSequences(99);
            Assert.AreEqual(0, project99Sequences.Count);
        }

        [TestMethod]
        public void GetSequence()
        {
            Sequence sequence1 = _sequenceService.GetSequence("c2f5f76a-1ee7-4f92-9150-55de4cefa76f");
            Assert.IsNotNull(sequence1);

            Sequence sequence99 = _sequenceService.GetSequence("99");
            Assert.IsNull(sequence99);
        }

        [TestMethod]
        public void InsertSequence()
        {
            IList<Sequence> projectSequencesBefore = _sequenceService.GetAllSequences(1);
            Assert.AreEqual(2, projectSequencesBefore.Count);

            Sequence newSequence = _sequenceService.InsertSequence("d4708c0d-721e-426e-b49e-35990687db22", "New test sequence", "Description of new test sequence");
            Assert.IsNotNull(newSequence);
            Assert.AreEqual("New test sequence", newSequence.Name);
            Assert.AreEqual("Description of new test sequence", newSequence.Description);

            IList<Sequence> projectSequencesAfter = _sequenceService.GetAllSequences(1);
            Assert.AreEqual(3, projectSequencesAfter.Count);

            Sequence failingSequence = _sequenceService.InsertSequence("99", "Failing test sequence", "Description of failing test sequence");
            Assert.IsNull(failingSequence);

            IList<Sequence> projectSequencesAfterFailing = _sequenceService.GetAllSequences(1);
            Assert.AreEqual(3, projectSequencesAfterFailing.Count);
        }

        [TestMethod]
        public void GetAllSequencescripts()
        {
            IList<SequenceScript> sequenceScripts = _sequenceScriptService.GetAllSequenceScripts("c2f5f76a-1ee7-4f92-9150-55de4cefa76f");
            Assert.AreEqual(2, sequenceScripts.Count);

            IList<SequenceScript> failingSequenceScripts = _sequenceScriptService.GetAllSequenceScripts("99");
            Assert.AreEqual(0, failingSequenceScripts.Count);
        }

        [TestMethod]
        public void AddScriptToSequence()
        {
            IList<SequenceScript> sequenceScriptsBefore = _sequenceScriptService.GetAllSequenceScripts("c2f5f76a-1ee7-4f92-9150-55de4cefa76f");
            Assert.AreEqual(2, sequenceScriptsBefore.Count);

            SequenceScript sequenceScript = _sequenceService.AddScriptToSequence("c2f5f76a-1ee7-4f92-9150-55de4cefa76f", "8ebb4cd0-8e36-4778-9b0d-5ba86d9c0cce", "Test Script 1", new Dictionary<String, String>());
            Assert.IsNotNull(sequenceScript);

            SequenceScript failingSequenceScript = _sequenceService.AddScriptToSequence("99", "8ebb4cd0-8e36-4778-9b0d-5ba86d9c0cce", "Test Script 99", new Dictionary<String, String>());
            Assert.IsNull(failingSequenceScript);

            SequenceScript failingAgainSequenceScript = _sequenceService.AddScriptToSequence("c2f5f76a-1ee7-4f92-9150-55de4cefa76f", "99", "Test Script 99", new Dictionary<String, String>());
            Assert.IsNull(failingAgainSequenceScript);
        }

        [TestMethod]
        public void UpdateScriptOrderInSequence()
        {
            /*IList<SequenceScript> sequenceScriptsBefore = _sequenceScriptService.GetAllSequenceScripts("c2f5f76a-1ee7-4f92-9150-55de4cefa76f");
            Assert.AreEqual(2, sequenceScriptsBefore.Count);

            foreach (SequenceScript sequenceScript in sequenceScriptsBefore)
            {
                if (sequenceScript.Par != "5a768553-052e-47ee-bf48-68f8aaf9cd05") Assert.AreEqual(2, sequenceScript.Position);
                else Assert.AreEqual(1, sequenceScript.Position);
            }

            Boolean positionUpdated = _sequenceService.UpdateScriptOrderInSequence("c2f5f76a-1ee7-4f92-9150-55de4cefa76f", "5a768553-052e-47ee-bf48-68f8aaf9cd05", 2);
            Assert.IsTrue(positionUpdated);

            IList<SequenceScript> sequenceScriptsAfter = _sequenceScriptService.GetAllSequenceScripts("c2f5f76a-1ee7-4f92-9150-55de4cefa76f");
            Assert.AreEqual(2, sequenceScriptsAfter.Count);*/
        }

        [TestMethod]
        public void RemoveScriptFromSequence()
        {
            Boolean scriptDeleted = _sequenceService.RemoveScriptFromSequence("c2f5f76a-1ee7-4f92-9150-55de4cefa76f", "0cec8cba-3249-44e6-96bb-ff49ac31cdde");
            Assert.IsTrue(scriptDeleted);
        }

        #endregion
    }
}

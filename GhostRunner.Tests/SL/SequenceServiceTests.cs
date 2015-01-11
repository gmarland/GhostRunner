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
        public void UpdateSequence()
        {
            Sequence sequenceBefore = _sequenceService.GetSequence("c2f5f76a-1ee7-4f92-9150-55de4cefa76f");
            Assert.IsNotNull(sequenceBefore);
            Assert.AreEqual("Test Sequence 1", sequenceBefore.Name);
            Assert.AreEqual("Sequence used for testing", sequenceBefore.Description);

            _sequenceService.UpdateSequence("c2f5f76a-1ee7-4f92-9150-55de4cefa76f", "new sequence name", "new sequence desc");

            Sequence sequenceAfter = _sequenceService.GetSequence("c2f5f76a-1ee7-4f92-9150-55de4cefa76f");
            Assert.IsNotNull(sequenceAfter);
            Assert.AreEqual("new sequence name", sequenceAfter.Name);
            Assert.AreEqual("new sequence desc", sequenceAfter.Description);
        }

        [TestMethod]
        public void DeleteSequence()
        {
            Sequence sequenceBefore = _sequenceService.GetSequence("c2f5f76a-1ee7-4f92-9150-55de4cefa76f");
            Assert.IsNotNull(sequenceBefore);

            _sequenceService.DeleteSequence("c2f5f76a-1ee7-4f92-9150-55de4cefa76f");

            Sequence sequenceAfter = _sequenceService.GetSequence("c2f5f76a-1ee7-4f92-9150-55de4cefa76f");
            Assert.IsNull(sequenceAfter);
        }

        [TestMethod]
        public void AddScriptToSequence()
        {
            IList<SequenceScript> sequenceScriptsBefore = _sequenceScriptService.GetAllSequenceScripts("c2f5f76a-1ee7-4f92-9150-55de4cefa76f");
            Assert.AreEqual(2, sequenceScriptsBefore.Count);

            SequenceScript sequenceScript = _sequenceService.AddScriptToSequence("c2f5f76a-1ee7-4f92-9150-55de4cefa76f", "8ebb4cd0-8e36-4778-9b0d-5ba86d9c0cce", ScriptType.Node, "Test Script 1", new Dictionary<String, String>());
            Assert.IsNotNull(sequenceScript);

            SequenceScript failingSequenceScript = _sequenceService.AddScriptToSequence("99", "8ebb4cd0-8e36-4778-9b0d-5ba86d9c0cce", ScriptType.Node, "Test Script 99", new Dictionary<String, String>());
            Assert.IsNull(failingSequenceScript);

            SequenceScript failingAgainSequenceScript = _sequenceService.AddScriptToSequence("c2f5f76a-1ee7-4f92-9150-55de4cefa76f", "99", ScriptType.Node, "Test Script 99", new Dictionary<String, String>());
            Assert.IsNull(failingAgainSequenceScript);
        }

        [TestMethod]
        public void RemoveScriptFromSequence()
        {
            Boolean scriptDeleted = _sequenceService.RemoveScriptFromSequence("c2f5f76a-1ee7-4f92-9150-55de4cefa76f", "0cec8cba-3249-44e6-96bb-ff49ac31cdde");
            Assert.IsTrue(scriptDeleted);
        }

        [TestMethod]
        public void UpdateScriptOrderInSequence()
        {
            Boolean updateSuccessful = _sequenceService.UpdateScriptOrderInSequence("c2f5f76a-1ee7-4f92-9150-55de4cefa76f", new String[] { "d3094a43-b05d-4b3f-8031-50d082239ea3", "0cec8cba-3249-44e6-96bb-ff49ac31cdde" });
            Assert.IsTrue(updateSuccessful);
        }

        #endregion
    }
}

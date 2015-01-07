using GhostRunner.DAL;
using GhostRunner.DAL.Interface;
using GhostRunner.Models;
using GhostRunner.Utils;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace GhostRunner.SL
{
    public class SequenceService
    {
        #region Private Properties

        private static readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private IProjectDataAccess _projectDataAccess;
        private ISequenceDataAccess _sequenceDataAccess;
        private IScriptDataAccess _scriptDataAccess;
        private ISequenceScriptDataAccess _sequenceScriptDataAccess;

        #endregion

        #region Constructors

        public SequenceService()
        {
            InitializeDataAccess(new GhostRunnerContext("DatabaseConnectionString"));
        }

        public SequenceService(IContext context)
        {
            InitializeDataAccess(context);
        }

        #endregion

        #region Public Methods

        public IList<Sequence> GetAllSequences(int projectId)
        {
            return _sequenceDataAccess.GetAll(projectId).OrderBy(s => s.Name).ToList();
        }

        public Sequence GetSequence(String sequenceId)
        {
            return _sequenceDataAccess.Get(sequenceId);
        }

        public Sequence InsertSequence(String projectId, String name, String description)
        {
            Project project = _projectDataAccess.GetByExternalId(projectId);

            if (project != null)
            {
                Sequence sequence = new Sequence();
                sequence.ExternalId = System.Guid.NewGuid().ToString();
                sequence.Project = project;
                sequence.Name = name;
                sequence.Description = description;

                return _sequenceDataAccess.Insert(sequence);
            }
            else
            {
                _log.Info("InsertSequence(" + projectId + "): Unable to find project");

                return null;
            }
        }

        public Boolean UpdateSequence(String sequenceId, String name, String description)
        {
            return _sequenceDataAccess.Update(sequenceId, name, description);
        }

        public Boolean DeleteSequence(String sequenceId)
        {
            return _sequenceDataAccess.Delete(sequenceId);
        }

        public SequenceScript AddScriptToSequence(String sequenceId, String scriptId, ScriptType scriptType, String scriptName, Dictionary<String, String> parameters)
        {
            Sequence sequence = _sequenceDataAccess.Get(sequenceId);
            Script script = _scriptDataAccess.Get(scriptId);

            if ((sequence != null) && (script != null))
            {
                IGhostRunnerScript ghostRunnerScript = ScriptHelper.GetGhostRunnerScript(script);

                int scriptPosition = _sequenceScriptDataAccess.GetNextPosition(sequenceId);
                if (scriptPosition < 1) scriptPosition = 1;

                SequenceScript sequenceScript = new SequenceScript();
                sequenceScript.ExternalId = System.Guid.NewGuid().ToString();
                sequenceScript.Sequence = sequence;
                sequenceScript.Type = scriptType;
                sequenceScript.Name = scriptName;
                sequenceScript.Content = script.Content;

                foreach (String scriptParameter in ghostRunnerScript.GetAllParameters())
                {
                    String parameterValue = String.Empty;
                    if (parameters.ContainsKey(scriptParameter)) parameterValue = parameters[scriptParameter];

                    sequenceScript.Content = Regex.Replace(sequenceScript.Content, "\\[\\[" + scriptParameter + "\\]\\]", parameterValue);
                }

                sequenceScript.Position = scriptPosition;

                _sequenceScriptDataAccess.Insert(sequenceScript);

                _sequenceScriptDataAccess.UpdateScriptOrder(sequenceId);

                return sequenceScript;
            }
            else return null;
        }

        public Boolean RemoveScriptFromSequence(String sequenceId, String sequenceScriptId)
        {
            Boolean deleteSuccessful = _sequenceScriptDataAccess.Delete(sequenceScriptId);

            if (deleteSuccessful)
            {
                _sequenceScriptDataAccess.UpdateScriptOrder(sequenceId);

                return true;
            }
            else return false;
        }

        public Boolean UpdateScriptOrderInSequence(String sequenceId, String[] scriptSequence)
        {
            for (int i = 0; i < scriptSequence.Length; i++)
            {
                _sequenceScriptDataAccess.UpdateScriptSequenceOrder(scriptSequence[i], (i + 1));
            }

            return true;
        }

        #endregion

        #region Private Methods

        private void InitializeDataAccess(IContext context)
        {
            _projectDataAccess = new ProjectDataAccess(context);
            _sequenceDataAccess = new SequenceDataAccess(context);
            _scriptDataAccess = new ScriptDataAccess(context);
            _sequenceScriptDataAccess = new SequenceScriptDataAccess(context);
        }

        #endregion
    }
}

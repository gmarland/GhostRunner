using GhostRunner.DAL;
using GhostRunner.DAL.Interface;
using GhostRunner.Models;
using GhostRunner.Utils;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GhostRunner.SL
{
    public class ScriptService
    {
        #region Private Properties

        private static readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private IProjectDataAccess _projectDataAccess;
        private IScriptDataAccess _scriptDataAccess;
        private ITaskDataAccess _taskDataAccess;

        #endregion

        #region Constructors

        public ScriptService()
        {
            InitializeDataAccess(new GhostRunnerContext("DatabaseConnectionString"));
        }

        public ScriptService(IContext context)
        {
            InitializeDataAccess(context);
        }

        #endregion

        #region Public Methods

        public IList<IGhostRunnerScript> GetAllProjectGhostRunnerScripts(int projectId)
        {
            List<IGhostRunnerScript> ghostRunnerScripts = new List<IGhostRunnerScript>();

            foreach (Script script in _scriptDataAccess.GetAll(projectId))
            {
                ghostRunnerScripts.Add(ScriptHelper.GetGhostRunnerScript(script));
            }

            return ghostRunnerScripts.OrderBy(grs => grs.Name).ToList();
        }

        public Script GetScript(String scriptId)
        {
            return _scriptDataAccess.Get(scriptId);
        }

        public Status GetScriptTaskStatus(String scriptId)
        {
            Script script = _scriptDataAccess.Get(scriptId);

            if (script != null)
            {
                IList<Task> scriptTasks = _taskDataAccess.GetAllByScriptId(script.ID);

                if (scriptTasks.Where(it => it.Status == Status.Processing).Count() > 0) return Status.Processing;
                else if (scriptTasks.Where(it => it.Status == Status.Unprocessed).Count() > 0) return Status.Unprocessed;
                else return Status.Completed;
            }
            else
            {
                _log.Info("GetInitializationTaskStatus(" + scriptId + "): Unable to retrieve script");

                return Status.Unknown;
            }
        }

        public Script InsertScript(String projectId, String scriptType, String name, String description, String content)
        {
            Project project = _projectDataAccess.GetByExternalId(projectId);

            if (project != null)
            {
                Script script = new Script();
                script.ExternalId = System.Guid.NewGuid().ToString();
                script.Type = ScriptHelper.GetScriptType(scriptType);
                script.Project = project;
                script.Name = name;
                script.Description = description;
                script.Content = content;

                return _scriptDataAccess.Insert(script);
            }
            else
            {
                _log.Info("InsertScript(" + projectId + "): Unable to find project");

                return null;
            }
        }

        public Boolean UpdateScript(String scriptId, String name, String description, String script)
        {
            return _scriptDataAccess.Update(scriptId, name, description, script);
        }

        public Boolean DeleteScript(String scriptId)
        {
            return _scriptDataAccess.Delete(scriptId);
        }

        #endregion

        #region Private Methods

        private void InitializeDataAccess(IContext context)
        {
            _projectDataAccess = new ProjectDataAccess(context);
            _scriptDataAccess = new ScriptDataAccess(context);
            _taskDataAccess = new TaskDataAccess(context);
        }

        #endregion
    }
}

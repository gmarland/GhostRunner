using GhostRunner.DAL;
using GhostRunner.DAL.Interface;
using GhostRunner.Models;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GhostRunner.SL
{
    public class TaskService
    {
        #region Private Properties

        private static readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private ISequenceDataAccess _sequenceDataAccess;
        private IScriptDataAccess _scriptDataAccess;
        private ISequenceScriptDataAccess _sequenceScriptDataAccess;
        private ITaskDataAccess _taskDataAccess;
        private ITaskScriptDataAccess _taskScriptDataAccess;
        private ITaskScriptParameterDataAccess _taskScriptParameterDataAccess;

        #endregion

        #region Constructors

        public TaskService()
        {
            InitializeDataAccess(new GhostRunnerContext("DatabaseConnectionString"));
        }

        public TaskService(IContext context)
        {
            InitializeDataAccess(context);
        }

        #endregion

        #region Public Methods

        public IList<Task> GetAllTasks(int projectId)
        {
            return _taskDataAccess.GetAllByProjectId(projectId).OrderByDescending(it => it.Created).ToList();
        }

        public IList<Task> GetAllTasks(int projectId, int limit)
        {
            return GetAllTasks(projectId, limit, String.Empty);
        }

        public IList<Task> GetAllTasks(int projectId, int limit, String startingAfter)
        {
            IList<Task> tasks = GetAllTasks(projectId);

            if (!String.IsNullOrEmpty(startingAfter))
            {
                while ((tasks.Count > 0) && (tasks.First().ExternalId != startingAfter))
                {
                    tasks.RemoveAt(0);
                }

                if (tasks.Count > 0) tasks.RemoveAt(0);
            }

            if (tasks.Count <= limit) return tasks;
            else return tasks.Take(limit).ToList();
        }

        public Task InsertScriptTask(String scriptId, String name, IList<TaskScriptParameter> taskScriptParameter)
        {
            Script script = _scriptDataAccess.Get(scriptId);

            if (script != null)
            {
                Task task = new Task();
                task.ExternalId = System.Guid.NewGuid().ToString();
                task.Project = script.Project;
                task.ParentId = script.ID;
                task.ParentType = ItemType.Script;
                task.Name = name;
                task.Status = Status.Unprocessed;
                task.Created = DateTime.UtcNow;

                task = _taskDataAccess.Insert(task);

                TaskScript taskScript = new TaskScript();
                taskScript.Type = script.Type;
                taskScript.Task = task;
                taskScript.Content = script.Content;

                taskScript = _taskScriptDataAccess.Insert(taskScript);

                if (taskScriptParameter != null)
                {
                    foreach (TaskScriptParameter scriptTaskParameter in taskScriptParameter)
                    {
                        InsertTaskScriptParameter(taskScript.ID, scriptTaskParameter.Name, scriptTaskParameter.Value);
                    }
                }

                return task;
            }
            else
            {
                _log.Info("InsertScriptTask(" + scriptId + "): Unable to find script");

                return null;
            }
        }

        public Task InsertSequenceTask(String sequenceId, String name)
        {
            Sequence sequence = _sequenceDataAccess.Get(sequenceId);

            if (sequence != null)
            {
                Task task = new Task();
                task.ExternalId = System.Guid.NewGuid().ToString();
                task.Project = sequence.Project;
                task.ParentId = sequence.ID;
                task.ParentType = ItemType.Sequence;
                task.Name = name;
                task.Status = Status.Unprocessed;
                task.Created = DateTime.UtcNow;

                task = _taskDataAccess.Insert(task);

                foreach (SequenceScript sequenceScript in sequence.SequenceScripts)
                {
                    TaskScript taskScript = new TaskScript();
                    taskScript.Task = task;
                    taskScript.Type = sequenceScript.Type;
                    taskScript.Content = sequenceScript.Content;

                    _taskScriptDataAccess.Insert(taskScript);
                }

                return task;
            }
            else
            {
                _log.Info("InsertSequenceTask(" + sequenceId + "): Unable to find script");

                return null;
            }
        }

        public Task InsertSequenceScriptTask(String sequenceScriptId)
        {
            SequenceScript sequenceScript = _sequenceScriptDataAccess.Get(sequenceScriptId);

            if (sequenceScript != null)
            {
                Task task = new Task();
                task.ExternalId = System.Guid.NewGuid().ToString();
                task.Project = sequenceScript.Sequence.Project;
                task.ParentId = sequenceScript.ID;
                task.ParentType = ItemType.SequenceScript;
                task.Name = sequenceScript.Name;
                task.Status = Status.Unprocessed;
                task.Created = DateTime.UtcNow;

                task = _taskDataAccess.Insert(task);

                TaskScript taskScript = new TaskScript();
                taskScript.Task = task;
                taskScript.Type = sequenceScript.Type;
                taskScript.Content = sequenceScript.Content;

                _taskScriptDataAccess.Insert(taskScript);

                return task;
            }
            else
            {
                _log.Info("InsertSequenceScriptTask(" + sequenceScriptId + "): Unable to find sequence script");

                return null;
            }
        }

        public TaskScriptParameter InsertTaskScriptParameter(int taskScriptId, String name, String value)
        {
            TaskScript taskScript = _taskScriptDataAccess.Get(taskScriptId);

            if (taskScript != null)
            {
                TaskScriptParameter taskScriptParameter = new TaskScriptParameter();
                taskScriptParameter.TaskScript = taskScript;
                taskScriptParameter.Name = name;
                taskScriptParameter.Value = value;

                return _taskScriptParameterDataAccess.Insert(taskScriptParameter);
            }
            else
            {
                _log.Info("InsertTaskScriptParameter(" + taskScriptId + "): Unable to find script task");

                return null;
            }
        }

        #endregion

        #region Private Methods

        private void InitializeDataAccess(IContext context)
        {
            _sequenceDataAccess = new SequenceDataAccess(context);
            _scriptDataAccess = new ScriptDataAccess(context);
            _sequenceScriptDataAccess = new SequenceScriptDataAccess(context);
            _taskDataAccess = new TaskDataAccess(context);
            _taskScriptDataAccess = new TaskScriptDataAccess(context);
            _taskScriptParameterDataAccess = new TaskScriptParameterDataAccess(context);
        }

        #endregion
    }
}

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
    public class ProjectService
    {
        private IProjectDataAccess _projectDataAccess;
        private IUserDataAccess _userDataAccess;
        private ISequenceDataAccess _sequenceDataAccess;
        private IScriptDataAccess _scriptDataAccess;
        private ISequenceScriptDataAccess _sequenceScriptDataAccess;
        private ISequenceScriptParameterDataAccess _sequenceScriptParameterDataAccess;
        private ITaskDataAccess _taskDataAccess;
        private ITaskParameterDataAccess _taskParameterDataAccess;

        private static readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public ProjectService()
        {
            InitializeDataAccess(new GhostRunnerContext("DatabaseConnectionString"));
        }

        public ProjectService(IContext context)
        {
            InitializeDataAccess(context);
        }

        #region Project Methods

        public IList<Project> GetAllProjects(int userId)
        {
            return _projectDataAccess.GetByUserId(userId);
        }

        public Project GetProject(int projectId)
        {
            return _projectDataAccess.GetById(projectId);
        }

        public Project GetProject(String projectId)
        {
            return _projectDataAccess.GetByExternalId(projectId);
        }

        public Project InsertProject(int userId, String name)
        {
            User user = _userDataAccess.GetById(userId);

            if (user != null)
            {
                Project project = new Project();
                project.ExternalId = System.Guid.NewGuid().ToString();
                project.Name = name;
                project.Created = DateTime.UtcNow;

                project = _projectDataAccess.Insert(project);

                _projectDataAccess.AddUserToProject(user, project);

                return project;
            }
            else
            {
                _log.Info("InsertProject(" + userId + "): Unable to find the selected user");

                return null;
            }
        }

        public Boolean UpdateProject(String projectId, String name)
        {
            return _projectDataAccess.Update(projectId, name);
        }

        public Boolean DeleteProject(String projectId)
        {
            return _projectDataAccess.Delete(projectId);
        }

        #endregion

        #region Project Sequence Methods

        public IList<Sequence> GetAllProjectSequences(int projectId)
        {
            return _sequenceDataAccess.GetAll(projectId);
        }

        public Sequence GetProjectSequence(String sequenceId)
        {
            return _sequenceDataAccess.Get(sequenceId);
        }

        public IList<SequenceScript> GetAllProjectSequenceScripts(String sequenceId)
        {
            return _sequenceScriptDataAccess.GetAll(sequenceId).OrderBy(ss => ss.Position).ToList();
        }

        public IList<SequenceScript> GetProjectSequenceScripts(String sequenceId)
        {
            Sequence sequence = _sequenceDataAccess.Get(sequenceId);

            if ((sequence != null) && (sequence.SequenceScripts != null))
            {
                IList<SequenceScript> sequenceScripts = sequence.SequenceScripts.ToList();

                if (sequenceScripts.Count > 0) return sequenceScripts.OrderBy(ss => ss.Position).ToList();
                else return new List<SequenceScript>();
            }
            else return new List<SequenceScript>();
        }

        public Sequence InsertProjectSequence(String projectId, String name, String description)
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

        public SequenceScript AddScriptToProjectSequence(String sequenceId, String scriptId, String scriptName, Dictionary<String, String> parameters)
        {
            Sequence sequence = _sequenceDataAccess.Get(sequenceId);
            Script script = _scriptDataAccess.Get(scriptId);
            
            if ((sequence != null) && (script != null))
            {
                int scriptPosition = _sequenceScriptDataAccess.GetNextPosition(sequenceId);
                if (scriptPosition < 1) scriptPosition = 1;

                SequenceScript sequenceScript = new SequenceScript();
                sequenceScript.ExternalId = System.Guid.NewGuid().ToString();
                sequenceScript.Sequence = sequence;
                sequenceScript.Name = scriptName;
                sequenceScript.Content = script.Content;
                sequenceScript.Position = scriptPosition;

                _sequenceScriptDataAccess.Insert(sequenceScript);

                foreach (String scriptParameter in script.GetAllParameters())
                {
                    String parameterValue = String.Empty;
                    if (parameters.ContainsKey(scriptParameter)) parameterValue = parameters[scriptParameter];

                    SequenceScriptParameter sequenceScriptParameter = new SequenceScriptParameter();
                    sequenceScriptParameter.SequenceScript = sequenceScript;
                    sequenceScriptParameter.Name = scriptParameter;
                    sequenceScriptParameter.Value = parameterValue;

                    _sequenceScriptParameterDataAccess.Insert(sequenceScriptParameter);
                }

                _sequenceScriptDataAccess.UpdateScriptOrder(sequenceId);

                return sequenceScript;
            }
            else return null;
        }

        public Boolean UpdateScriptOrderInProjectSequence(String sequenceId, String[] scriptSequence)
        {
            for (int i=0; i<scriptSequence.Length; i++)
            {
                _sequenceScriptDataAccess.UpdateScriptSequenceOrder(scriptSequence[i], (i + 1));
            }

            return true;
        }

        public Boolean RemoveScriptFromProjectSequence(String sequenceId, String sequenceScriptId)
        {
            Boolean deleteSuccessful = _sequenceScriptDataAccess.Delete(sequenceScriptId);

            if (deleteSuccessful)
            {
                _sequenceScriptDataAccess.UpdateScriptOrder(sequenceId);

                return true;
            }
            else return false;
        }

        #endregion

        #region Project Script Methods

        public IList<Script> GetAllProjectScripts(int projectId)
        {
            return _scriptDataAccess.GetAll(projectId);
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

        public Script InsertScript(String projectId, String name, String description, String content)
        {
            Project project = _projectDataAccess.GetByExternalId(projectId);

            if (project != null)
            {
                Script script = new Script();
                script.ExternalId = System.Guid.NewGuid().ToString();
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

        #region Project Task Methods

        public IList<Task> GetAllTasks(int projectId)
        {
            return _taskDataAccess.GetAllByProjectId(projectId).OrderByDescending(it => it.Created).OrderBy(it => it.Status).ToList();
        }

        public Task InsertTask(int userId, String scriptId, String name)
        {
            User user = _userDataAccess.GetById(userId);

            if (user != null)
            {
                Script script = _scriptDataAccess.Get(scriptId);

                if (script != null)
                {
                    Task task = new Task();
                    task.ExternalId = System.Guid.NewGuid().ToString();
                    task.Script = script;
                    task.Name = name;
                    task.Description = script.Description;
                    task.Content = script.Content;
                    task.Status = Status.Unprocessed;
                    task.Created = DateTime.UtcNow;

                    return _taskDataAccess.Insert(task);
                }
                else
                {
                    _log.Info("InsertTask(" + scriptId + "): Unable to find script");

                    return null;
                }
            }
            else
            {
                _log.Info("InsertTask(" + userId + "): Unable to find user");

                return null;
            }
        }

        public TaskParameter InsertTaskParameter(String taskId, String name, String value)
        {
            Task task = _taskDataAccess.Get(taskId);

            if (task != null)
            {
                TaskParameter taskParameter = new TaskParameter();
                taskParameter.Task = task;
                taskParameter.Name = name;
                taskParameter.Value = value;

                return _taskParameterDataAccess.Insert(taskParameter);
            }
            else
            {
                _log.Info("InsertTaskParameter(" + taskId + "): Unable to find script task");

                return null;
            }
        }

        #endregion

        #region Private Methods

        private void InitializeDataAccess(IContext context)
        {
            _projectDataAccess = new ProjectDataAccess(context);
            _userDataAccess = new UserDataAccess(context);
            _sequenceDataAccess = new SequenceDataAccess(context);
            _scriptDataAccess = new ScriptDataAccess(context);
            _sequenceScriptDataAccess = new SequenceScriptDataAccess(context);
            _sequenceScriptParameterDataAccess = new SequenceScriptParameterDataAccess(context);
            _taskDataAccess = new TaskDataAccess(context);
            _taskParameterDataAccess = new TaskParameterDataAccess(context);
        }

        #endregion
    }
}

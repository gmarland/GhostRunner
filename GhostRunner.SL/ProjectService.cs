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
        private IScriptDataAccess _scriptDataAccess;
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
            User user = _userDataAccess.GetById(userId);

            if (user != null)
            {
                return _projectDataAccess.GetByUserId(user.ID);
            }
            else
            {
                return new List<Project>();
            }
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

        #endregion

        #region Project Initialization Methods

        public Status GetScriptTaskStatus(String scriptId)
        {
            Script script = _scriptDataAccess.Get(scriptId);

            if (script != null) {
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

        public IList<Script> GetAllProjectScripts(int projectId)
        {
            return _scriptDataAccess.GetAll(projectId);
        }

        public Script GetScript(String scriptId)
        {
            return _scriptDataAccess.Get(scriptId);
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
            _scriptDataAccess = new ScriptDataAccess(context);
            _taskDataAccess = new TaskDataAccess(context);
            _taskParameterDataAccess = new TaskParameterDataAccess(context);
        }

        #endregion
    }
}

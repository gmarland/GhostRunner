using GhostRunner.DAL;
using GhostRunner.DAL.Interface;
using GhostRunner.Models;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace GhostRunner.SL
{
    public class ProjectService
    {
        #region Private Properties

        private static readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private IProjectDataAccess _projectDataAccess;
        private IUserDataAccess _userDataAccess;
        
        #endregion

        #region Constructors

        public ProjectService()
        {
            InitializeDataAccess(new GhostRunnerContext("DatabaseConnectionString"));
        }

        public ProjectService(IContext context)
        {
            InitializeDataAccess(context);
        }

        #endregion

        #region Public Methods

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

        #region Private Methods

        private void InitializeDataAccess(IContext context)
        {
            _projectDataAccess = new ProjectDataAccess(context);
            _userDataAccess = new UserDataAccess(context);
        }

        #endregion
    }
}

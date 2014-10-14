using GhostRunner.DAL.Interface;
using GhostRunner.Models;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GhostRunner.DAL
{
    public class ProjectDataAccess : IProjectDataAccess
    {
        protected GhostRunnerContext _context;

        private static readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public ProjectDataAccess(GhostRunnerContext context)
        {
            _context = context;
        }

        public Project GetById(int projectId)
        {
            try
            {
                return _context.Projects.SingleOrDefault(p => p.ID == projectId);
            }
            catch (Exception ex)
            {
                _log.Error("GetById(" + projectId + "): Error retrieving project", ex);

                return null;
            }
        }

        public Project GetByExternalId(String projectId)
        {
            try
            {
                return _context.Projects.SingleOrDefault(p => p.ExternalId== projectId);
            }
            catch (Exception ex)
            {
                _log.Error("GetByExternalId(" + projectId + "): Error retrieving project", ex);

                return null;
            }
        }

        public IList<Project> GetByUserId(int userId)
        {
            try
            {
                return _context.Users.SingleOrDefault(u => u.ID == userId).Projects.ToList();
            }
            catch (Exception ex)
            {
                _log.Error("GetByUserId(" + userId + "): Error retrieving projects", ex);

                return new List<Project>();
            }
        }

        public Boolean AddUserToProject(User user, Project project)
        {
            try
            {
                user.Projects.Add(project);
                Save();

                return true;
            }
            catch (Exception ex)
            {
                _log.Error("AddUserToProject(): Unable to add user " + user.ExternalId + " to project " + project.ExternalId, ex);

                return false;
            }
        }

        public Project Insert(Project project)
        {
            try
            {
                _context.Projects.Add(project);
                Save();

                return project;
            }
            catch (Exception ex)
            {
                _log.Error("Insert(): Unable to add new project", ex);

                return null;
            }
        }

        public Boolean Update(int projectId, string name)
        {
            Project project = null;

            try
            {
                project = _context.Projects.SingleOrDefault(c => c.ID == projectId);
            }
            catch (Exception ex)
            {
                _log.Error("Update(" + projectId + "): Error retrieving project", ex);

                return false;
            }

            if (project != null)
            {
                try
                {
                    project.Name = name;
                    Save();

                    return true;
                }
                catch (Exception ex)
                {
                    _log.Error("Update(" + projectId + ", " + name + "): Error saving project", ex);

                    return false;
                }
            }
            else
            {
                _log.Info("Update(" + projectId + "): Project not found");

                return false;
            }
        }

        public Boolean Delete(Project project)
        {
            try
            {
                _context.Projects.Remove(project);
                Save();

                return true;
            }
            catch (Exception ex)
            {
                _log.Error("Delete(" + project.ID + "): Unable to remove project", ex);

                return false;
            }
        }

        private void Save()
        {
            try
            {
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

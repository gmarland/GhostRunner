using GhostRunner.DAL.Interface;
using GhostRunner.Models;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GhostRunner.DAL
{
    public class TaskDataAccess : ITaskDataAccess
    {
        protected IContext _context;

        private static readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public TaskDataAccess(IContext context)
        {
            _context = context;
        }

        public Task Get(String taskId)
        {
            try
            {
                return _context.Tasks.SingleOrDefault(it => it.ExternalId == taskId);
            }
            catch (Exception ex)
            {
                _log.Error("Get(" + taskId + "): Unable to retrieve initialization task", ex);

                return null;
            }
        }

        public IList<Task> GetAllByProjectId(long projectId)
        {
            try
            {
                return _context.Tasks.Where(t => t.Project.ID == projectId).ToList();
            }
            catch (Exception ex)
            {
                _log.Error("GetAllByProjectId(" + projectId + "): Unable to retrieve tasks", ex);

                return new List<Task>();
            }
        }

        public IList<Task> GetAllByScriptId(long scriptId)
        {
            try
            {
                return _context.Tasks.Where(t => t.ParentId == scriptId && t.ParentType == ItemType.Script).ToList();
            }
            catch (Exception ex)
            {
                _log.Error("GetAll(" + scriptId + "): Unable to retrieve initialization tasks", ex);

                return new List<Task>();
            }
        }

        public Task Insert(Task task)
        {
            try
            {
                _context.Tasks.Add(task);
                Save();

                return task;
            }
            catch (Exception ex)
            {
                _log.Error("Insert(): Unable to add new task", ex);

                return null;
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

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
    public class TaskScriptParameterDataAccess : ITaskScriptParameterDataAccess
    {
        protected IContext _context;

        private static readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public TaskScriptParameterDataAccess(IContext context)
        {
            _context = context;
        }

        public IList<TaskScriptParameter> GetAll(long taskScriptId)
        {
            try
            {
                return _context.TaskScriptParameters.Where(tsp => tsp.ID == taskScriptId).ToList();
            }
            catch (Exception ex)
            {
                _log.Error("GetAll(" + taskScriptId + "): Unable to retrieve task script parameters", ex);

                return new List<TaskScriptParameter>();
            }
        }

        public TaskScriptParameter Insert(TaskScriptParameter taskScriptParameter)
        {
            try
            {
                _context.TaskScriptParameters.Add(taskScriptParameter);
                Save();

                return taskScriptParameter;
            }
            catch (Exception ex)
            {
                _log.Error("Insert(): Unable to add new task parameter", ex);

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

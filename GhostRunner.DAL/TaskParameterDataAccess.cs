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
    public class TaskParameterDataAccess : ITaskParameterDataAccess
    {
        protected IContext _context;

        private static readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public TaskParameterDataAccess(IContext context)
        {
            _context = context;
        }

        public IList<TaskParameter> GetAll(int intializationTaskId)
        {
            try
            {
                return _context.TaskParameters.Where(itp => itp.Task.ID == intializationTaskId).ToList();
            }
            catch (Exception ex)
            {
                _log.Error("GetAll(" + intializationTaskId + "): Unable to retrieve initialization task parameters", ex);

                return new List<TaskParameter>();
            }
        }

        public TaskParameter Insert(TaskParameter taskParameter)
        {
            try
            {
                _context.TaskParameters.Add(taskParameter);
                Save();

                return taskParameter;
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

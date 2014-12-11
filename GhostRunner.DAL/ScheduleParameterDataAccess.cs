using GhostRunner.DAL.Interface;
using GhostRunner.Models;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GhostRunner.DAL
{
    public class ScheduleParameterDataAccess : IScheduleParameterDataAccess
    {
        protected IContext _context;

        private static readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public ScheduleParameterDataAccess(IContext context)
        {
            _context = context;
        }

        public ScheduleParameter Insert(ScheduleParameter scheduleParameter)
        {
            try
            {
                _context.ScheduleParameters.Add(scheduleParameter);
                Save();

                return scheduleParameter;
            }
            catch (Exception ex)
            {
                _log.Error("Insert(): Unable to add new schedule parameter", ex);

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

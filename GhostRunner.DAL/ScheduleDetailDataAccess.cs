using GhostRunner.DAL.Interface;
using GhostRunner.Models;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GhostRunner.DAL
{
    public class ScheduleDetailDataAccess : IScheduleDetailDataAccess
    {
        protected IContext _context;

        private static readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public ScheduleDetailDataAccess(IContext context)
        {
            _context = context;
        }

        public ScheduleDetail Insert(ScheduleDetail scheduleDetail)
        {
            try
            {
                _context.ScheduleDetails.Add(scheduleDetail);
                Save();

                return scheduleDetail;
            }
            catch (Exception ex)
            {
                _log.Error("Insert(): Unable to add new schedule detail", ex);

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

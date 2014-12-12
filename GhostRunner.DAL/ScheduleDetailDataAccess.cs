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

        public Boolean Delete(int scheduleDetailId)
        {
            ScheduleDetail scheduleDetail = null;

            try
            {
                scheduleDetail = _context.ScheduleDetails.SingleOrDefault(sd => sd.ID == scheduleDetailId);
            }
            catch (Exception ex)
            {
                _log.Error("Delete(" + scheduleDetailId + "): Unable to find schedule detail", ex);

                return false;
            }

            if (scheduleDetail != null)
            {
                _context.ScheduleDetails.Remove(scheduleDetail);
                    
                try
                {
                    Save();

                    return true;
                }
                catch (Exception ex)
                {
                    _log.Error("Delete(" + scheduleDetailId + "): Unable to delete schedule detail", ex);

                    return false;
                }
            }
            else
            {
                _log.Error("Delete(" + scheduleDetailId + "): Unable to find schedule detail");

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

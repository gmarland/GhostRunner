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
    public class ScheduleDataAccess : IScheduleDataAccess
    {
        protected IContext _context;

        private static readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public ScheduleDataAccess(IContext context)
        {
            _context = context;
        }

        public IList<Schedule> GetAll(int projectId)
        {
            try
            {
                return _context.Schedules.Where(s => s.Project.ID == projectId).ToList();
            }
            catch (Exception ex)
            {
                _log.Error("GetAll(" + projectId + "): Error retrieving all schedules", ex);

                return new List<Schedule>();
            }
        }

        public Schedule Get(String scheduleId)
        {
            try
            {
                return _context.Schedules.SingleOrDefault(s => s.ExternalId == scheduleId);
            }
            catch (Exception ex)
            {
                _log.Error("Get(" + scheduleId + "): Error retrieving schedule", ex);

                return null;
            }
        }

        public Schedule Insert(Schedule schedule)
        {
            try
            {
                _context.Schedules.Add(schedule);
                Save();

                return schedule;
            }
            catch (Exception ex)
            {
                _log.Error("Insert(): Unable to add new schedule", ex);

                return null;
            }
        }

        public Boolean Update(String scheduleId, String type)
        {
            Schedule schedule = null;

            try
            {
                schedule = _context.Schedules.SingleOrDefault(s => s.ExternalId == scheduleId);
            }
            catch (Exception ex)
            {
                _log.Error("Update(" + scheduleId + "): Unable to find schedule", ex);
            }

            if (schedule != null)
            {
                if (type.Trim().ToLower() == "daily") schedule.ScheduleType = ScheduleType.Daily;
                else if (type.Trim().ToLower() == "weekly") schedule.ScheduleType = ScheduleType.Weekly;
                else if (type.Trim().ToLower() == "monthly") schedule.ScheduleType = ScheduleType.Monthly;

                try
                {
                    Save();

                    return true;
                }
                catch (Exception ex)
                {
                    _log.Error("Update(" + scheduleId + "): Unable to update schedule", ex);

                    return false;
                }
            }
            else return false;
        }

        public Boolean Delete(String scheduleId)
        {
            Schedule schedule = null;

            try
            {
                schedule = _context.Schedules.SingleOrDefault(s => s.ExternalId == scheduleId);
            }
            catch (Exception ex)
            {
                _log.Error("Delete(" + scheduleId + "): Unable to find schedule", ex);
            }

            if (schedule != null)
            {
                List<ScheduleParameter> scheduleParameters = schedule.ScheduleParameters.ToList();

                foreach (ScheduleParameter scheduleParameter in scheduleParameters)
                {
                    _context.ScheduleParameters.Remove(scheduleParameter);
                }

                List<ScheduleDetail> scheduleDetails = schedule.ScheduleDetails.ToList();

                foreach (ScheduleDetail scheduleDetail in scheduleDetails)
                {
                    _context.ScheduleDetails.Remove(scheduleDetail);
                }

                try
                {
                    Save();
                }
                catch (Exception ex)
                {
                    _log.Error("Delete(" + scheduleId + "): Unable to remove schedule metadata", ex);

                    return false;
                }

                _context.Schedules.Remove(schedule);

                try
                {
                    Save();

                    return true;
                }
                catch (Exception ex)
                {
                    _log.Error("Delete(" + scheduleId + "): Unable to remove schedule", ex);

                    return false;
                }
            }
            else return false;
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

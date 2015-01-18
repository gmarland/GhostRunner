using GhostRunner.DAL.Interface;
using GhostRunner.Models;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GhostRunner.DAL
{
    public class ScriptDataAccess : IScriptDataAccess
    {
        protected IContext _context;

        private static readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public ScriptDataAccess(IContext context)
        {
            _context = context;
        }

        public IList<Script> GetAll(long projectId)
        {
            try
            {
                return _context.Scripts.Where(s => s.Project.ID == projectId).ToList();
            }
            catch (Exception ex)
            {
                _log.Error("GetAll(" + projectId + "): Error retrieving all scripts", ex);

                return new List<Script>();
            }
        }

        public IList<Script> GetAll(long projectId, IList<long> scriptIds)
        {
            try
            {
                return _context.Scripts.Where(s => s.Project.ID == projectId && scriptIds.Contains(s.ID)).ToList();
            }
            catch (Exception ex)
            {
                _log.Error("GetAll(" + projectId + "): Error retrieving all {", ex);

                return new List<Script>();
            }
        }

        public Script Get(long scriptId)
        {
            try
            {
                return _context.Scripts.SingleOrDefault(s => s.ID == scriptId);
            }
            catch (Exception ex)
            {
                _log.Error("Get(" + scriptId + "): Error retrieving script", ex);

                return null;
            }
        }

        public Script Get(String scriptId)
        {
            try
            {
                return _context.Scripts.SingleOrDefault(s => s.ExternalId == scriptId);
            }
            catch (Exception ex)
            {
                _log.Error("Get(" + scriptId + "): Error retrieving script", ex);

                return null;
            }
        }

        public Script Insert(Script script)
        {
            try
            {
                _context.Scripts.Add(script);
                Save();

                return script;
            }
            catch (Exception ex)
            {
                _log.Error("Insert(): Unable to add new script", ex);

                return null;
            }
        }

        public Boolean Update(String scriptId, String name, String description, String content)
        {
            Script script = null;

            try
            {
                script = _context.Scripts.SingleOrDefault(i => i.ExternalId == scriptId);
            }
            catch (Exception ex)
            {
                _log.Error("Update(" + scriptId + "): Unable to get script", ex);

                return false;
            }

            if (script != null)
            {
                script.Name = name;
                script.Description = description;
                script.Content = content;

                try
                {
                    Save();

                    return true;
                }
                catch (Exception ex)
                {
                    _log.Error("Update(" + scriptId + "): Unable to save script", ex);

                    return false;
                }
            }
            else
            {
                _log.Error("Update(" + scriptId + "): Unable to get find script");

                return false;
            }
        }

        public Boolean Delete(String scriptId)
        {
            Script script = null;

            try
            {
                script = _context.Scripts.SingleOrDefault(i => i.ExternalId == scriptId);
            }
            catch (Exception ex)
            {
                _log.Error("Delete(" + scriptId + "): Unable to get script", ex);

                return false;
            }

            if (script != null)
            {
                List<Schedule> schedules = new List<Schedule>();

                try
                {
                    schedules = _context.Schedules.Where(s => s.ScheduleItemId == script.ID && s.ScheduleItemType == ItemType.Script).ToList();
                }
                catch (Exception ex)
                {
                    _log.Error("Delete(" + scriptId + "): Unable to get schedules", ex);

                    return false;
                }

                foreach (Schedule schedule in schedules)
                {
                    List<ScheduleDetail> scheduleDetails = schedule.ScheduleDetails.ToList();

                    foreach (ScheduleDetail scheduleDetail in scheduleDetails)
                    {
                        _context.ScheduleDetails.Remove(scheduleDetail);
                    }

                    List<ScheduleParameter> scheduleParameters = schedule.ScheduleParameters.ToList();

                    foreach (ScheduleParameter scheduleParameter in scheduleParameters)
                    {
                        _context.ScheduleParameters.Remove(scheduleParameter);
                    }

                    _context.Schedules.Remove(schedule);
                }

                try
                {
                    Save();
                }
                catch (Exception ex)
                {
                    _log.Error("Delete(): An error occured deleting the scruot schedules", ex);

                    return false;
                }

                // Remove the selected script
                _context.Scripts.Remove(script);

                try
                {
                    Save();
                }
                catch (Exception ex)
                {
                    _log.Error("Delete(): An error occured deleting the script", ex);

                    return false;
                }

                return true;
            }
            else
            {
                _log.Info("Delete(" + scriptId + "): Unable to find script");

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

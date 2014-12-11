using GhostRunner.DAL;
using GhostRunner.DAL.Interface;
using GhostRunner.Models;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GhostRunner.SL
{
    public class ScheduleService
    {
        #region Private Properties

        private static readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private IScheduleDataAccess _scheduleDataAccess;
        private IScheduleParameterDataAccess _scheduleParameterDataAccess;
        private IScheduleDetailDataAccess _scheduleDetailDataAccess;
        private IProjectDataAccess _projectDataAccess;
        private ISequenceDataAccess _sequenceDataAccess;
        private IScriptDataAccess _scriptDataAccess;

        #endregion

        #region Constructors

        public ScheduleService()
        {
            InitializeDataAccess(new GhostRunnerContext("DatabaseConnectionString"));
        }

        public ScheduleService(IContext context)
        {
            InitializeDataAccess(context);
        }

        #endregion

        public IList<IScheduleItem> GetAllSchedulesItems(String projectId)
        {
            Project project = _projectDataAccess.GetByExternalId(projectId);

            if (project != null)
            {
                IList<Schedule> schedules = _scheduleDataAccess.GetAll(project.ID);

                IList<Sequence> sequences = _sequenceDataAccess.GetAll(project.ID, schedules.Where(s => (s.ScheduleItemType == ItemType.Sequence)).Select(s => s.ScheduleItemId).ToList());
                IList<Script> scripts = _scriptDataAccess.GetAll(project.ID, schedules.Where(s => (s.ScheduleItemType == ItemType.Script)).Select(s => s.ScheduleItemId).ToList());
                
                List<IScheduleItem> scheduleItems = new List<IScheduleItem>();

                foreach (Schedule schedule in schedules)
                {
                    if (schedule.ScheduleItemType == ItemType.Sequence) scheduleItems.Add(new SequenceScheduleItem(schedule, sequences.SingleOrDefault(s => s.ID == schedule.ScheduleItemId)));
                    else if (schedule.ScheduleItemType == ItemType.Script) scheduleItems.Add(new ScriptScheduleItem(schedule, scripts.SingleOrDefault(s => s.ID == schedule.ScheduleItemId)));
                }

                return scheduleItems;
            }
            else
            {
                _log.Info("GetAllSchedulesItems(" + projectId + "): Unable to find project");

                return new List<IScheduleItem>();
            }
        }

        public Schedule InsertSchedule(String projectId, String type, String itemId, String itemType)
        {
            Project project = _projectDataAccess.GetByExternalId(projectId);

            if (project != null)
            {
                Schedule schedule = new Schedule();
                schedule.ExternalId = System.Guid.NewGuid().ToString();
                schedule.Project = project;

                if (type.Trim().ToLower() == "daily") schedule.ScheduleType = ScheduleType.Daily;
                else if (type.Trim().ToLower() == "weekly") schedule.ScheduleType = ScheduleType.Weekly;
                else if (type.Trim().ToLower() == "monthly") schedule.ScheduleType = ScheduleType.Monthly;

                if (itemType.Trim().ToLower() == "sequence") {
                    schedule.ScheduleItemType = ItemType.Sequence;

                    Sequence sequence = null;

                    try
                    {
                        sequence = _sequenceDataAccess.Get(itemId);
                    }
                    catch (Exception ex)
                    {
                        _log.Info("InsertSchedule(" + projectId + ", " + itemId + "): Error finding sequence", ex);
                    }

                    if (sequence == null) return null;

                    schedule.ScheduleItemType = ItemType.Sequence;
                    schedule.ScheduleItemId = sequence.ID;
                }
                else if (itemType.Trim().ToLower() == "script")
                {
                    schedule.ScheduleItemType = ItemType.Script;

                    Script script = null;

                    try
                    {
                        script = _scriptDataAccess.Get(itemId);
                    }
                    catch (Exception ex)
                    {
                        _log.Info("InsertSchedule(" + projectId + ", " + itemId + "): Error finding script", ex);
                    }

                    if (script == null) return null;

                    schedule.ScheduleItemType = ItemType.Script;
                    schedule.ScheduleItemId = script.ID;
                }

                return _scheduleDataAccess.Insert(schedule);
            }
            else
            {
                _log.Info("InsertSchedule(" + projectId + "): Unable to find project");

                return null;
            }
        }

        public ScheduleParameter InsertScheduleParameter(String scheduleId, String name, String value)
        {
            Schedule schedule = _scheduleDataAccess.Get(scheduleId);

            if (schedule != null)
            {
                ScheduleParameter scheduleParameter = new ScheduleParameter();
                scheduleParameter.Schedule = schedule;
                scheduleParameter.Name = name;
                scheduleParameter.Value = value;

                return _scheduleParameterDataAccess.Insert(scheduleParameter);
            }
            else
            {
                _log.Info("InsertScheduleParameter(" + scheduleId + "): Unable to find schedule");

                return null;
            }
        }

        public ScheduleDetail InsertScheduleDetail(String scheduleId, String name, String value)
        {
            Schedule schedule = _scheduleDataAccess.Get(scheduleId);

            if (schedule != null)
            {
                ScheduleDetail scheduleDetail = new ScheduleDetail();
                scheduleDetail.Schedule = schedule;
                scheduleDetail.Name = name;
                scheduleDetail.Value = value;

                return _scheduleDetailDataAccess.Insert(scheduleDetail);
            }
            else
            {
                _log.Info("InsertSchedulePaInsertScheduleDetailrameter(" + scheduleId + "): Unable to find schedule");

                return null;
            }
        }

        #region Private Methods

        private void InitializeDataAccess(IContext context)
        {
            _scheduleDataAccess = new ScheduleDataAccess(context);
            _scheduleParameterDataAccess = new ScheduleParameterDataAccess(context);
            _scheduleDetailDataAccess = new ScheduleDetailDataAccess(context);
            _projectDataAccess = new ProjectDataAccess(context);
            _sequenceDataAccess = new SequenceDataAccess(context);
            _scriptDataAccess = new ScriptDataAccess(context);
        }

        #endregion
    }
}

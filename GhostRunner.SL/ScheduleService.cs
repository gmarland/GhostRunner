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

        #region Private Methods

        private void InitializeDataAccess(IContext context)
        {
            _scheduleDataAccess = new ScheduleDataAccess(context);
            _projectDataAccess = new ProjectDataAccess(context);
            _sequenceDataAccess = new SequenceDataAccess(context);
            _scriptDataAccess = new ScriptDataAccess(context);
        }

        #endregion
    }
}

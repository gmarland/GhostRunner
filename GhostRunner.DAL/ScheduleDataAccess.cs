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
    }
}

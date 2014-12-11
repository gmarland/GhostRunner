using GhostRunner.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GhostRunner.DAL.Interface
{
    public interface IScheduleParameterDataAccess
    {
        ScheduleParameter Insert(ScheduleParameter scheduleParameter);
    }
}

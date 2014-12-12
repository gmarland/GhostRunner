using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GhostRunner.Models
{
    public interface IContext
    {
        IDbSet<User> Users { get; set; }

        IDbSet<Project> Projects { get; set; }

        IDbSet<Sequence> Sequences { get; set; }

        IDbSet<SequenceScript> SequenceScripts { get; set; }

        IDbSet<ScheduleDetail> ScheduleDetails { get; set; }

        IDbSet<Script> Scripts { get; set; }

        IDbSet<Task> Tasks { get; set; }

        IDbSet<TaskScript> TaskScripts { get; set; }

        IDbSet<TaskScriptParameter> TaskScriptParameters { get; set; }

        IDbSet<Schedule> Schedules { get; set; }

        IDbSet<ScheduleParameter> ScheduleParameters { get; set; }

        int SaveChanges();
    }
}

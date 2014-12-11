using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GhostRunner.Models
{
    public interface IScheduleItem
    {
        ItemType Type { get; }

        String ExternalId { get; }

        Project Project { get; }

        String Name { get; }

        ScheduleType ScheduleType { get; }

        String ScheduleDetail { get; }

        IList<ScheduleDetail> ScheduleDetails { get; }
    }
}

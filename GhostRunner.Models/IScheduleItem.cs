using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GhostRunner.Models
{
    public interface IScheduleItem
    {
        ItemType Type { get; set; }

        String Name { get; set; }

        ScheduleType ScheduleType { get; set; }

        String ScheduleDetail { get; set; }
    }
}

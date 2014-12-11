using GhostRunner.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GhostRunner.ViewModels.Schedules.Partials
{
    public class EditScheduledItemModel
    {
        public User User { get; set; }

        public Project Project { get; set; }

        public IScheduleItem ScheduleItem { get; set; }

        public String GetScheduledHour()
        {
            ScheduleDetail detail = ScheduleItem.ScheduleDetails.SingleOrDefault(sd => sd.Name == "hour");

            if (detail != null) return detail.Value;
            else return "0";
        }

        public String GetScheduledMinute()
        {
            ScheduleDetail detail = ScheduleItem.ScheduleDetails.SingleOrDefault(sd => sd.Name == "minute");

            if (detail != null) return detail.Value;
            else return "0";
        }
    }
}
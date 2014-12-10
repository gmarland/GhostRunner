using GhostRunner.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GhostRunner.Models
{
    public class ScheduleItem
    {
        private Schedule _schedule;

        public ScheduleItem(Schedule schedule)
        {
            _schedule = schedule;
        }

        public ScheduleType ScheduleType
        {
            get
            {
                return _schedule.ScheduleType;
            }
        }

        public string ScheduleDetail
        {
            get
            {
                if (_schedule.ScheduleType == ScheduleType.Monthly)
                {
                    List<int> dateParameters = _schedule.ScheduleParameters.Where(sp => sp.Name.Trim().ToLower() == "date").Select(sp => Int32.Parse(sp.Value)).OrderBy(sp => sp).ToList();
                    List<String> timeParameters = _schedule.ScheduleParameters.Where(sp => sp.Name.Trim().ToLower() == "time").Select(sp => sp.Value).ToList();

                    List<String> dateOrdinals = new List<String>();

                    foreach (int dateParameter in dateParameters)
                    {
                        dateOrdinals.Add(DateHelper.ToDateOrdinal(dateParameter));
                    }

                    return "Monthly, every " + String.Join(", ", dateOrdinals) + " at " + timeParameters.First();
                }
                else if (_schedule.ScheduleType == ScheduleType.Weekly)
                {
                    List<String> dayParameters = _schedule.ScheduleParameters.Where(sp => sp.Name.Trim().ToLower() == "day").Select(sp => sp.Value).ToList();
                    List<String> timeParameters = _schedule.ScheduleParameters.Where(sp => sp.Name.Trim().ToLower() == "time").Select(sp => sp.Value).ToList();

                    return "Weekly, every " + String.Join(", ", dayParameters) + " at " + timeParameters.First();
                }

                else if (_schedule.ScheduleType == ScheduleType.Daily)
                {
                    List<String> timeParameters = _schedule.ScheduleParameters.Where(sp => sp.Name.Trim().ToLower() == "time").Select(sp => sp.Value).ToList();

                    return "Daily, at " + String.Join(", ", timeParameters);
                }

                return "Unknown";
            }
        }
    }
}

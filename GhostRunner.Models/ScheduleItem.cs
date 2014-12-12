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

        public String ExternalId
        {
            get
            {
                return _schedule.ExternalId;
            }
        }

        public Project Project
        {
            get
            {
                return _schedule.Project;
            }
        }

        public ScheduleType ScheduleType
        {
            get
            {
                return _schedule.ScheduleType;
            }
        }

        public IList<ScheduleDetail> ScheduleDetails
        { 
            get
            {
                return _schedule.ScheduleDetails.ToList();
            }
        }

        public string ScheduleDetail
        {
            get
            {
                List<String> hourParameters = _schedule.ScheduleDetails.Where(sp => sp.Name.Trim().ToLower() == "hour").Select(sp => sp.Value).ToList();
                List<String> minuteParameters = _schedule.ScheduleDetails.Where(sp => sp.Name.Trim().ToLower() == "minute").Select(sp => sp.Value).ToList();

                if (_schedule.ScheduleType == ScheduleType.Monthly)
                {
                    List<int> dateParameters = _schedule.ScheduleDetails.Where(sp => sp.Name.Trim().ToLower() == "date").Select(sp => Int32.Parse(sp.Value)).OrderBy(sp => sp).ToList();

                    List<String> dateOrdinals = new List<String>();

                    foreach (int dateParameter in dateParameters)
                    {
                        dateOrdinals.Add(DateHelper.ToDateOrdinal(dateParameter));
                    }

                    if ((hourParameters.Count > 0) && (minuteParameters.Count > 0)) return "Monthly, every " + String.Join(", ", dateOrdinals) + " at " + hourParameters.First().PadLeft(2, '0') + ":" + minuteParameters.First().PadLeft(2, '0');
                    else return "Monthly, every " + String.Join(", ", dateOrdinals);
                }
                else if (_schedule.ScheduleType == ScheduleType.Weekly)
                {
                    List<String> dayParameters = _schedule.ScheduleDetails.Where(sp => sp.Name.Trim().ToLower() == "day").Select(sp => sp.Value).ToList();

                    if ((hourParameters.Count > 0) && (minuteParameters.Count > 0)) return "Weekly, every " + String.Join(", ", dayParameters) + " at " + hourParameters.First().PadLeft(2, '0') + ":" + minuteParameters.First().PadLeft(2, '0');
                    else return "Weekly, every " + String.Join(", ", dayParameters);
                }

                else if (_schedule.ScheduleType == ScheduleType.Daily)
                {
                    if ((hourParameters.Count > 0) && (minuteParameters.Count > 0)) return "Daily, at " + hourParameters.First().PadLeft(2, '0') + ":" + minuteParameters.First().PadLeft(2, '0');
                    else return "Daily";
                }

                return "Unknown";
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GhostRunner.Models
{
    public class SequenceScheduleItem : ScheduleItem, IScheduleItem
    {
        private Sequence _sequence;

        public SequenceScheduleItem(Schedule schedule, Sequence sequence)
            : base(schedule)
        {
            _sequence = sequence;
        }

        public ItemType Type
        {
            get
            {
                return ItemType.Sequence;
            }
        }

        public string Name
        {
            get
            {
                return _sequence.Name;
            }
        }
    }
}

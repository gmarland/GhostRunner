using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GhostRunner.Models
{
    public class ScriptScheduleItem : ScheduleItem, IScheduleItem
    {
        private Script _script;

        public ScriptScheduleItem(Schedule schedule, Script script)
            : base(schedule)
        {
            _script = script;
        }

        public ItemType Type
        {
            get
            {
                return ItemType.Script;
            }
        }

        public string Name
        {
            get
            {
                return _script.Name;
            }
        }
    }
}

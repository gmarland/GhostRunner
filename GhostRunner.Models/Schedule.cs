using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GhostRunner.Models
{
    public class Schedule
    {
        [Required]
        public int ID { get; set; }

        [Required, MaxLength(38)]
        public String ExternalId { get; set; }

        [Required]
        public ScheduleType ScheduleType { get; set; }

        [Required]
        public int ScheduleItemId { get; set; }

        [Required]
        public ItemType ScheduleItemType { get; set; }

        public virtual ICollection<ScheduleParameter> ScheduleParameters { get; set; }
    }
}

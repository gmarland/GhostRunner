using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GhostRunner.Models
{
    public class ScheduleParameter
    {
        [Required]
        public int ID { get; set; }

        [Required]
        public String Name { get; set; }

        public String Value { get; set; }

        public virtual Schedule Schedule { get; set; }
    }
}

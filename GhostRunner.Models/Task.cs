using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace GhostRunner.Models
{
    public class Task
    {
        [Required]
        public int ID { get; set; }

        [Required, MaxLength(38)]
        public String ExternalId { get; set; }

        [Required(ErrorMessage = " * Required")]
        public String Name { get; set; }

        [Required]
        public Status Status { get; set; }

        [Required]
        public int ParentId { get; set; }

        [Required]
        public ItemType ParentType { get; set; }

        [Required]
        public DateTime Created { get; set; }

        public DateTime? Started { get; set; }

        public DateTime? Completed { get; set; }

        public virtual User User { get; set; }

        public virtual Project Project { get; set; }

        public virtual ICollection<TaskScript> TaskScripts { get; set; }

        public IList<TaskScript> GetCompletedTaskScripts()
        {
            return TaskScripts.Where(ts => !String.IsNullOrEmpty(ts.Log)).ToList();
        }
    }
}

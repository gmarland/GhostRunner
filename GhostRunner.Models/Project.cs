using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GhostRunner.Models
{
    public class Project
    {
        [Required]
        public long ID { get; set; }

        [Required, MaxLength(38)]
        public String ExternalId { get; set; }

        [Required(ErrorMessage = " * Required")]
        public String Name { get; set; }
        
        public DateTime Created { get; set; }

        public virtual User User { get; set; }

        public virtual ICollection<Sequence> Sequences { get; set; }

        public virtual ICollection<Script> Scripts { get; set; }
    }
}

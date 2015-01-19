using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GhostRunner.Models
{
    public class PackageCache
    {
        [Required]
        public long ID { get; set; }

        [Required, MaxLength(38)]
        public String ExternalId { get; set; }

        [Required(ErrorMessage = " * Required")]
        public String Name { get; set; }

        [Required]
        public String Version { get; set; }

        [Required]
        public Boolean Store { get; set; }

        public virtual Project Project { get; set; }
    }
}

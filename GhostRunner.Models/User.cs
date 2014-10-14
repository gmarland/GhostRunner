using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace GhostRunner.Models
{
    public class User
    {
        [Required]
        public int ID { get; set; }

        [Required, MaxLength(38)]
        public String ExternalId { get; set; }

        public String SessionId { get; set; }

        [Required(ErrorMessage = " * Required"), MaxLength(250)]
        public String Name { get; set; }

        [Required(ErrorMessage = " * Required"), MaxLength(250)]
        public String Email { get; set; }

        [Required(ErrorMessage = " * Required")]
        [StringLength(100, ErrorMessage = " * Requires at least {2} characters", MinimumLength = 8)]
        public String Password { get; set; }

        [Required]
        public Boolean IsAdminstrator { get; set; }

        [Required]
        public DateTime Created { get; set; }

        public virtual ICollection<Project> Projects { get; set; }
    }
}

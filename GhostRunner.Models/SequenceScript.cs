using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GhostRunner.Models
{
    public class SequenceScript
    {
        [Required]
        public int ID { get; set; }

        [Required]
        public int Position { get; set; }

        public virtual Sequence Sequence { get; set; }

        public virtual Script Script { get; set; }
    }
}

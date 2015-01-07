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

        [Required, MaxLength(38)]
        public String ExternalId { get; set; }

        [Required]
        public String Name { get; set; }

        [Required]
        public String Content { get; set; }

        [Required]
        public ScriptType Type { get; set; }

        [Required]
        public int Position { get; set; }

        public virtual Sequence Sequence { get; set; }

        public String GetHTMLFormattedContent()
        {
            if (!String.IsNullOrEmpty(Content)) return Content.Replace(Environment.NewLine, "<br/>").Replace("\t", "&nbsp;&nbsp;&nbsp;&nbsp;");
            else return String.Empty;
        }
    }
}

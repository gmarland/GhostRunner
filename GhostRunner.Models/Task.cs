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
        
        public String Content { get; set; }

        [Required]
        public Status Status { get; set; }

        [Required]
        public int ParentId { get; set; }

        [Required]
        public ParentType ParentType { get; set; }

        public String Log { get; set; }

        public String PhantomScript { get; set; }

        [Required]
        public DateTime Created { get; set; }

        public DateTime? Started { get; set; }

        public DateTime? Completed { get; set; }

        public virtual User User { get; set; }

        public virtual ICollection<TaskParameter> TaskParameters { get; set; }

        public String GetHTMLFormattedContent()
        {
            if (!String.IsNullOrEmpty(Content)) return Content.Replace(Environment.NewLine, "<br/>").Replace("\t", "&nbsp;&nbsp;&nbsp;&nbsp;");
            else return String.Empty;
        }

        public String GetHTMLFormattedPhantomScript()
        {
            if (!String.IsNullOrEmpty(PhantomScript)) return PhantomScript.Replace(Environment.NewLine, "<br/>").Replace("\t", "&nbsp;&nbsp;&nbsp;&nbsp;");
            else return String.Empty;
        }

        public String GetHTMLFormattedLogScript()
        {
            if (!String.IsNullOrEmpty(Log)) return Log.Replace(Environment.NewLine, "<br/>").Replace("\t", "&nbsp;&nbsp;&nbsp;&nbsp;");
            else return String.Empty;
        }
    }
}

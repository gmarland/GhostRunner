using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GhostRunner.Models
{
    public class TaskScript
    {
        [Required]
        public int ID { get; set; }

        public String Content { get; set; }

        public String PhantomScript { get; set; }

        public String Log { get; set; }

        public virtual Task Task { get; set; }

        public virtual ICollection<TaskScriptParameter> TaskScriptParameters { get; set; }

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

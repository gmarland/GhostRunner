using Newtonsoft.Json;
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
        public long ID { get; set; }

        public String Content { get; set; }

        public ScriptType Type { get; set; }

        public String Log { get; set; }

        [Required]
        public int Position { get; set; }

        public virtual Task Task { get; set; }

        public virtual ICollection<TaskScriptParameter> TaskScriptParameters { get; set; }

        public String GetJSONVariable(String variableName)
        {
            try
            {
                Dictionary<String, String> contentOptions = JsonConvert.DeserializeObject<Dictionary<String, String>>(Content);

                if (contentOptions.ContainsKey(variableName)) return contentOptions[variableName];
                else return String.Empty;
            }
            catch(Exception)
            {
                return String.Empty;
            }
        }

        public String GetHTMLFormattedContent()
        {
            if (!String.IsNullOrEmpty(Content)) return Content.Replace(Environment.NewLine, "<br/>").Replace("\t", "&nbsp;&nbsp;&nbsp;&nbsp;").Replace(" ", "&nbsp;");
            else return String.Empty;
        }

        public String GetHTMLFormattedLogScript()
        {
            if (!String.IsNullOrEmpty(Log)) return Log.Replace(Environment.NewLine, "<br/>").Replace("\t", "&nbsp;&nbsp;&nbsp;&nbsp;").Replace(" ", "&nbsp;");
            else return String.Empty;
        }
    }
}

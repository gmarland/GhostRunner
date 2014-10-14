using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace GhostRunner.Models
{
    public class Script
    {
        [Required]
        public int ID { get; set; }

        [Required, MaxLength(38)]
        public String ExternalId { get; set; }

        [Required(ErrorMessage = " * Required")]
        public String Name { get; set; }

        public String Description { get; set; }

        public String Content { get; set; }

        public virtual Project Project { get; set; }

        public virtual ICollection<Task> Tasks { get; set; }

        public String[] GetAllParameters()
        {
            Regex parameterMatches = new Regex(@"(\[.*?\])");

            if ((!String.IsNullOrEmpty(Content)) && (parameterMatches.IsMatch(Content)))
            {
                List<String> matches = new List<String>();

                foreach (Match match in parameterMatches.Matches(Content))
                {
                    matches.Add(match.Value.TrimStart(new char[] { '[' }).TrimEnd(new char[] { ']' }));
                }

                return matches.Distinct().ToArray();
            }
            else
            {
                return new String[0];
            }
        }
    }
}

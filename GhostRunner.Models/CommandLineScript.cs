using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace GhostRunner.Models
{
    public class CommandLineScript : IGhostRunnerScript
    {
        private Script _script;
        private SequenceScript _sequenceScript;

        public CommandLineScript()
        {
            _script = new Script();
            _sequenceScript = new SequenceScript();
        }

        public CommandLineScript(Script script)
        {
            _script = script;
            _sequenceScript = null;
        }

        public CommandLineScript(SequenceScript sequenceScript)
        {
            _script = null;
            _sequenceScript = sequenceScript;
        }

        public string ExternalId
        {
            get 
            {
                if (_script != null) return _script.ExternalId;
                else if (_sequenceScript != null) return _sequenceScript.ExternalId;
                else return String.Empty;
            }
        }

        public string Name
        {
            get 
            {
                if (_script != null) return _script.Name;
                else if (_sequenceScript != null) return _sequenceScript.Name;
                else return String.Empty;
            }
        }

        public string Description
        {
            get
            {
                if (_script != null) return _script.Description;
                else return String.Empty;
            }
        }

        public string Content
        {
            get
            {
                if (_script != null) return _script.Content;
                else if (_sequenceScript != null) return _sequenceScript.Content;
                else return String.Empty;
            }
        }

        public ScriptType Type
        {
            get
            {
                return ScriptType.CommandLine;
            }
        }

        public Project Project
        {
            get
            {
                if (_script != null) return _script.Project;
                else if (_sequenceScript != null) return _sequenceScript.Sequence.Project;
                else return null;
            }
        }

        public Boolean HasParameters()
        {
            return GetAllParameters().Length > 0;
        }

        public String[] GetAllParameters()
        {
            String content = String.Empty;

            if (_script != null) content = _script.Content;
            else if (_sequenceScript != null) content = _sequenceScript.Content;

            Regex parameterMatches = new Regex(@"(\[\[.*?\]\])");

            if ((!String.IsNullOrEmpty(content)) && (parameterMatches.IsMatch(content)))
            {
                List<String> matches = new List<String>();

                foreach (Match match in parameterMatches.Matches(content))
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

        public String GetHTMLFormattedContent()
        {
            String content = String.Empty;

            if (_script != null) content = _script.Content;
            else if (_sequenceScript != null) content = _sequenceScript.Content;

            if (!String.IsNullOrEmpty(content)) return content.Replace(Environment.NewLine, "<br/>").Replace("\t", "&nbsp;&nbsp;&nbsp;&nbsp;");
            else return String.Empty;
        }
    }
}

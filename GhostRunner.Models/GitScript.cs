using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace GhostRunner.Models
{
    public class GitScript : IGhostRunnerScript
    {
        private Script _script;
        private SequenceScript _sequenceScript;

        private String _location = String.Empty;
        private String _username = String.Empty;
        private String _password = String.Empty;

        public GitScript()
        {
            _script = new Script();
            _sequenceScript = new SequenceScript();
        }

        public GitScript(Script script)
        {
            _script = script;
            _sequenceScript = null;

            Dictionary<String, String> scriptOptions = JsonConvert.DeserializeObject<Dictionary<String, String>>(script.Content);

            if (scriptOptions.ContainsKey("Location")) _location = scriptOptions["Location"];
            if (scriptOptions.ContainsKey("Username")) _username = scriptOptions["Username"];
            if (scriptOptions.ContainsKey("Password")) _password = scriptOptions["Password"];
        }

        public GitScript(SequenceScript sequenceScript)
        {
            _script = null;
            _sequenceScript = sequenceScript;

            Dictionary<String, String> scriptOptions = JsonConvert.DeserializeObject<Dictionary<String, String>>(_sequenceScript.Content);

            if (scriptOptions.ContainsKey("Location")) _location = scriptOptions["Location"];
            if (scriptOptions.ContainsKey("Username")) _username = scriptOptions["Username"];
            if (scriptOptions.ContainsKey("Password")) _password = scriptOptions["Password"];
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

        public string Location
        {
            get
            {
                return _location;
            }
        }

        public string Username
        {
            get
            {
                return _username;
            }
        }

        public string Password
        {
            get
            {
                return _password;
            }
        }

        public ScriptType Type
        {
            get
            {
                return ScriptType.Git;
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
            return false;
        }

        public String[] GetAllParameters()
        {
            return new String[0];
        }
    }
}

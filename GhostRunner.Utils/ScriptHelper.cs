using GhostRunner.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GhostRunner.Utils
{
    public class ScriptHelper
    {
        #region Public Static Methods

        public static IGhostRunnerScript GetGhostRunnerScript(Script script)
        {
            switch (script.Type)
            {
                case ScriptType.Git: return new GitScript(script);
                case ScriptType.CommandLine: return new CommandLineScript(script);
                case ScriptType.Node: return new NodeScript(script);
                case ScriptType.Grunt: return new GruntScript(script);
                case ScriptType.PhantomJS: return new PhantomJSScript(script);
            }

            return null;
        }

        public static IGhostRunnerScript GetGhostRunnerScript(SequenceScript sequenceScript)
        {
            switch (sequenceScript.Type)
            {
                case ScriptType.Git: return new GitScript(sequenceScript);
                case ScriptType.CommandLine: return new CommandLineScript(sequenceScript);
                case ScriptType.Node: return new NodeScript(sequenceScript);
                case ScriptType.Grunt: return new GruntScript(sequenceScript);
                case ScriptType.PhantomJS: return new PhantomJSScript(sequenceScript);
            }

            return null;
        }

        public static ScriptType GetScriptType(String scriptType)
        {
            switch (scriptType.Trim().ToLower())
            {
                case "git": return ScriptType.Git;
                case "commandline": return ScriptType.CommandLine;
                case "node": return ScriptType.Node;
                case "grunt": return ScriptType.Grunt;
                case "phantomjs": return ScriptType.PhantomJS;
            }

            return ScriptType.CommandLine;
        }

        public static IGhostRunnerScript GetNewScriptObject(String scriptType)
        {
            switch (scriptType.Trim().ToLower())
            {
                case "git": return new GitScript();
                case "commandline": return new CommandLineScript();
                case "node": return new NodeScript();
                case "grunt": return new GruntScript();
                case "phantomjs": return new PhantomJSScript();
            }

            return new CommandLineScript();
        }

        public static String GetReadableScriptType(String scriptType)
        {
            switch (scriptType.Trim().ToLower())
            {
                case "git": return "Git";
                case "commandline": return "Command Line";
                case "node": return "Node";
                case "grunt": return "Grunt";
                case "phantomjs": return "PhantomJS";
            }

            return String.Empty;
        }

        #endregion
    }
}

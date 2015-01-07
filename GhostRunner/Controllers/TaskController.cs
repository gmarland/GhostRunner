using GhostRunner.Models;
using GhostRunner.SL;
using GhostRunner.Utils;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GhostRunner.Controllers
{
    public class TaskController : ApiController
    {
        private ScriptService _scriptService = new ScriptService();
        private SequenceService _sequenceService = new SequenceService();
        private TaskService _taskService = new TaskService();

        // GET get task parameters
        public String[] Get(String id)
        {
            Script script = _scriptService.GetScript(id);

            if (script != null)
            {
                IGhostRunnerScript ghostRunnerScript = ScriptHelper.GetGhostRunnerScript(script);

                if (ghostRunnerScript.HasParameters()) return ghostRunnerScript.GetAllParameters();
                else return new String[0];
            }
            else return new String[0];
        }

        // POST create a new task
        public String Post(String id, JObject parameters)
        {
            String itemId = String.Empty;

            Script script = _scriptService.GetScript(id);

            if (script != null)
            {
                IGhostRunnerScript ghostRunnerScript = ScriptHelper.GetGhostRunnerScript(script);

                IList<TaskScriptParameter> taskParameters = new List<TaskScriptParameter>();

                if (ghostRunnerScript.HasParameters())
                {
                    foreach (String parameter in ghostRunnerScript.GetAllParameters())
                    {
                        if (parameters[parameter] != null)
                        {
                            TaskScriptParameter taskParameter = new TaskScriptParameter();
                            taskParameter.Name = parameter;
                            taskParameter.Value = parameters[parameter].ToString();

                            taskParameters.Add(taskParameter);
                        }

                    }
                }

                Task task = _taskService.InsertScriptTask(id, script.Name, taskParameters);
                
                if (task != null) return task.ExternalId;
                else return string.Empty;

                return String.Empty;
            }
            else
            {
                Sequence sequence = _sequenceService.GetSequence(id);

                if (sequence != null)
                {
                    Task task = _taskService.InsertSequenceTask(id, sequence.Name);

                    if (task != null) return task.ExternalId;
                    else return string.Empty;
                }
                else return String.Empty;
            }

            return String.Empty;
        }
    }
}
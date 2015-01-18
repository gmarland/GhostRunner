using GhostRunner.Models;
using GhostRunner.SL;
using GhostRunner.Utils;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GhostRunner.Controllers
{
    public class TaskController : ApiController
    {
        private ScriptService _scriptService;
        private SequenceService _sequenceService;
        private TaskService _taskService;

        public TaskController()
        {
            _scriptService = new ScriptService(new GhostRunnerContext(ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString));
            _sequenceService = new SequenceService(new GhostRunnerContext(ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString));
            _taskService = new TaskService(new GhostRunnerContext(ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString));
        }

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
        }
    }
}
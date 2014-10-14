using GhostRunner.DAL.Interface;
using GhostRunner.Models;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GhostRunner.DAL
{
    public class ScriptDataAccess : IScriptDataAccess
    {
        protected GhostRunnerContext _context;

        private static readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public ScriptDataAccess(GhostRunnerContext context)
        {
            _context = context;
        }

        public IList<Script> GetAll(int projectId)
        {
            try
            {
                return _context.Scripts.Where(d => d.Project.ID == projectId).ToList();
            }
            catch (Exception ex)
            {
                _log.Error("GetAll(" + projectId + "): Error retrieving all scripts", ex);

                return new List<Script>();
            }
        }

        public Script Get(String scriptId)
        {
            try
            {
                return _context.Scripts.SingleOrDefault(s => s.ExternalId == scriptId);
            }
            catch (Exception ex)
            {
                _log.Error("Get(" + scriptId + "): Error retrieving script", ex);

                return null;
            }
        }

        public Script Insert(Script script)
        {
            try
            {
                _context.Scripts.Add(script);
                Save();

                return script;
            }
            catch (Exception ex)
            {
                _log.Error("Insert(): Unable to add new script", ex);

                return null;
            }
        }

        public Boolean Update(String scriptId, String name, String description, String content)
        {
            Script script = null;

            try
            {
                script = _context.Scripts.SingleOrDefault(i => i.ExternalId == scriptId);
            }
            catch (Exception ex)
            {
                _log.Error("Update(" + scriptId + "): Unable to get script", ex);

                return false;
            }

            if (script != null)
            {
                script.Name = name;
                script.Description = description;
                script.Content = content;

                try
                {
                    Save();

                    return true;
                }
                catch (Exception ex)
                {
                    _log.Error("Update(" + scriptId + "): Unable to save script", ex);

                    return false;
                }
            }
            else
            {
                _log.Error("Update(" + scriptId + "): Unable to get find script");

                return false;
            }
        }

        public Boolean Delete(String scriptId)
        {
            Script script = null;

            try
            {
                script = _context.Scripts.SingleOrDefault(i => i.ExternalId == scriptId);
            }
            catch (Exception ex)
            {
                _log.Error("Delete(" + scriptId + "): Unable to get script", ex);

                return false;
            }

            if (script != null)
            {
                // Remove the task parameters associated to the seleted script
                foreach (TaskParameter taskParameter in _context.TaskParameters.Where(tp => tp.Task.Script.ExternalId == scriptId))
                {
                    _context.TaskParameters.Remove(taskParameter);
                }

                // Remove the task parameters associated to the seleted script
                foreach (Task task in _context.Tasks.Where(t => t.Script.ExternalId == scriptId))
                {
                    _context.Tasks.Remove(task);
                }

                // Remove the selected script
                _context.Scripts.Remove(script);

                try
                {
                    _context.SaveChanges();
                }
                catch (Exception ex)
                {
                    _log.Error("Delete(): An error occured deleting the script", ex);

                    return false;
                }

                return true;
            }
            else
            {
                _log.Info("Delete(" + scriptId + "): Unable to find script");

                return false;
            }
        }

        private void Save()
        {
            try
            {
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

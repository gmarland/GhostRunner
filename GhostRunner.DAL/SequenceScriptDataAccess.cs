using GhostRunner.DAL.Interface;
using GhostRunner.Models;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GhostRunner.DAL
{
    public class SequenceScriptDataAccess : ISequenceScriptDataAccess
    {
        protected IContext _context;

        private static readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public SequenceScriptDataAccess(IContext context)
        {
            _context = context;
        }

        public IList<SequenceScript> GetAll(String sequenceId)
        {
            try
            {
                return _context.SequenceScripts.Where(ss => ss.Sequence.ExternalId == sequenceId).ToList();
            }
            catch (Exception ex)
            {
                _log.Error("GetAll(" + sequenceId + "): Error retrieving sequence scriptss");
                
                return new List<SequenceScript>();
            }
        }

        public int GetNextPosition(string sequenceId)
        {
            try
            {
                return (_context.SequenceScripts.Where(ss => ss.Sequence.ExternalId == sequenceId).Select(ss => ss.Position).Max() + 1);
            }
            catch (Exception ex)
            {
                _log.Error("GetNextPosition(" + sequenceId + "): Errror getting next position");

                return -1;
            }
        }

        public SequenceScript Insert(SequenceScript sequenceScript)
        {
            try
            {
                _context.SequenceScripts.Add(sequenceScript);
                Save();

                return sequenceScript;
            }
            catch (Exception ex)
            {
                _log.Error("Insert(): Error inserting new sequence script", ex);

                return null;
            }
        }

        public Boolean UpdateScriptOrder(String sequenceId)
        {
            SequenceScript[] sequenceScripts = _context.SequenceScripts.Where(ss => ss.Sequence.ExternalId == sequenceId).OrderBy(ss => ss.Position).ToArray();

            for (int i = 0; i < sequenceScripts.Length; i++)
            {
                sequenceScripts[i].Position = (i + 1);
            }

            try
            {
                Save();

                return true;
            }
            catch (Exception ex)
            {
                _log.Error("UpdateScriptOrder(" + sequenceId + "): Error updating sequence script", ex);

                return false;
            }
        }

        public Boolean UpdateScriptSequenceOrder(String sequenceScriptId, int position)
        {
            SequenceScript sequenceScript = _context.SequenceScripts.SingleOrDefault(ss => ss.ExternalId == sequenceScriptId);

            if (sequenceScript != null)
            {
                sequenceScript.Position = position;

                try
                {
                    Save();

                    return true;
                }
                catch (Exception ex)
                {
                    _log.Error("UpdateScriptOrder(" + sequenceScriptId + ", " + position + "): Error updating sequence script", ex);

                    return false;
                }
            }
            else return false;
        }

        public Boolean Delete(String sequenceId, String scriptId, int position)
        {
            SequenceScript[] sequenceScripts = _context.SequenceScripts.Where(ss => ss.Sequence.ExternalId == sequenceId && ss.Script.ExternalId == scriptId && ss.Position == position).ToArray();

            foreach (SequenceScript sequenceScript in sequenceScripts)
            {
                _context.SequenceScripts.Remove(sequenceScript);
            }

            try
            {
                Save();

                return true;
            }
            catch (Exception ex)
            {
                _log.Error("Delete(" + sequenceId + ", " + scriptId + ", " + position + "): Error deleting a sequence script", ex);

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

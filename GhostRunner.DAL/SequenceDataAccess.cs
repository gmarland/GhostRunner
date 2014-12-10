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
    public class SequenceDataAccess : ISequenceDataAccess
    {
        protected IContext _context;

        private static readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public SequenceDataAccess(IContext context)
        {
            _context = context;
        }

        public IList<Sequence> GetAll(int projectId)
        {
            try
            {
                return _context.Sequences.Where(s => s.Project.ID == projectId).ToList();
            }
            catch (Exception ex)
            {
                _log.Error("GetAll(" + projectId + "): Error retrieving all sequences", ex);

                return new List<Sequence>();
            }
        }

        public IList<Sequence> GetAll(int projectId, IList<int> sequenceIds)
        {
            try
            {
                return _context.Sequences.Where(s => s.Project.ID == projectId && sequenceIds.Contains(s.ID)).ToList();
            }
            catch (Exception ex)
            {
                _log.Error("GetAll(" + projectId + "): Error retrieving all sequences", ex);

                return new List<Sequence>();
            }
        }

        public Sequence Get(String sequenceId)
        {
            try
            {
                return _context.Sequences.SingleOrDefault(s => s.ExternalId == sequenceId);
            }
            catch (Exception ex)
            {
                _log.Error("Get(" + sequenceId + "): Error retrieving sequence", ex);

                return null;
            }
        }

        public Sequence Insert(Sequence sequence)
        {
            try
            {
                _context.Sequences.Add(sequence);
                Save();

                return sequence;
            }
            catch (Exception ex)
            {
                _log.Error("Insert(): Unable to add new Sequence", ex);

                return null;
            }
        }

        public Boolean Update(String sequenceId, String name, String description)
        {
            Sequence sequence = null;

            try
            {
                sequence = _context.Sequences.SingleOrDefault(s => s.ExternalId == sequenceId);
            }
            catch (Exception ex)
            {
                _log.Error("Update(" + sequenceId + "): Unable to find sequence", ex);

                return false;
            }

            if (sequence != null)
            {
                try
                {
                    sequence.Name = name;
                    sequence.Description = description;

                    Save();

                    return true;
                }
                catch (Exception ex)
                {
                    _log.Error("Update(" + sequenceId + "): Unable to update Sequence", ex);

                    return false;
                }
            }
            else return false;
        }

        public bool Delete(string sequenceId)
        {
            Sequence sequence = null;

            try
            {
                sequence = _context.Sequences.SingleOrDefault(i => i.ExternalId == sequenceId);
            }
            catch (Exception ex)
            {
                _log.Error("Delete(" + sequenceId + "): Unable to get sequence", ex);

                return false;
            }

            if (sequence != null)
            {
                List<SequenceScript> sequenceScripts = sequence.SequenceScripts.ToList();

                foreach (SequenceScript sequenceScript in sequenceScripts)
                {
                    _context.SequenceScripts.Remove(sequenceScript);
                }

                // Remove the selected script
                _context.Sequences.Remove(sequence);

                try
                {
                    _context.SaveChanges();
                }
                catch (Exception ex)
                {
                    _log.Error("Delete(): An error occured deleting the sequence", ex);

                    return false;
                }

                return true;
            }
            else
            {
                _log.Info("Delete(" + sequenceId + "): Unable to find sequence");

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

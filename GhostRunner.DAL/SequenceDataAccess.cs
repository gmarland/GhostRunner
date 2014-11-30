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

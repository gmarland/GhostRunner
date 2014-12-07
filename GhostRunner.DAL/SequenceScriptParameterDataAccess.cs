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
    public class SequenceScriptParameterDataAccess : ISequenceScriptParameterDataAccess
    {
        protected IContext _context;

        private static readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public SequenceScriptParameterDataAccess(IContext context)
        {
            _context = context;
        }

        public IList<SequenceScriptParameter> GetAll(int sequenceScriptId)
        {
            try
            {
                return _context.SequenceScriptParameters.Where(ssp => ssp.SequenceScript.ID == sequenceScriptId).ToList();
            }
            catch (Exception ex)
            {
                _log.Error("GetAll(" + sequenceScriptId + "): Unable to retrieve sequence script parameters", ex);

                return new List<SequenceScriptParameter>();
            }
        }

        public SequenceScriptParameter Insert(SequenceScriptParameter sequenceScriptParameter)
        {
            try
            {
                _context.SequenceScriptParameters.Add(sequenceScriptParameter);
                Save();

                return sequenceScriptParameter;
            }
            catch (Exception ex)
            {
                _log.Error("Insert(): Unable to add new sequence script parameter", ex);

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

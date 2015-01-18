using GhostRunner.DAL;
using GhostRunner.DAL.Interface;
using GhostRunner.Models;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GhostRunner.SL
{
    public class SequenceScriptService
    {
        #region Private Properties

        private static readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private ISequenceDataAccess _sequenceDataAccess;
        private ISequenceScriptDataAccess _sequenceScriptDataAccess;

        #endregion

        #region Constructors

        public SequenceScriptService(IContext context)
        {
            InitializeDataAccess(context);
        }

        #endregion

        #region Public Methods

        public IList<SequenceScript> GetAllSequenceScripts(String sequenceId)
        {
            return _sequenceScriptDataAccess.GetAll(sequenceId).OrderBy(ss => ss.Position).ToList();
        }

        public SequenceScript GetSequenceScript(String sequenceScriptId)
        {
            return _sequenceScriptDataAccess.Get(sequenceScriptId);
        }

        public Boolean UpdateSequenceScript(String sequenceScriptId, String name, String content)
        {
            return _sequenceScriptDataAccess.Update(sequenceScriptId, name, content);
        }

        public Boolean DeleteSequenceScript(String sequenceScriptId)
        {
            return _sequenceScriptDataAccess.Delete(sequenceScriptId);
        }

        #endregion

        #region Private Methods

        private void InitializeDataAccess(IContext context)
        {
            _sequenceDataAccess = new SequenceDataAccess(context);
            _sequenceScriptDataAccess = new SequenceScriptDataAccess(context);
        }

        #endregion
    }
}

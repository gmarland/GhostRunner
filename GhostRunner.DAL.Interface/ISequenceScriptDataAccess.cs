using GhostRunner.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GhostRunner.DAL.Interface
{
    public interface ISequenceScriptDataAccess
    {
        IList<SequenceScript> GetAll(String sequenceId);

        int GetNextPosition(String sequenceId);

        SequenceScript Insert(SequenceScript sequenceScript);

        Boolean UpdateScriptOrder(String sequenceId);

        Boolean UpdateScriptOrder(String sequenceId, String scriptId, int position);

        Boolean Delete(String sequenceId, String scriptId, int position);
    }
}

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

        SequenceScript Get(String sequenceScriptId);

        int GetNextPosition(String sequenceId);

        Boolean UpdateScriptOrder(String sequenceId);

        Boolean UpdateScriptSequenceOrder(String sequenceScriptId, int position);

        SequenceScript Insert(SequenceScript sequenceScript);

        Boolean Update(String sequenceScriptId, String name, String content);

        Boolean Delete(String sequenceScriptId);
    }
}

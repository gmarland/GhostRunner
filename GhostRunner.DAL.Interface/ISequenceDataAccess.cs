using GhostRunner.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GhostRunner.DAL.Interface
{
    public interface ISequenceDataAccess
    {
        IList<Sequence> GetAll(int projectId);

        Sequence Get(String sequenceId);

        Sequence Insert(Sequence sequence);

        Boolean Update(String sequenceId, String name, String description);
    }
}

using GhostRunner.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GhostRunner.DAL.Interface
{
    public interface IScriptDataAccess
    {
        IList<Script> GetAll(long projectId);

        IList<Script> GetAll(long projectId, IList<long> scriptIds);

        Script Get(long scriptId);

        Script Get(String scriptId);

        Script Insert(Script script);

        Boolean Update(String scriptId, String name, String description, String content);

        Boolean Delete(String scriptId);
    }
}

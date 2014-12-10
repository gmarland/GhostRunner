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
        IList<Script> GetAll(int projectId);

        IList<Script> GetAll(int projectId, IList<int> scriptIds);

        Script Get(String scriptId);

        Script Insert(Script script);

        Boolean Update(String scriptId, String name, String description, String content);

        Boolean Delete(String scriptId);
    }
}

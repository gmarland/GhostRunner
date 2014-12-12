using GhostRunner.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GhostRunner.DAL.Interface
{
    public interface ITaskScriptParameterDataAccess
    {
        IList<TaskScriptParameter> GetAll(int taskId);

        TaskScriptParameter Insert(TaskScriptParameter taskParameter);
    }
}

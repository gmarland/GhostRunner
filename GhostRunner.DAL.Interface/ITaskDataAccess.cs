using GhostRunner.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GhostRunner.DAL.Interface
{
    public interface ITaskDataAccess
    {
        Task Get(String initializationTaskId);

        IList<Task> GetAllByProjectId(int projectId);

        IList<Task> GetAllByScriptId(int scriptId);

        Task Insert(Task initializationTask);
    }
}

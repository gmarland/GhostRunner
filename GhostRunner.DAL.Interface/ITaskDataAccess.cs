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

        IList<Task> GetAllByProjectId(long projectId);

        IList<Task> GetAllByScriptId(long scriptId);

        Task Insert(Task initializationTask);
    }
}

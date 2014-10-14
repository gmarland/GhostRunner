using GhostRunner.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GhostRunner.DAL.Interface
{
    public interface IProjectDataAccess
    {
        Project GetById(int projectId);

        Project GetByExternalId(String projectId);

        IList<Project> GetByUserId(int userId);

        Boolean AddUserToProject(User user, Project project);

        Project Insert(Project project);

        Boolean Update(int projectId, String name);

        Boolean Delete(Project project);
    }
}

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
        IList<Project> GetAll();

        Project GetById(int projectId);

        Project GetByExternalId(String projectId);

        Boolean AddUserToProject(User user, Project project);

        Project Insert(Project project);

        Boolean Update(String projectId, String name);

        Boolean Delete(String projectId);
    }
}

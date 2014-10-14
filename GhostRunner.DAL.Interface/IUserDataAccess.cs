using GhostRunner.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GhostRunner.DAL.Interface
{
    public interface IUserDataAccess
    {
        User GetById(int userId);

        User GetBySessionId(String sessionId);

        User GetByEmail(String email);

        User Insert(User user);

        Boolean Update(int userId, String name, String email);

        Boolean UpdatePassword(int userId, String password);

        Boolean UpdateSessionId(int userId, String sessionId);
    }
}

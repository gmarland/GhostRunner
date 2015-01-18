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
        User GetById(long userId);

        User GetBySessionId(String sessionId);

        User GetByEmail(String email);

        User Insert(User user);

        Boolean Update(long userId, String name, String email);

        Boolean UpdatePassword(long userId, String password);

        Boolean UpdateSessionId(long userId, String sessionId);
    }
}

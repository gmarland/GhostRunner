using GhostRunner.DAL.Interface;
using GhostRunner.Models;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GhostRunner.DAL
{
    public class UserDataAccess : IUserDataAccess
    {
        protected IContext _context;

        private static readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public UserDataAccess(IContext context)
        {
            _context = context;
        }

        public User GetById(int userId)
        {
            try
            {
                return _context.Users.SingleOrDefault(u => u.ID == userId);
            }
            catch (Exception ex)
            {
                _log.Error("GetById(" + userId + "): Unable to retrieve user by ID", ex);

                return null;
            }
        }

        public User GetBySessionId(string sessionId)
        {
            try
            {
                return _context.Users.SingleOrDefault(u => u.SessionId == sessionId);
            }
            catch (Exception ex)
            {
                _log.Error("GetBySessionId(" + sessionId + "): Unable to retrieve user by session ID", ex);

                return null;
            }
        }

        public User GetByEmail(string email)
        {
            try
            {
                return _context.Users.SingleOrDefault(u => u.Email.Trim().ToLower() == email.Trim().ToLower());
            }
            catch (Exception ex)
            {
                _log.Error("GetByEmail(" + email + "): Unable to retrieve user by email", ex);

                return null;
            }
        }

        public User Insert(User user)
        {
            try
            {
                _context.Users.Add(user);
                Save();

                return user;
            }
            catch (Exception ex)
            {
                _log.Error("Insert(): Unable to add new user", ex);

                return null;
            }
        }

        public Boolean Update(int userId, String name, String email)
        {
            User user = null;

            try
            {
                user = _context.Users.SingleOrDefault(u => u.ID == userId);
            }
            catch (Exception ex)
            {
                _log.Error("Update(" + userId + "): Error retrieving user", ex);

                return false;
            }

            if (user != null)
            {
                try
                {
                    user.Name = name;
                    user.Email = email;
                    Save();

                    return true;
                }
                catch (Exception ex)
                {
                    _log.Error("Update(" + userId + ", " + name + ", " + email + "): Error saving user", ex);

                    return false;
                }
            }
            else
            {
                _log.Info("Update(" + userId + "): User not found");

                return false;
            }
        }

        public Boolean UpdatePassword(int userId, String password)
        {
            User user = null;

            try
            {
                user = _context.Users.SingleOrDefault(u => u.ID == userId);
            }
            catch (Exception ex)
            {
                _log.Error("UpdatePassword(" + userId + "): Error retrieving user", ex);

                return false;
            }

            if (user != null)
            {
                try
                {
                    user.Password = password;
                    Save();

                    return true;
                }
                catch (Exception ex)
                {
                    _log.Error("UpdatePassword(" + userId + "): Error saving user password", ex);

                    return false;
                }
            }
            else
            {
                _log.Info("UpdatePassword(" + userId + "): User not found");

                return false;
            }
        }

        public Boolean UpdateSessionId(int userId, String sessionId)
        {
            User user = null;

            try
            {
                user = _context.Users.SingleOrDefault(u => u.ID == userId);
            }
            catch (Exception ex)
            {
                _log.Error("UpdateSessionId(" + userId + "): Error retrieving user", ex);

                return false;
            }

            if (user != null)
            {
                try
                {
                    user.SessionId = sessionId;
                    Save();

                    return true;
                }
                catch (Exception ex)
                {
                    _log.Error("UpdateSessionId(" + userId + ", " + sessionId + "): Error saving user sessionId", ex);

                    return false;
                }
            }
            else
            {
                _log.Info("UpdateSessionId(" + userId + "): User not found");

                return false;
            }
        }

        private void Save()
        {
            try
            {
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

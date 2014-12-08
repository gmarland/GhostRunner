using GhostRunner.DAL;
using GhostRunner.DAL.Interface;
using GhostRunner.Models;
using GhostRunner.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GhostRunner.SL
{
    public class UserService
    {
        #region Private Properties

        private IUserDataAccess _userDataAccess;

        #endregion

        #region Constructors

        public UserService()
        {
            InitializeDataAccess(new GhostRunnerContext("DatabaseConnectionString"));
        }

        public UserService(IContext context)
        {
            InitializeDataAccess(context);
        }

        #endregion

        #region User Methods

        public User Authenticate(String email, String password)
        {
            User user = _userDataAccess.GetByEmail(email);
        
            if (user != null)
            {
                if (user.Password == EncryptionHelper.Hash(password, "SarahMcGowan")) return user;
                else return null;
            }
            else
            {
                return null;
            }
        }

        public User GetUser(String sessionId)
        {
            return _userDataAccess.GetBySessionId(sessionId);
        }

        public User GetUserByEmail(String email)
        {
            return _userDataAccess.GetByEmail(email);
        }

        public User InsertUser(String name, String email, String password)
        {
            User user = new User();
            user.ExternalId = System.Guid.NewGuid().ToString();
            user.SessionId = String.Empty;
            user.Name = name;
            user.Email = email;
            user.Password = EncryptionHelper.Hash(password, "SarahMcGowan");
            user.IsAdminstrator = false;
            user.Created = DateTime.UtcNow;

            return _userDataAccess.Insert(user);
        }

        public String UpdateSessionId(int userId)
        {
            String sessionId = System.Guid.NewGuid().ToString();

            if (_userDataAccess.UpdateSessionId(userId, sessionId)) return sessionId;
            else return String.Empty;
        }

        public Boolean UpdateUser(int userId, String name, String email, String password)
        {
            if (!String.IsNullOrEmpty(password)) _userDataAccess.UpdatePassword(userId, EncryptionHelper.Hash(password, "SarahMcGowan"));

            return _userDataAccess.Update(userId, name, email);
        }

        #endregion

        #region Private Methods

        private void InitializeDataAccess(IContext context)
        {
            _userDataAccess = new UserDataAccess(context);
        }

        #endregion
    }
}

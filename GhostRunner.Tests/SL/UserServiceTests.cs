using GhostRunner.Models;
using GhostRunner.SL;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GhostRunner.Tests.SL
{
    [TestClass]
    public class UserServiceTests
    {
        UserService _userService;

        [TestInitialize]
        public void Initialize()
        {
            _userService = new UserService(new TestContext());
        }

        [TestMethod]
        public void Authenticate()
        {
            User authenticatedUser = _userService.Authenticate("test.user.1@gmail.com", "test");
            Assert.IsNotNull(authenticatedUser);
            Assert.AreEqual(1, authenticatedUser.ID);
            Assert.AreEqual("test.user.1@gmail.com", authenticatedUser.Email);

            User unauthenticatedUser99 = _userService.Authenticate("test.user.99@gmail.com", "test");
            Assert.IsNull(unauthenticatedUser99);

            User unauthenticatedUser1 = _userService.Authenticate("test.user.99@gmail.com", "tester");
            Assert.IsNull(unauthenticatedUser1);
        }

        [TestMethod]
        public void GetUser()
        {
            User user1 = _userService.GetUser("e36fbc0a-99b0-450c-83d7-9077820db7bc");
            Assert.IsNotNull(user1);
            Assert.AreEqual(1, user1.ID);
            Assert.AreEqual("test.user.1@gmail.com", user1.Email);

            User unauthenticatedUser99 = _userService.GetUser("99");
            Assert.IsNull(unauthenticatedUser99);
        }

        [TestMethod]
        public void GetUserByEmail()
        {
            User user1 = _userService.GetUserByEmail("test.user.1@gmail.com");
            Assert.IsNotNull(user1);
            Assert.AreEqual(1, user1.ID);
            Assert.AreEqual("test.user.1@gmail.com", user1.Email);

            User unauthenticatedUser99 = _userService.GetUserByEmail("test.user.9@gmail.com");
            Assert.IsNull(unauthenticatedUser99);
        }

        [TestMethod]
        public void InsertUser()
        {
            User newUser = _userService.InsertUser("test user 14", "test.user.14@gmail.com", "test");
            Assert.IsNotNull(newUser);
            Assert.AreEqual("test user 14", newUser.Name);
            Assert.AreEqual("test.user.14@gmail.com", newUser.Email);

            User authenticatedUser = _userService.Authenticate("test.user.14@gmail.com", "test");
            Assert.IsNotNull(authenticatedUser);
            Assert.AreEqual("test.user.14@gmail.com", authenticatedUser.Email);

            User unauthenticatedUser = _userService.Authenticate("test.user.14@gmail.com", "tester");
            Assert.IsNull(unauthenticatedUser);
        }

        [TestMethod]
        public void UpdateSessionId()
        {
            User userBefore = _userService.GetUser("e36fbc0a-99b0-450c-83d7-9077820db7bc");
            Assert.IsNotNull(userBefore);
            Assert.AreEqual(1, userBefore.ID);
            Assert.AreEqual("test.user.1@gmail.com", userBefore.Email);

            _userService.UpdateSessionId(1);

            User userAfter = _userService.GetUser("e36fbc0a-99b0-450c-83d7-9077820db7bc");
            Assert.IsNull(userAfter);
        }

        [TestMethod]
        public void UpdateUser()
        {
            User user1 = _userService.GetUserByEmail("test.user.1@gmail.com");
            Assert.IsNotNull(user1);
            Assert.AreEqual(1, user1.ID);
            Assert.AreEqual("test.user.1@gmail.com", user1.Email);

            Boolean updateSuccessful = _userService.UpdateUser(1, "new name", "new.email@gmail.com", String.Empty);
            Assert.IsTrue(updateSuccessful);

            User authenticatedUser = _userService.Authenticate("new.email@gmail.com", "test");
            Assert.IsNotNull(authenticatedUser);
            Assert.AreEqual(1, authenticatedUser.ID);

            Boolean updateSuccessfulPassword = _userService.UpdateUser(1, "new name", "new.email@gmail.com", "tester");
            Assert.IsTrue(updateSuccessfulPassword);

            User authenticatedUserSuccessful = _userService.Authenticate("new.email@gmail.com", "tester");
            Assert.IsNotNull(authenticatedUserSuccessful);
            Assert.AreEqual(1, authenticatedUserSuccessful.ID);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Net.Http;
using LoyaltySoftware.Models;
using LoyaltySoftware.Pages.Login;
using Microsoft.AspNetCore.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestLoyaltySoftware
{
    [TestClass]
    public class UnitTestsAuthentication
    {
        [TestMethod]
        public void EmptyUsernameField_ShouldReturnErrorMessage()
        {
            // Arrange
            var userLoginModel = new UserLoginModel();
            userLoginModel.UserAccount = new UserAccount();

            // Act
            var result = userLoginModel.OnPost();

            // Assert
            Assert.IsTrue(userLoginModel.ModelState.ContainsKey("UserAccount.username"));
            Assert.IsTrue(userLoginModel.ModelState["UserAccount.username"].Errors.Count > 0);
        }

        [TestMethod]
        public void EmptyPasswordField_ShouldReturnErrorMessage()
        {
            // Arrange
            var userLoginModel = new UserLoginModel();
            userLoginModel.UserAccount = new UserAccount();

            // Act
            var result = userLoginModel.OnPost();

            // Assert
            Assert.IsTrue(userLoginModel.ModelState.ContainsKey("UserAccount.password"));
            Assert.IsTrue(userLoginModel.ModelState["UserAccount.us"].Errors.Count > 0);
        }

        // Similarly, write tests for other scenarios like InvalidUsername, SuspendedAccount, RevokedAccount, IncorrectPassword, etc.

        [TestMethod]
        public void InvalidUsername_ShouldReturnErrorMessage()
        {
            // Arrange
            var userLoginModel = new UserLoginModel();
            userLoginModel.UserAccount = new UserAccount { username = "nonexistentuser", password = "password123" };

            // Act
            var result = userLoginModel.OnPost();

            // Assert
            Assert.AreEqual("Username does not exist!", userLoginModel.Message1);
        }

        [TestMethod]
        public void SuspendedAccount_ShouldReturnErrorMessage()
        {
            // Arrange
            var userLoginModel = new UserLoginModel();
            userLoginModel.UserAccount = new UserAccount { username = "suspendedUser", password = "password123" };

            // Act
            var result = userLoginModel.OnPost();

            // Assert
            Assert.AreEqual("Your account has been suspended.", userLoginModel.Message2);
        }

        [TestMethod]
        public void RevokedAccount_ShouldReturnErrorMessage()
        {
            // Arrange
            var userLoginModel = new UserLoginModel();
            userLoginModel.UserAccount = new UserAccount { username = "revokedUser", password = "password123" };

            // Act
            var result = userLoginModel.OnPost();

            // Assert
            Assert.AreEqual("Your account has been revoked.", userLoginModel.Message2);
        }

        // Test for error message for dictionary attack
        [TestMethod]
        public void IncorrectPassword_ShouldReturnMessage()
        {
            // Arrange
            UserLoginModel userLoginModel = new UserLoginModel();

            // list of common passwords for dictionary attack
            // 1. 123456 (returns message)
            // 2. password (returns message)
            // 3. 12345678 (returns message)
            // 4. qwerty (returns message)
            // 5. 123456789 (returns message)
            // 6. 12345 (fails)
            // 7. 1234 
            // 8. 111111 
            // 9. 1234567
            // 10. dragon

            userLoginModel.UserAccount = new UserAccount { username = "jsmith1", password = "12345"};
            var result = userLoginModel.OnPost();

            // Assert
            Assert.AreEqual("Password does not match!", userLoginModel.Message2);
        }


        [TestMethod]
        public void RateLimiting_ShouldBlockExcessiveLoginAttempts()
        {
            // Arrange
            var userLoginModel = new UserLoginModel();
            userLoginModel.UserAccount = new UserAccount { username = "validUser", password = "password123" };
            HttpContext httpContext = null;
            string ipAddress = httpContext.Connection.RemoteIpAddress?.ToString();

            // Act
            for (int i = 0; i < 5; i++) // Simulate 5 login attempts within a short period
            {
                // Set IP address for each attempt
                userLoginModel.IPAddress = ipAddress;
                var result = userLoginModel.OnPost();
            }

            // Assert
            Assert.AreEqual("You have reached the maximum number of login attempts. Please try again later.", userLoginModel.Message2);

            // Additional assertions to verify that further login attempts are blocked
            for (int i = 0; i < 3; i++) // Simulate additional login attempts after reaching the limit
            {
                // Set IP address for each attempt
                userLoginModel.IPAddress = ipAddress;
                var result = userLoginModel.OnPost();
                Assert.AreEqual("You have reached the maximum number of login attempts. Please try again later.", userLoginModel.Message2);
            }
        }
    }

}
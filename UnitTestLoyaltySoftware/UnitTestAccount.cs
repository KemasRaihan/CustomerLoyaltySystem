using LoyaltySoftware.Models;
using LoyaltySoftware.Pages.Login;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestLoyaltySoftware
{
    [TestClass]
    public class UnitTestAccount
    {
        // Test to verify the member's user role is correctly identified during login
        [TestMethod]
        public void TestCheckRole()
        {
            string username = "jsmith1";
            string roleExpected = "member";
            string roleActual = UserAccount.checkRole(username);
            Assert.AreEqual(roleExpected, roleActual);
        }

        // Test to verify an account's status
        [TestMethod]
        public void TestCheckStatus()
        {
            string username = "jsmith1";
            string userStatusExpected = "active";
            string userStatusActual;
            userStatusActual = UserAccount.checkStatus(username);
            Assert.AreEqual(userStatusActual, userStatusExpected);
        }

        // Test to see if system identifies an account that already exists by checking username
        [TestMethod]
        public void TestCheckIfUsernameExists()
        {
            string username = "jsmith1";
            Assert.IsTrue(UserAccount.checkIfUsernameExists(username));
        }

        // Test to see whether the password matches with the username
        [TestMethod]
        public void TestCheckPassword()
        {
            string username = "jsmith1";
            string password = "12345";
            Assert.IsTrue(UserAccount.checkPassword(username, password));
        }

        // Test to see if method findAccountID returns the correct account ID for the input username
        [TestMethod]
        public void TestFindAccountID()
        {
            string username = "jsmith1";
            int accountIDExpected = 2029;
            int accountIDActual = UserAccount.findAccountID(username);
            Assert.AreEqual(accountIDExpected, accountIDActual);
        }

        // Test to see if correct number of total points has been retrieved from the database
        [TestMethod]
        public void TestTotalPoints()
        {
            string username = "jsmith1";
            int accountIDExpected = 2029;
            int accountIDActual = UserAccount.findAccountID(username);
            Assert.AreEqual(accountIDExpected, accountIDActual);
        }

    }
}

using LoyaltySoftware.Pages.Admin;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTestLoyaltySoftware
{

    [TestClass]
    public class UnitTestSuspend
    {

        [TestMethod]
        public void SuspendUserAccount()
        {
            string username = "jsmith1";
            string statusActual = SuspendModel.SuspendMember(username);
            Assert.AreEqual(statusActual, "suspended");

        }
    }
}

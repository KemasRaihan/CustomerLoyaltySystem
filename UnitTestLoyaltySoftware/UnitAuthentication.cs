using LoyaltySoftware.Models;
using LoyaltySoftware.Pages.Login;
using LoyaltySoftware.Pages.Rewards;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestLoyaltySoftware
{
    [TestClass]
    public class UnitTestLogin
    {
        [TestMethod]
        public void TestLockAccounts()
        {
            int? ID = 1;
            string rewardNameExpected = "Reward 1";
            string rewardNameActual = RewardInfoModel.getReward(ID).rewardName;
            Assert.AreEqual(rewardNameExpected, rewardNameActual);
        }

    }


}


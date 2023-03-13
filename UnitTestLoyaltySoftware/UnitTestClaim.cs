using LoyaltySoftware.Models;
using LoyaltySoftware.Pages.Rewards;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTestLoyaltySoftware
{
    [TestClass]
    public class UnitTestReward
    {
        [TestMethod]
        public void TestGetRewardName()
        {
            int? ID = 1;
            string rewardNameExpected = "Reward 1";
            string rewardNameActual = RewardInfoModel.getReward(ID).rewardName;
            Assert.AreEqual(rewardNameExpected, rewardNameActual);
        }

    }
}

using LoyaltySoftware.Models;
using LoyaltySoftware.Pages.Shop;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTestLoyaltySoftware
{
    [TestClass]
    public class UnitTestProduct
    {
        // Test to verify that the correct product records have been chosen. We can show this by returning the product name.
        [TestMethod]
        public void TestFindProduct()
        {
            int? productID = 1;
            string productNameExpected = "Shoe 1";
            string productNameActual = ProductInfoModel.findProduct(productID).productName;
            Assert.AreEqual(productNameExpected, productNameActual);
        }

        // Test to verify that the points, earned by the user, is calculated correctly based on the price of the product that they have purchased.
        [TestMethod]
        public void TestCalculatePointsEarned()
        {
            double price = 100.0;
            int pointsClaimedExpected = 100;
            int pointsClaimedActual = ProductInfoModel.calculatePointsEarned(price);
            Assert.AreEqual(pointsClaimedExpected, pointsClaimedActual);
        }
    }
}

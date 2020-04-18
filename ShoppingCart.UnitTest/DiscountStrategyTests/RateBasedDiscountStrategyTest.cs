using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using ShoppingCart.Business.DiscountStrategy;

namespace ShoppingCart.UnitTest.DiscountStrategyTests
{
    [TestFixture]
    public class RateBasedDiscountStrategyTest
    {
        [Test]
        public void CalculateDiscount_ShouldBeExpectedValue_RateBased()
        {
            var rateBasedDiscountStrategy = new RateBasedDiscountStrategy(40);
            Assert.AreEqual(180, rateBasedDiscountStrategy.CalculateDiscount(300));
        }
    }
}

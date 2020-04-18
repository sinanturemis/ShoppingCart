using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using ShoppingCart.Business.DiscountStrategy;

namespace ShoppingCart.UnitTest.DiscountStrategyTests
{
    [TestFixture]
    public class AmountBasedDiscountStrategyTest
    {
        [Test]
        public void CalculateDiscount_ShouldBeExpectedValue_AmountBased()
        {
            var amountBasedDiscountStrategy = new AmountBasedDiscountStrategy(40);
            Assert.AreEqual(260, amountBasedDiscountStrategy.CalculateDiscount(300));
        }
    }
}

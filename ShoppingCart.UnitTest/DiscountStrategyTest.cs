using System;
using Moq;
using NUnit.Framework;
using ShoppingCart.Business;
using ShoppingCart.Business.DiscountStrategy;
using ShoppingCart.Business.Factories;
using ShoppingCart.Business.Objects;

namespace ShoppingCart.UnitTest
{
    [TestFixture]
    public class DiscountStrategyTest
    {
        [Test]
        public void CalculateDiscount_ShouldBeExpectedValue_AmountBased()
        {
            var amountBasedDiscountStrategy = new AmountBasedDiscountStrategy(40);
            Assert.AreEqual(40, amountBasedDiscountStrategy.CalculateDiscount(300));
        }

        [Test]
        public void CalculateDiscount_ShouldBeExpectedValue_RateBased()
        {
            var rateBasedDiscountStrategy = new RateBasedDiscountStrategy(40);
            Assert.AreEqual(120, rateBasedDiscountStrategy.CalculateDiscount(300));
        }

        [Test]
        public void CalculateDiscount_ShouldBeInstanceOfAmountBased()
        {
            var strategy = DiscountFactory.GetDiscountStrategy(DiscountType.Amount, It.IsAny<int>());
            Assert.IsInstanceOf<AmountBasedDiscountStrategy>(strategy);
        }

        [Test]
        public void CalculateDiscount_ShouldBeInstanceOfRateBased()
        {
            var strategy = DiscountFactory.GetDiscountStrategy(DiscountType.Rate, It.IsAny<int>());
            Assert.IsInstanceOf<RateBasedDiscountStrategy>(strategy);
        }

        [Test]
        public void CalculateDiscount_ThrowsException_UnexpectedTypes()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => DiscountFactory.GetDiscountStrategy(DiscountType.Unknown, It.IsAny<int>()));
        }
    }
}

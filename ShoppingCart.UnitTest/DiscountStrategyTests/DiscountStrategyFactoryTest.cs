using System;
using System.Collections.Generic;
using System.Text;
using Moq;
using NUnit.Framework;
using ShoppingCart.Business.DiscountStrategy;
using ShoppingCart.Business.Factories;
using ShoppingCart.Business.Objects;

namespace ShoppingCart.UnitTest.DiscountStrategyTests
{
    [TestFixture]
    public class DiscountStrategyFactoryTest
    {
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

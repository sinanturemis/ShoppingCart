using NUnit.Framework;
using ShoppingCart.Business.DiscountStrategy;
using ShoppingCart.Business.Objects;

namespace ShoppingCart.UnitTest
{
    [TestFixture]
    public class CouponTest
    {
        #region IsApplicable

        [Test]
        public void CheckIfApplicable_ShouldBeApplicable_ExactAmountBiggerThanExpectedAmount()
        {
            var campaign = new Coupon(100, new RateBasedDiscountStrategy(50));

            Assert.IsTrue(campaign.IsApplicable(150));
        }

        [Test]
        public void CheckIfApplicable_ShouldBeApplicable_ExactAmountEqualsExpectedAmount()
        {
            var campaign = new Coupon(100, new RateBasedDiscountStrategy(50));

            //Request => if cart amount is less than minimum, discount is not applied.
            //Equality is acceptable to apply a coupon
            Assert.IsTrue(campaign.IsApplicable(100));
        }

        [Test]
        public void CheckIfApplicable_ShouldntBeApplicable_ExactAmountLessThanExpectedAmount()
        {
            var campaign = new Coupon(100, new RateBasedDiscountStrategy(50));

            Assert.IsFalse(campaign.IsApplicable(60));
        }

        #endregion
    }
}

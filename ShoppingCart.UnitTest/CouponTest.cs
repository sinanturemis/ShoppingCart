using NUnit.Framework;
using ShoppingCart.Business.Objects;

namespace ShoppingCart.UnitTest
{
    [TestFixture]
    public class CouponTest
    {
        [Test]
        public void CheckIfApplicable_ShouldBeApplicable_ExactAmountBiggerThanExpectedAmount()
        {
            var campaign = new Coupon(100, 20, DiscountType.Amount);
            Assert.IsTrue(campaign.IsApplicable(150));
        }

        [Test]
        public void CheckIfApplicable_ShouldBeApplicable_ExactAmountEqualsExpectedAmount()
        {
            var campaign = new Coupon(100, 20, DiscountType.Amount);

            //Request => if cart amount is less than minimum, discount is not applied (Equality is acceptable to apply a coupon)
            Assert.IsTrue(campaign.IsApplicable(100));
        }

        [Test]
        public void CheckIfApplicable_ShouldntBeApplicable_ExactAmountLessThanExpectedAmount()
        {
            var campaign = new Coupon(100, 20, DiscountType.Amount);
            Assert.IsFalse(campaign.IsApplicable(60));
        }
    }
}

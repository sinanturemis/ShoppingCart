using NUnit.Framework;
using ShoppingCart.Business.Objects;

namespace ShoppingCart.UnitTest
{
    [TestFixture]
    public class CouponTest
    {
        private Coupon _coupon;

        [SetUp]
        public void SetUp()
        {
            _coupon = new Coupon(100, 20, DiscountType.Amount);
        }

        [Test]
        public void CheckIfApplicable_ShouldBeApplicable_ExactAmountBiggerThanExpectedAmount()
        {
            Assert.IsTrue(_coupon.IsApplicable(150));
        }

        [Test]
        public void CheckIfApplicable_ShouldBeApplicable_ExactAmountEqualsExpectedAmount()
        {
            //Request => if cart amount is less than minimum, discount is not applied (Equality is acceptable to apply a coupon)
            Assert.IsTrue(_coupon.IsApplicable(100));
        }

        [Test]
        public void CheckIfApplicable_ShouldntBeApplicable_ExactAmountLessThanExpectedAmount()
        {
            Assert.IsFalse(_coupon.IsApplicable(60));
        }
    }
}

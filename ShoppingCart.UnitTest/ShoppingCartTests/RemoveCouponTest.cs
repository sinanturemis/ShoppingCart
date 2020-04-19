using NUnit.Framework;
using ShoppingCart.Business.Objects;

namespace ShoppingCart.UnitTest.ShoppingCartTests
{
    [TestFixture]
    public class RemoveCouponTest
    {
        [Test]
        public void RemoveCoupon_ShouldntBeRemoved_NoAppliedCoupon()
        {
            var toshibaTvProduct = new Product("Toshiba Tv", 4000, new Category("Electronic"));

            var cart = new Business.Objects.ShoppingCart();
            var addItemResult = cart.AddItem(toshibaTvProduct, 2);
            var removeCouponResult = cart.RemoveCoupon();

            Assert.IsTrue(addItemResult);
            Assert.IsFalse(removeCouponResult);
        }

        [Test]
        public void RemoveCoupon_ShouldBeRemoved_ThereIsAnAppliedCoupon()
        {
            var toshibaTvProduct = new Product("Toshiba Tv", 4000, new Category("Electronic"));

            var cart = new Business.Objects.ShoppingCart();

            var addItemResult = cart.AddItem(toshibaTvProduct, 2);
            var addCouponResult = cart.ApplyCoupon(new Coupon(6000, 10, DiscountType.Rate));
            var removeCouponResult = cart.RemoveCoupon();

            Assert.IsTrue(addItemResult);
            Assert.IsTrue(addCouponResult);
            Assert.IsTrue(removeCouponResult);
        }
    }
}

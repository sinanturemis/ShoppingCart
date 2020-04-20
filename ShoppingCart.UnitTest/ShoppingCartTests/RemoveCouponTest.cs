using NUnit.Framework;
using ShoppingCart.Business;
using ShoppingCart.Business.Objects;

namespace ShoppingCart.UnitTest.ShoppingCartTests
{
    [TestFixture]
    public class RemoveCouponTest: ShoppingCartTest
    {
        [Test]
        public void RemoveCoupon_ShouldntBeRemoved_NoAppliedCoupon()
        {
            var toshibaTvProduct = new Product("Toshiba Tv", 4000, new Category("Electronic"));

            var addItemResult = Cart.AddItem(toshibaTvProduct, 2);
            var removeCouponResult = Cart.RemoveCoupon();

            Assert.IsTrue(addItemResult);
            Assert.IsFalse(removeCouponResult);
        }

        [Test]
        public void RemoveCoupon_ShouldBeRemoved_ThereIsAnAppliedCoupon()
        {
            var toshibaTvProduct = new Product("Toshiba Tv", 4000, new Category("Electronic"));

            var addItemResult = Cart.AddItem(toshibaTvProduct, 2);
            var addCouponResult = Cart.ApplyCoupon(new Coupon(6000, 10, DiscountType.Rate));
            var removeCouponResult = Cart.RemoveCoupon();

            Assert.IsTrue(addItemResult);
            Assert.IsTrue(addCouponResult);
            Assert.IsTrue(removeCouponResult);
        }
    }
}

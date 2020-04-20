using NUnit.Framework;
using ShoppingCart.Business;
using ShoppingCart.Business.Objects;

namespace ShoppingCart.UnitTest.ShoppingCartTests
{
    [TestFixture]
    public class GetCouponDiscountTest : ShoppingCartTest
    {
        [Test]
        public void GetCouponDiscount_ShouldZero_EmptyCart()
        {
            Assert.Zero(Cart.GetCouponDiscount());
        }

        [Test]
        public void GetCouponDiscount_ShouldBeZero_WithoutApplyingCoupon()
        {
            var toshibaTvProduct = new Product("Toshiba Tv", 4000, new Category("Electronic"));

            Cart.AddItem(toshibaTvProduct, 2);

            Assert.Zero(Cart.GetCouponDiscount());
        }

        [Test]
        public void GetCouponDiscount_ShouldntBeZero_CartAmountGreaterThanCouponMinAmount()
        {
            Cart.AddItem(new Product("Toshiba Tv", 4000, new Category("Electronic")), 2);
            Cart.ApplyCoupon(new Coupon(6000, 10, DiscountType.Rate));

            Assert.NotZero(Cart.GetCouponDiscount());
        }

        [Test]
        public void GetCouponDiscount_ShouldntBeZero_CartAmountEqualsCouponMinAmount()
        {
            Cart.AddItem(new Product("Toshiba Tv", 4000, new Category("Electronic")), 2);
            Cart.ApplyCoupon(new Coupon(8000, 10, DiscountType.Rate));

            Assert.NotZero(Cart.GetCouponDiscount());
        }

        [Test]
        public void GetCouponDiscount_ShouldBeZero_CartAmountLessThanCouponMinAmount()
        {
            Cart.AddItem(new Product("Toshiba Tv", 4000, new Category("Electronic")), 2);
            Cart.ApplyCoupon(new Coupon(9000, 10, DiscountType.Rate));

            Assert.Zero(Cart.GetCouponDiscount());
        }
    }
}

using NUnit.Framework;
using ShoppingCart.Business.Objects;

namespace ShoppingCart.UnitTest.ShoppingCartTests
{
    [TestFixture]
    public class GetCouponDiscountTest
    {
        [Test]
        public void GetCouponDiscount_ShouldZero_EmptyCart()
        {
            var cart = new Business.Objects.ShoppingCart();
            Assert.Zero(cart.GetCouponDiscount());
        }

        [Test]
        public void GetCouponDiscount_ShouldBeZero_WithoutApplyingCoupon()
        {
            var toshibaTvProduct = new Product("Toshiba Tv", 4000, new Category("Electronic"));

            var cart = new Business.Objects.ShoppingCart();
            cart.AddItem(toshibaTvProduct, 2);

            Assert.Zero(cart.GetCouponDiscount());
        }

        [Test]
        public void GetCouponDiscount_ShouldntBeZero_CartAmountGreaterThanCouponMinAmount()
        {
            var cart = new Business.Objects.ShoppingCart();

            cart.AddItem(new Product("Toshiba Tv", 4000, new Category("Electronic")), 2);
            cart.ApplyCoupon(new Coupon(6000, 10, DiscountType.Rate));

            Assert.NotZero(cart.GetCouponDiscount());
        }

        [Test]
        public void GetCouponDiscount_ShouldntBeZero_CartAmountEqualsCouponMinAmount()
        {
            var cart = new Business.Objects.ShoppingCart();

            cart.AddItem(new Product("Toshiba Tv", 4000, new Category("Electronic")), 2);
            cart.ApplyCoupon(new Coupon(8000, 10, DiscountType.Rate));

            Assert.NotZero(cart.GetCouponDiscount());
        }

        [Test]
        public void GetCouponDiscount_ShouldBeZero_CartAmountLessThanCouponMinAmount()
        {
            var cart = new Business.Objects.ShoppingCart();

            cart.AddItem(new Product("Toshiba Tv", 4000, new Category("Electronic")), 2);
            cart.ApplyCoupon(new Coupon(9000, 10, DiscountType.Rate));

            Assert.Zero(cart.GetCouponDiscount());
        }
    }
}

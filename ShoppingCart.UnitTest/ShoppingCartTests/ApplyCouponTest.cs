using System.Linq;
using NUnit.Framework;
using ShoppingCart.Business.Objects;

namespace ShoppingCart.UnitTest.ShoppingCartTests
{
    [TestFixture]
    public class ApplyCouponTest
    {
        [Test]
        public void ApplyCoupon_ShouldntBeApplied_ACouponAlreadyApplied()
        {
            var cart = new Business.Objects.ShoppingCart();

            var category = new Category("Electronic");
            var toshibaTvProduct = new Product("Toshiba Tv", 4000, category);

            var addItemResult = cart.AddItem(toshibaTvProduct, 2);
            var addCouponResult = cart.ApplyCoupon(new Coupon(6000, 10, DiscountType.Rate));
            var addOtherCouponResult = cart.ApplyCoupon(new Coupon(7000, 100, DiscountType.Amount));

            Assert.IsTrue(addItemResult);
            Assert.IsTrue(addCouponResult);
            Assert.IsFalse(addOtherCouponResult);
        }

        [Test]
        public void ApplyCoupon_ShouldBeApplied_CartAmountGreaterThanCouponMinAmount()
        {
            var toshibaTvProduct = new Product("Toshiba Tv", 4000, new Category("Electronic"));

            var cart = new Business.Objects.ShoppingCart();

            Assert.IsTrue(cart.AddItem(toshibaTvProduct, 2));
            Assert.IsTrue(cart.ApplyCoupon(new Coupon(6000, 10, DiscountType.Rate)));
        }

        [Test]
        public void ApplyCoupon_ShouldBeApplied_CartAmountEqualsCouponMinAmount()
        {
            var toshibaTvProduct = new Product("Toshiba Tv", 4000, new Category("Electronic"));

            var cart = new Business.Objects.ShoppingCart();

            Assert.IsTrue(cart.AddItem(toshibaTvProduct, 2));
            Assert.IsTrue(cart.ApplyCoupon(new Coupon(8000, 10, DiscountType.Rate)));
        }

        [Test]
        public void ApplyCoupon_ShouldntBeApplied_CartAmountLessThanCouponMinAmount()
        {
            var cart = new Business.Objects.ShoppingCart();

            var toshibaTvProduct = new Product("Toshiba Tv", 4000, new Category("Electronic"));

            var addItemResult = cart.AddItem(toshibaTvProduct, 2);
            var addCouponResult = cart.ApplyCoupon(new Coupon(9000, 10, DiscountType.Rate));

            var cartItems = cart.GetCartItems();

            Assert.IsTrue(addItemResult);
            Assert.IsFalse(addCouponResult);
            Assert.IsTrue(cartItems.Count == 1);
            Assert.IsTrue(cartItems.Values.Sum(x => x.OrderQuantity) == 2);
        }

    }
}

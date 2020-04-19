using NUnit.Framework;
using ShoppingCart.Business.Objects;

namespace ShoppingCart.UnitTest.ShoppingCartTests
{
    [TestFixture]
    public class ApplyCampaignsTest
    {
        [Test]
        public void ApplyCampaigns_ShouldBeAdded_CouponAndCampaignsHaventApplied()
        {
            var campaign = new Campaign(new Category("Electronic"), 5, 1, DiscountType.Rate);

            var cart = new Business.Objects.ShoppingCart();

            Assert.IsTrue(cart.ApplyDiscounts(campaign));
        }

        [Test]
        public void ApplyCampaigns_ShouldntBeAdded_ApplySameCampaignsAgain()
        {
            var campaign = new Campaign(new Category("Electronic"), 5, 1, DiscountType.Rate);

            var cart = new Business.Objects.ShoppingCart();
            var addDiscountResult = cart.ApplyDiscounts(campaign);
            var addDiscountResultSecond = cart.ApplyDiscounts(campaign);

            Assert.IsTrue(addDiscountResult);
            Assert.IsFalse(addDiscountResultSecond);
        }

        [Test]
        public void ApplyCampaigns_ShouldntBeAdded_ApplyCampaignsAfterCoupon()
        {
            var category = new Category("Electronic");
            var campaign = new Campaign(category, 5, 1, DiscountType.Rate);
            var toshibaTvProduct = new Product("Toshiba Tv", 4000, category);

            var cart = new Business.Objects.ShoppingCart();
            cart.AddItem(toshibaTvProduct, 3);

            var addCouponResult = cart.ApplyCoupon(new Coupon(100, 10, DiscountType.Amount));
            var addDiscountResult = cart.ApplyDiscounts(campaign);

            Assert.IsTrue(addCouponResult);
            Assert.IsFalse(addDiscountResult);
        }
    }
}

using NUnit.Framework;
using ShoppingCart.Business.Objects;

namespace ShoppingCart.UnitTest.ShoppingCartTests
{
    [TestFixture]
    public class GetCampaignDiscountTest
    {
        [Test]
        public void GetCampaignDiscount_ShouldBeZero_EmptyCart()
        {
            var cart = new Business.Objects.ShoppingCart();
            Assert.Zero(cart.GetCampaignDiscount());
        }

        [Test]
        public void GetCampaignDiscount_ShouldBeZero_WithNonApplicableCampaign()
        {
            var electronicCategory = new Category("Electronic");
            var electronicCampaign = new Campaign(electronicCategory, 10, 500, DiscountType.Amount);
            var toshibaTvProduct = new Product("Toshiba Tv", 100, electronicCategory);

            var cart = new Business.Objects.ShoppingCart();
            cart.AddItem(toshibaTvProduct, 3);
            cart.ApplyDiscounts(electronicCampaign);

            Assert.Zero(cart.GetCampaignDiscount());
        }

        [Test]
        public void GetCampaignDiscount_ShouldBeGreaterThanZero_WithApplicableCampaign()
        {
            var electronicCategory = new Category("Electronic");
            var electronicCampaign = new Campaign(electronicCategory, 10, 1, DiscountType.Amount);
            var toshibaTvProduct = new Product("Toshiba Tv", 100, electronicCategory);

            var cart = new Business.Objects.ShoppingCart();
            cart.AddItem(toshibaTvProduct, 3);
            cart.ApplyDiscounts(electronicCampaign);

            Assert.Greater(cart.GetCampaignDiscount(), 0);
        }
    }
}

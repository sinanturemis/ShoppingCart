using NUnit.Framework;
using ShoppingCart.Business.Objects;

namespace ShoppingCart.UnitTest.ShoppingCartTests
{
    [TestFixture]
    public class GetCampaignDiscountTest : ShoppingCartTest
    {
        [Test]
        public void GetCampaignDiscount_ShouldBeZero_EmptyCart()
        {
            Assert.Zero(Cart.GetCampaignDiscount());
        }

        [Test]
        public void GetCampaignDiscount_ShouldBeZero_WithNonApplicableCampaign()
        {
            var electronicCategory = new Category("Electronic");
            var electronicCampaign = new Campaign(electronicCategory, 10, 500, DiscountType.Amount);
            var toshibaTvProduct = new Product("Toshiba Tv", 100, electronicCategory);

            Cart.AddItem(toshibaTvProduct, 3);
            Cart.ApplyDiscounts(electronicCampaign);

            Assert.Zero(Cart.GetCampaignDiscount());
        }

        [Test]
        public void GetCampaignDiscount_ShouldBeGreaterThanZero_WithApplicableCampaign()
        {
            var electronicCategory = new Category("Electronic");
            var electronicCampaign = new Campaign(electronicCategory, 10, 1, DiscountType.Amount);
            var toshibaTvProduct = new Product("Toshiba Tv", 100, electronicCategory);

            Cart.AddItem(toshibaTvProduct, 3);
            Cart.ApplyDiscounts(electronicCampaign);

            Assert.Greater(Cart.GetCampaignDiscount(), 0);
        }
    }
}

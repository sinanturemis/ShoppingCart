using System.Collections.Generic;
using NUnit.Framework;
using ShoppingCart.Business.Objects;

namespace ShoppingCart.UnitTest
{
    [TestFixture]
    public class ShoppingCartItemTest
    {
        [Test]
        public void GetBestDiscount_ShouldBeZero_WithoutCampaign()
        {
            var cartItem = new ShoppingCartItem(
                new Product("Toshiba Tv", 4000, new Category("Electronic")),
                2);

            cartItem.ApplyBestCampaign(new List<Campaign>());

            Assert.Zero(cartItem.BestCampaignDiscount.DiscountAmount);
            Assert.IsNull(cartItem.BestCampaignDiscount.Campaign);
        }

        [Test]
        public void GetBestDiscount_ShouldBeZero_WithoutApplicableCampaign()
        {
            var cartItem = new ShoppingCartItem(
                new Product("Toshiba Tv", 4000, new Category("Electronic")),
                2);

            cartItem.ApplyBestCampaign(new List<Campaign>()
            {
                new Campaign(new Category("Baby"), 10, 200, DiscountType.Amount)
            });

            Assert.Zero(cartItem.BestCampaignDiscount.DiscountAmount);
            Assert.IsNull(cartItem.BestCampaignDiscount.Campaign);
        }

        [Test]
        public void GetBestDiscount_ShouldBeExactValue_WithOneApplicableCampaign()
        {
            var commonCategory = new Category("Electronic");

            var cartItem = new ShoppingCartItem(new Product("Toshiba Tv", 4000, commonCategory), 2);
            cartItem.ApplyBestCampaign(new List<Campaign>()
            {
                new Campaign(commonCategory, 100, 1, DiscountType.Amount)
            });

            Assert.AreEqual(100, cartItem.BestCampaignDiscount.DiscountAmount);
            Assert.IsNotNull(cartItem.BestCampaignDiscount.Campaign);
        }

        [Test]
        public void GetBestDiscount_ShouldBeMaxValue_WithManyApplicableCampaigns()
        {
            var electronicCategory = new Category("Electronic");
            var tvCategory = new Category("Tv", electronicCategory);
            var ledCategory = new Category("Led", tvCategory);

            var cartItem = new ShoppingCartItem(new Product("Toshiba Tv", 4000, ledCategory), 2);
            cartItem.ApplyBestCampaign(new List<Campaign>()
            {
                new Campaign(ledCategory, 100, 1, DiscountType.Amount),
                new Campaign(tvCategory, 20, 1, DiscountType.Rate),
                new Campaign(electronicCategory, 1000, 1, DiscountType.Amount)
            });

            Assert.AreEqual(1600, cartItem.BestCampaignDiscount.DiscountAmount);
            Assert.AreEqual("Tv", cartItem.BestCampaignDiscount.Campaign.Category.Title);
        }
    }
}

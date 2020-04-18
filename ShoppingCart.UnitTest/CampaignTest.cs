using Moq;
using NUnit.Framework;
using ShoppingCart.Business.DiscountStrategy;
using ShoppingCart.Business.Objects;

namespace ShoppingCart.UnitTest
{
    [TestFixture]
    public class CampaignTest
    {
        #region IsApplicable

        [Test]
        public void CheckIfApplicable_ShouldBeApplicable_ExactQuantityBiggerThanExpectedQuantity()
        {
            var campaign = new Campaign(new Category("Tv"), 50, 2, DiscountType.Amount);

            Assert.IsTrue(campaign.IsApplicable(5));
        }

        [Test]
        public void CheckIfApplicable_ShouldntBeApplicable_ExactQuantityEqualsExpectedQuantity()
        {
            var campaign = new Campaign(new Category("Tv"), 50, 2, DiscountType.Amount);

            //Request => if bought more than x items.
            //Equality is not acceptable to apply a campaign
            Assert.IsFalse(campaign.IsApplicable(2));
        }

        [Test]
        public void CheckIfApplicable_ShouldntBeApplicable_ExactQuantityLessThanExpectedQuantity()
        {
            var campaign = new Campaign(new Category("Tv"), 50, 2, DiscountType.Amount);

            Assert.IsFalse(campaign.IsApplicable(1));
        }

        #endregion

        #region CalculateDiscount

        [Test]
        public void CalculateDiscount_ShouldBeBiggerThanZero_ProductPriceGreaterThanDiscount()
        {
            var totalPriceForTheProduct = 500;

            var discountStrategyMock = new Mock<IDiscountStrategy>();
            discountStrategyMock.Setup(x => x.CalculateDiscount(totalPriceForTheProduct)).Returns(450);

            var campaign = new Campaign(new Category("Tv"), 2, discountStrategyMock.Object);

            Assert.Greater(campaign.CalculateDiscount(totalPriceForTheProduct), 0);
        }

        [Test]
        public void CalculateDiscount_ShouldBeZero_ProductPriceEqualsDiscount()
        {
            var totalPriceForTheProduct = 50;

            var discountStrategyMock = new Mock<IDiscountStrategy>();
            discountStrategyMock.Setup(x => x.CalculateDiscount(totalPriceForTheProduct)).Returns(0);

            var campaign = new Campaign(new Category("Tv"), 2, discountStrategyMock.Object);

            Assert.AreEqual(0, campaign.CalculateDiscount(totalPriceForTheProduct));
        }

        [Test]
        public void CalculateDiscount_ShouldBeZero_ProductPriceLessThanDiscount()
        {
            var totalPriceForTheProduct = 30;

            var discountStrategyMock = new Mock<IDiscountStrategy>();
            discountStrategyMock.Setup(x => x.CalculateDiscount(totalPriceForTheProduct)).Returns(-20);

            var campaign = new Campaign(new Category("Tv"), 2, discountStrategyMock.Object);

            Assert.AreEqual(0, campaign.CalculateDiscount(totalPriceForTheProduct));
        }

        #endregion

    }
}

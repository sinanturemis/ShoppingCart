using NUnit.Framework;
using ShoppingCart.Business.Objects;

namespace ShoppingCart.UnitTest
{
    [TestFixture]
    public class CampaignTest
    {
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

            //Request => if bought more than x items.(Equality is not acceptable to apply a campaign)
            Assert.IsFalse(campaign.IsApplicable(2));
        }

        [Test]
        public void CheckIfApplicable_ShouldntBeApplicable_ExactQuantityLessThanExpectedQuantity()
        {
            var campaign = new Campaign(new Category("Tv"), 50, 2, DiscountType.Amount);
            Assert.IsFalse(campaign.IsApplicable(1));
        }
    }
}

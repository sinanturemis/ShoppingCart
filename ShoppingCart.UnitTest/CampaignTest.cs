using NUnit.Framework;
using ShoppingCart.Business;
using ShoppingCart.Business.Objects;

namespace ShoppingCart.UnitTest
{
    [TestFixture]
    public class CampaignTest
    {
        private Campaign _campaign;
       
        [SetUp]
        public void SetUp()
        {
            _campaign = new Campaign(new Category("Tv"), 50, 2, DiscountType.Amount);
        }

        [Test]
        public void CheckIfApplicable_ShouldBeApplicable_ExactQuantityBiggerThanExpectedQuantity()
        {
            Assert.IsTrue(_campaign.IsApplicable(5));
        }

        [Test]
        public void CheckIfApplicable_ShouldntBeApplicable_ExactQuantityEqualsExpectedQuantity()
        {
            //Request => if bought more than x items.(Equality is not acceptable to apply a campaign)
            Assert.IsFalse(_campaign.IsApplicable(2));
        }

        [Test]
        public void CheckIfApplicable_ShouldntBeApplicable_ExactQuantityLessThanExpectedQuantity()
        {
            Assert.IsFalse(_campaign.IsApplicable(1));
        }
    }
}

using Moq;
using NUnit.Framework;
using ShoppingCart.Business.DiscountStrategy;
using ShoppingCart.Business.Objects;

namespace ShoppingCart.UnitTest
{
    public class DiscountTest
    {
        [Test]
        public void CalculateDiscount_ShouldBeBiggerThanZero_ProductPriceGreaterThanDiscount()
        {
            var totalPriceForTheProduct = 500;

            var discountStrategyMock = new Mock<IDiscountStrategy>();
            discountStrategyMock.Setup(x => x.CalculateDiscount(totalPriceForTheProduct)).Returns(450);

            var discount = new DiscountConcrete(discountStrategyMock.Object);

            Assert.Greater(discount.CalculateDiscount(totalPriceForTheProduct), 0);
        }

        [Test]
        public void CalculateDiscount_ShouldBeZero_ProductPriceEqualsDiscount()
        {
            var totalPriceForTheProduct = 50;

            var discountStrategyMock = new Mock<IDiscountStrategy>();
            discountStrategyMock.Setup(x => x.CalculateDiscount(totalPriceForTheProduct)).Returns(0);

            var discount = new DiscountConcrete(discountStrategyMock.Object);

            Assert.AreEqual(0, discount.CalculateDiscount(totalPriceForTheProduct));
        }

        [Test]
        public void CalculateDiscount_ShouldBeZero_ProductPriceLessThanDiscount()
        {
            var totalPriceForTheProduct = 30;

            var discountStrategyMock = new Mock<IDiscountStrategy>();
            discountStrategyMock.Setup(x => x.CalculateDiscount(totalPriceForTheProduct)).Returns(-20);

            var discount = new DiscountConcrete(discountStrategyMock.Object);

            Assert.AreEqual(0, discount.CalculateDiscount(totalPriceForTheProduct));
        }
    }
}

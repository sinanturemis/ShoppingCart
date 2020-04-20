using Moq;
using NUnit.Framework;
using ShoppingCart.Business.Interfaces;

namespace ShoppingCart.UnitTest.ShoppingCartTests
{
    [TestFixture]
    public abstract class ShoppingCartTest
    {
        protected Business.Objects.ShoppingCart Cart;

        [SetUp]
        public void SetUp()
        {
            var deliveryCostCalculator = new Mock<IDeliveryCostCalculator>();
            deliveryCostCalculator.Setup(x => x.CalculateFor(It.IsAny<IShoppingCart>())).Returns(50);
            Cart = new Business.Objects.ShoppingCart(deliveryCostCalculator.Object);
        }
    }
}

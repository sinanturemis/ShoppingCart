using System;
using Moq;
using NUnit.Framework;
using ShoppingCart.Business.Helpers;
using ShoppingCart.Business.Interfaces;

namespace ShoppingCart.UnitTest
{
    [TestFixture]
    public class DeliveryCostCalculatorTest
    {
        private DeliveryCostCalculator _deliveryCostCalculator;

        [SetUp]
        public void SetUp()
        {
            _deliveryCostCalculator = new DeliveryCostCalculator(4, 2, 2.99);
        }

        [Test]
        public void CalculateFor_ShouldBeZero_EmptyCart()
        {
            var shoppingCart = new Mock<IShoppingCart>();
            shoppingCart.Setup(sc => sc.GetNumberOfDeliveries()).Returns(0);
            shoppingCart.Setup(sc => sc.GetNumberOfProducts()).Returns(0);

            Assert.Zero(_deliveryCostCalculator.CalculateFor(shoppingCart.Object));
        }

        [Test]
        public void CalculateFor_ShouldThrowException_NullShoppingCart()
        {
            /*'CalculateFor' will be calling from ShoppingCart object and its not possible to send a null shopping cart in this scenario.
             Not likely but still an instance can be taken from 'DeliveryCostCalculator' and can be injected a null shopping cart. This test is just for preventing this kind of scenarios.
             */
            Assert.Throws<NullReferenceException>(() => _deliveryCostCalculator.CalculateFor(null));
        }

        [Test]
        public void CalculateFor_ShouldBeExactValue_OneProductWithMultipleDeliveries()
        {
            var shoppingCart = new Mock<IShoppingCart>();
            shoppingCart.Setup(sc => sc.GetNumberOfDeliveries()).Returns(5);
            shoppingCart.Setup(sc => sc.GetNumberOfProducts()).Returns(1);

            /* Formula:
            CostPerDelivery * shoppingCart.GetNumberOfDeliveries() + CostPerProduct * shoppingCart.GetNumberOfProducts() + FixedCost;
            4 * 5 + 2 * 1 + 2.99 => 24.99
             */
            Assert.AreEqual(Math.Round(24.99, 2), Math.Round(_deliveryCostCalculator.CalculateFor(shoppingCart.Object), 2));
        }
    }
}

using NUnit.Framework;
using ShoppingCart.Business.Objects;

namespace ShoppingCart.UnitTest.ShoppingCartTests
{
    [TestFixture]
    public class GetNumberOfDeliveriesTest
    {
        [Test]
        public void GetNumberOfDeliveries_ShouldBeZero_EmptyCart()
        {
            var cart = new Business.Objects.ShoppingCart();
            Assert.Zero(cart.GetNumberOfDeliveries());
        }

        [Test]
        public void GetNumberOfDeliveries_ShouldBeOne_OneProductHasOneCategory()
        {
            var cart = new Business.Objects.ShoppingCart();

            cart.AddItem(new Product("Toshiba Tv", 4000, new Category("Electronic")), 2);

            Assert.AreEqual(1, cart.GetNumberOfDeliveries());
        }

        [Test]
        public void GetNumberOfDeliveries_ShouldBeOne_TwoProductsHaveSameCategory()
        {
            var electronicCategory = new Category("Electronic");
            var toshibaTvProduct = new Product("Toshiba Tv", 4000, electronicCategory);
            var lenovoLaptopProduct = new Product("Lenovo Laptop", 4000, electronicCategory);

            var cart = new Business.Objects.ShoppingCart();
            cart.AddItem(toshibaTvProduct, 4);
            cart.AddItem(lenovoLaptopProduct, 4);

            Assert.AreEqual(1, cart.GetNumberOfDeliveries());
        }

        [Test]
        public void GetNumberOfDeliveries_ShouldBeOne_OneProductHasTwoCategories()
        {
            var electronicCategory = new Category("Electronic");
            var tvCategory = new Category("Tv", electronicCategory);
            var toshibaTvProduct = new Product("Toshiba Tv", 4000, tvCategory);

            var cart = new Business.Objects.ShoppingCart();
            cart.AddItem(toshibaTvProduct, 1);

            Assert.AreEqual(2, cart.GetNumberOfDeliveries());
        }

        [Test]
        public void GetNumberOfDeliveries_ShouldBeTwo_TwoProductsHaveTwoDifferentCategories()
        {
            var cart = new Business.Objects.ShoppingCart();
            cart.AddItem(new Product("Toshiba Tv", 4000, new Category("Electronic")), 4);
            cart.AddItem(new Product("Prima Diaper", 200, new Category("Baby")), 4);

            Assert.AreEqual(2, cart.GetNumberOfDeliveries());
        }
    }
}

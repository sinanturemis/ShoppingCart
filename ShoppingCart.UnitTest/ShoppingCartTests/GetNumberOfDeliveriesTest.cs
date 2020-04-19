using NUnit.Framework;
using ShoppingCart.Business.Objects;

namespace ShoppingCart.UnitTest.ShoppingCartTests
{
    [TestFixture]
    public class GetNumberOfDeliveriesTest : ShoppingCartTest
    {
        [Test]
        public void GetNumberOfDeliveries_ShouldBeZero_EmptyCart()
        {
            Assert.Zero(Cart.GetNumberOfDeliveries());
        }

        [Test]
        public void GetNumberOfDeliveries_ShouldBeOne_OneProductHasOneCategory()
        {
            Cart.AddItem(new Product("Toshiba Tv", 4000, new Category("Electronic")), 2);

            Assert.AreEqual(1, Cart.GetNumberOfDeliveries());
        }

        [Test]
        public void GetNumberOfDeliveries_ShouldBeOne_TwoProductsHaveSameCategory()
        {
            var electronicCategory = new Category("Electronic");
            var toshibaTvProduct = new Product("Toshiba Tv", 4000, electronicCategory);
            var lenovoLaptopProduct = new Product("Lenovo Laptop", 4000, electronicCategory);

            Cart.AddItem(toshibaTvProduct, 4);
            Cart.AddItem(lenovoLaptopProduct, 4);

            Assert.AreEqual(1, Cart.GetNumberOfDeliveries());
        }

        [Test]
        public void GetNumberOfDeliveries_ShouldBeOne_OneProductHasTwoCategories()
        {
            var electronicCategory = new Category("Electronic");
            var tvCategory = new Category("Tv", electronicCategory);
            var toshibaTvProduct = new Product("Toshiba Tv", 4000, tvCategory);

            Cart.AddItem(toshibaTvProduct, 1);

            Assert.AreEqual(2, Cart.GetNumberOfDeliveries());
        }

        [Test]
        public void GetNumberOfDeliveries_ShouldBeTwo_TwoProductsHaveTwoDifferentCategories()
        {
            Cart.AddItem(new Product("Toshiba Tv", 4000, new Category("Electronic")), 4);
            Cart.AddItem(new Product("Prima Diaper", 200, new Category("Baby")), 4);

            Assert.AreEqual(2, Cart.GetNumberOfDeliveries());
        }
    }
}

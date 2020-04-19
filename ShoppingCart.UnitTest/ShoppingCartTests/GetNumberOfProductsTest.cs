using NUnit.Framework;
using ShoppingCart.Business.Objects;

namespace ShoppingCart.UnitTest.ShoppingCartTests
{
    [TestFixture]
    public class GetNumberOfProductsTest: ShoppingCartTest
    {
        [Test]
        public void GetNumberOfProduct_ShouldBeZero_EmptyCart()
        {
            Assert.Zero(Cart.GetNumberOfProducts());
        }

        [Test]
        public void GetNumberOfProduct_ShouldBeOne_OneProductQuantityWithOneProduct()
        {
            Cart.AddItem(new Product("Toshiba Tv", 4000, new Category("Electronic")), 1);

            Assert.AreEqual(1, Cart.GetNumberOfProducts());
        }

        [Test]
        public void GetNumberOfProduct_ShouldBeOne_TwoProductQuantityWithOneProduct()
        {
            Cart.AddItem(new Product("Toshiba Tv", 4000, new Category("Electronic")), 2);

            Assert.AreEqual(1, Cart.GetNumberOfProducts());
        }

        [Test]
        public void GetNumberOfProduct_ShouldBeTwo_ThreeProductQuantityWithTwoProduct()
        {
            var toshibaTvProduct = new Product("Toshiba Tv", 4000, new Category("Electronic"));
            var lenovoLaptopProduct = new Product("Lenovo Laptop", 7000, new Category("Laptop"));

            Cart.AddItem(toshibaTvProduct, 1);
            Cart.AddItem(lenovoLaptopProduct, 2);

            Assert.AreEqual(2, Cart.GetNumberOfProducts());
        }
    }
}

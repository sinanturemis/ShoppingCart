using NUnit.Framework;
using ShoppingCart.Business.Objects;

namespace ShoppingCart.UnitTest.ShoppingCartTests
{
    [TestFixture]
    public class GetNumberOfProductsTest
    {
        [Test]
        public void GetNumberOfProduct_ShouldBeZero_EmptyCart()
        {
            var cart = new Business.Objects.ShoppingCart();
            Assert.Zero(cart.GetNumberOfProducts());
        }

        [Test]
        public void GetNumberOfProduct_ShouldBeOne_OneProductQuantityWithOneProduct()
        {
            var cart = new Business.Objects.ShoppingCart();
            cart.AddItem(new Product("Toshiba Tv", 4000, new Category("Electronic")), 1);

            Assert.AreEqual(1, cart.GetNumberOfProducts());
        }

        [Test]
        public void GetNumberOfProduct_ShouldBeOne_TwoProductQuantityWithOneProduct()
        {
            var cart = new Business.Objects.ShoppingCart();
            cart.AddItem(new Product("Toshiba Tv", 4000, new Category("Electronic")), 2);

            Assert.AreEqual(1, cart.GetNumberOfProducts());
        }

        [Test]
        public void GetNumberOfProduct_ShouldBeTwo_ThreeProductQuantityWithTwoProduct()
        {
            var toshibaTvProduct = new Product("Toshiba Tv", 4000, new Category("Electronic"));
            var lenovoLaptopProduct = new Product("Lenovo Laptop", 7000, new Category("Laptop"));

            var cart = new Business.Objects.ShoppingCart();
            cart.AddItem(toshibaTvProduct, 1);
            cart.AddItem(lenovoLaptopProduct, 2);

            Assert.AreEqual(2, cart.GetNumberOfProducts());
        }
    }
}

using NUnit.Framework;
using ShoppingCart.Business.Objects;

namespace ShoppingCart.UnitTest.ShoppingCartTests
{
    [TestFixture]
    public class GetTotalAmountTest
    {
        [Test]
        public void GetTotalAmount_ShouldBeZero_EmptyCart()
        {
            var cart = new Business.Objects.ShoppingCart();
            Assert.Zero(cart.GetTotalAmount());
        }

        [Test]
        public void GetTotalAmount_ShouldPass_CartWithOneTypeItems()
        {
            var toshibaTvProduct = new Product("Toshiba Tv", 4000, new Category("Electronic"));

            var cart = new Business.Objects.ShoppingCart();
            cart.AddItem(toshibaTvProduct, 1);

            Assert.AreEqual(4000, cart.GetTotalAmount());
        }

        [Test]
        public void GetTotalAmount_ShouldPass_CartWithMultipleTypeItems()
        {
            var toshibaTvProduct = new Product("Toshiba Tv", 4000, new Category("Electronic"));
            var lenovoLaptopProduct = new Product("Lenovo Laptop", 5000, new Category("Laptop"));

            var cart = new Business.Objects.ShoppingCart();
            cart.AddItem(toshibaTvProduct, 1);
            cart.AddItem(lenovoLaptopProduct, 2);

            Assert.AreEqual(14000, cart.GetTotalAmount());
        }
    }
}

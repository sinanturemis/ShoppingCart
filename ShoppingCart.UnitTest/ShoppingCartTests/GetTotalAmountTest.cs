using NUnit.Framework;
using ShoppingCart.Business.Objects;

namespace ShoppingCart.UnitTest.ShoppingCartTests
{
    [TestFixture]
    public class GetTotalAmountTest: ShoppingCartTest
    {
        [Test]
        public void GetTotalAmount_ShouldBeZero_EmptyCart()
        {
            Assert.Zero(Cart.GetTotalAmount());
        }

        [Test]
        public void GetTotalAmount_ShouldPass_CartWithOneTypeItems()
        {
            var toshibaTvProduct = new Product("Toshiba Tv", 4000, new Category("Electronic"));

            Cart.AddItem(toshibaTvProduct, 1);

            Assert.AreEqual(4000, Cart.GetTotalAmount());
        }

        [Test]
        public void GetTotalAmount_ShouldPass_CartWithMultipleTypeItems()
        {
            var toshibaTvProduct = new Product("Toshiba Tv", 4000, new Category("Electronic"));
            var lenovoLaptopProduct = new Product("Lenovo Laptop", 5000, new Category("Laptop"));

            Cart.AddItem(toshibaTvProduct, 1);
            Cart.AddItem(lenovoLaptopProduct, 2);

            Assert.AreEqual(14000, Cart.GetTotalAmount());
        }
    }
}

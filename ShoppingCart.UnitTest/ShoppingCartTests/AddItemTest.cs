using System.Linq;
using NUnit.Framework;
using ShoppingCart.Business.Objects;

namespace ShoppingCart.UnitTest.ShoppingCartTests
{
    [TestFixture]
    public class AddItemTest
    {
        [Test]
        public void AddItem_ShouldBeAddedAsNew_EmptyCart()
        {
            var toshibaTvProduct = new Product("Toshiba Tv", 4000, new Category("Electronic"));

            var cart = new Business.Objects.ShoppingCart();
            var actualResult = cart.AddItem(toshibaTvProduct, 2);
            var cartItems = cart.GetCartItems();

            Assert.IsTrue(actualResult);
            Assert.IsTrue(cartItems.Count == 1);
            Assert.IsTrue(cartItems.Values.Sum(x => x.OrderQuantity) == 2);
        }

        [Test]
        public void AddItem_ShouldBeAddedAsNew_NotExistingProductInCart()
        {
            var cart = new Business.Objects.ShoppingCart();

            var category = new Category("Electronic");
            var toshibaTvProduct = new Product("Toshiba Tv", 4000, category);
            var lenovoLaptopProduct = new Product("Lenovo Laptop", 7000, category);

            var actualResultForFirstAdding = cart.AddItem(toshibaTvProduct, 2);
            var actualResultForSecondAdding = cart.AddItem(lenovoLaptopProduct, 3);

            var cartItems = cart.GetCartItems();

            Assert.IsTrue(actualResultForFirstAdding);
            Assert.IsTrue(actualResultForSecondAdding);
            Assert.IsTrue(cartItems.Count == 2);
            Assert.IsTrue(cartItems.Values.Sum(x => x.OrderQuantity) == 5);
        }

        [Test]
        public void AddItem_ShouldBeAddedOnExistingOne_ExistingProductInCart()
        {
            var cart = new Business.Objects.ShoppingCart();

            var category = new Category("Electronic");
            var toshibaTvProduct = new Product("Toshiba Tv", 4000, category);

            var actualResultForFirstAdding = cart.AddItem(toshibaTvProduct, 2);
            var actualResultForSecondAdding = cart.AddItem(toshibaTvProduct, 3);

            var cartItems = cart.GetCartItems();

            Assert.IsTrue(actualResultForFirstAdding);
            Assert.IsTrue(actualResultForSecondAdding);
            Assert.IsTrue(cartItems.Count == 1);
            Assert.IsTrue(cartItems.Values.Sum(x => x.OrderQuantity) == 5);
        }

        [Test]
        public void AddItem_ShouldBeAddedOnExistingOne_SameProductAsDifferentInstance()
        {
            var cart = new Business.Objects.ShoppingCart();

            var category = new Category("Electronic");
            var toshibaTvProduct = new Product("Toshiba Tv", 4000, category);
            var toshibaTvProduct2 = new Product("Toshiba Tv", 4000, category);

            var actualResultForFirstAdding = cart.AddItem(toshibaTvProduct, 2);
            var actualResultForSecondAdding = cart.AddItem(toshibaTvProduct2, 3);

            var cartItems = cart.GetCartItems();

            Assert.IsTrue(actualResultForFirstAdding);
            Assert.IsTrue(actualResultForSecondAdding);
            Assert.IsTrue(cartItems.Count == 1);
            Assert.IsTrue(cartItems.Values.Sum(x => x.OrderQuantity) == 5);
        }

        [Test]
        public void AddItem_ShouldntBeAdded_NullProductInstance()
        {
            var toshibaTvProduct = new Product("Toshiba Tv", 4000, new Category("Electronic"));
            Product toshibaTvProduct2 = null;

            var cart = new Business.Objects.ShoppingCart();
            var actualResultForFirstAdding = cart.AddItem(toshibaTvProduct, 2);
            var actualResultForSecondAdding = cart.AddItem(toshibaTvProduct2, 3);
            var cartItems = cart.GetCartItems();

            Assert.IsTrue(actualResultForFirstAdding);
            Assert.IsFalse(actualResultForSecondAdding);
            Assert.IsTrue(cartItems.Count == 1);
            Assert.IsTrue(cartItems.Values.Sum(x => x.OrderQuantity) == 2);
        }

        [Test]
        public void AddItem_ShouldntBeAdded_WithZeroQuantity()
        {
            var toshibaTvProduct = new Product("Toshiba Tv", 4000, new Category("Electronic"));

            var cart = new Business.Objects.ShoppingCart();
            var actualResult = cart.AddItem(toshibaTvProduct, 0);
            var cartItems = cart.GetCartItems();

            Assert.IsFalse(actualResult);
            Assert.Zero(cartItems.Count);
            Assert.Zero(cartItems.Values.Sum(x => x.OrderQuantity));
        }

        [Test]
        public void AddItem_ShouldntBeAdded_WithNegativeQuantity()
        {
            var toshibaTvProduct = new Product("Toshiba Tv", 4000, new Category("Electronic"));

            var cart = new Business.Objects.ShoppingCart();
            cart.AddItem(toshibaTvProduct, -3);
            var cartItems = cart.GetCartItems();

            Assert.Zero(cartItems.Count);
            Assert.Zero(cartItems.Values.Sum(x => x.OrderQuantity));
        }

    }
}

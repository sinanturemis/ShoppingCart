using System.Linq;
using NUnit.Framework;
using ShoppingCart.Business;
using ShoppingCart.Business.Objects;

namespace ShoppingCart.UnitTest.ShoppingCartTests
{
    [TestFixture]
    public class AddItemTest : ShoppingCartTest
    {
        [Test]
        public void AddItem_ShouldBeAddedAsNew_EmptyCart()
        {
            var toshibaTvProduct = new Product("Toshiba Tv", 4000, new Category("Electronic"));

            var actualResult = Cart.AddItem(toshibaTvProduct, 2);
            var cartItems = Cart.GetCartItems();

            Assert.IsTrue(actualResult);
            Assert.IsTrue(cartItems.Count == 1);
            Assert.IsTrue(cartItems.Values.Sum(x => x.OrderQuantity) == 2);
        }

        [Test]
        public void AddItem_ShouldBeAddedAsNew_NotExistingProductInCart()
        {
            var category = new Category("Electronic");
            var toshibaTvProduct = new Product("Toshiba Tv", 4000, category);
            var lenovoLaptopProduct = new Product("Lenovo Laptop", 7000, category);

            var actualResultForFirstAdding = Cart.AddItem(toshibaTvProduct, 2);
            var actualResultForSecondAdding = Cart.AddItem(lenovoLaptopProduct, 3);

            var cartItems = Cart.GetCartItems();

            Assert.IsTrue(actualResultForFirstAdding);
            Assert.IsTrue(actualResultForSecondAdding);
            Assert.IsTrue(cartItems.Count == 2);
            Assert.IsTrue(cartItems.Values.Sum(x => x.OrderQuantity) == 5);
        }

        [Test]
        public void AddItem_ShouldBeAddedOnExistingOne_ExistingProductInCart()
        {
            var category = new Category("Electronic");
            var toshibaTvProduct = new Product("Toshiba Tv", 4000, category);

            var actualResultForFirstAdding = Cart.AddItem(toshibaTvProduct, 2);
            var actualResultForSecondAdding = Cart.AddItem(toshibaTvProduct, 3);

            var cartItems = Cart.GetCartItems();

            Assert.IsTrue(actualResultForFirstAdding);
            Assert.IsTrue(actualResultForSecondAdding);
            Assert.IsTrue(cartItems.Count == 1);
            Assert.IsTrue(cartItems.Values.Sum(x => x.OrderQuantity) == 5);
        }

        [Test]
        public void AddItem_ShouldBeAddedOnExistingOne_SameProductAsDifferentInstance()
        {
            var category = new Category("Electronic");
            var toshibaTvProduct = new Product("Toshiba Tv", 4000, category);
            var toshibaTvProduct2 = new Product("Toshiba Tv", 4000, category);

            var actualResultForFirstAdding = Cart.AddItem(toshibaTvProduct, 2);
            var actualResultForSecondAdding = Cart.AddItem(toshibaTvProduct2, 3);

            var cartItems = Cart.GetCartItems();

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

            var actualResultForFirstAdding = Cart.AddItem(toshibaTvProduct, 2);
            var actualResultForSecondAdding = Cart.AddItem(toshibaTvProduct2, 3);
            var cartItems = Cart.GetCartItems();

            Assert.IsTrue(actualResultForFirstAdding);
            Assert.IsFalse(actualResultForSecondAdding);
            Assert.IsTrue(cartItems.Count == 1);
            Assert.IsTrue(cartItems.Values.Sum(x => x.OrderQuantity) == 2);
        }

        [Test]
        public void AddItem_ShouldntBeAdded_WithZeroQuantity()
        {
            var toshibaTvProduct = new Product("Toshiba Tv", 4000, new Category("Electronic"));

            var actualResult = Cart.AddItem(toshibaTvProduct, 0);
            var cartItems = Cart.GetCartItems();

            Assert.IsFalse(actualResult);
            Assert.Zero(cartItems.Count);
            Assert.Zero(cartItems.Values.Sum(x => x.OrderQuantity));
        }

        [Test]
        public void AddItem_ShouldntBeAdded_WithNegativeQuantity()
        {
            var toshibaTvProduct = new Product("Toshiba Tv", 4000, new Category("Electronic"));

            Cart.AddItem(toshibaTvProduct, -3);
            var cartItems = Cart.GetCartItems();

            Assert.Zero(cartItems.Count);
            Assert.Zero(cartItems.Values.Sum(x => x.OrderQuantity));
        }

        [Test]
        public void AddItem_ShouldntBeAdded_WhileThereIsAnAppliedCoupon()
        {
            var toshibaTvProduct = new Product("Toshiba Tv", 4000, new Category("Electronic"));

            var firstAttemptToAdd = Cart.AddItem(toshibaTvProduct, 2);
            Cart.ApplyCoupon(new Coupon(10, 100, DiscountType.Amount));
            var secondAttemptToAdd = Cart.AddItem(toshibaTvProduct, 2);

            Assert.IsTrue(firstAttemptToAdd);
            Assert.IsFalse(secondAttemptToAdd);
        }

        [Test]
        public void AddItem_ShouldntBeAdded_WhileThereIsAnAppliedCampaign()
        {
            var category = new Category("Electronic");
            var toshibaTvProduct = new Product("Toshiba Tv", 4000, category);

            var firstAttemptToAdd = Cart.AddItem(toshibaTvProduct, 2);
            Cart.ApplyDiscounts(new Campaign(category, 50, 1, DiscountType.Amount));
            var secondAttemptToAdd = Cart.AddItem(toshibaTvProduct, 2);

            Assert.IsTrue(firstAttemptToAdd);
            Assert.IsFalse(secondAttemptToAdd);
        }

    }
}

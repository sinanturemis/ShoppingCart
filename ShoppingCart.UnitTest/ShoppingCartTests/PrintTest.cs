using NUnit.Framework;
using ShoppingCart.Business;
using ShoppingCart.Business.Objects;

namespace ShoppingCart.UnitTest.ShoppingCartTests
{
    [TestFixture]
    public class PrintTest : ShoppingCartTest
    {
        [Test]
        public void Print_ShouldBeExactValue_WithEmptyCart()
        {
            var result = Cart.Print();

            Assert.AreEqual(
                @"   Category Name |     Product Name |         Quantity |       Unit Price |       TotalPrice | Campaign Discount | 
                                                            No item in the cart.

Cart Total Amount: 0,00
Cart Net Amount: 0,00
Campaign Discounts: 0,00
Coupon Discount: 0,00
Delivery Cost: 50,00
", result);

        }

        [Test]
        public void Print_ShouldBeExactValue_OneProductWithCampaign()
        {
            var electronicCategory = new Category("Electronic");
            var electronicCampaign = new Campaign(electronicCategory, 100, 1, DiscountType.Amount);
            var electronicProduct = new Product("Toshiba Tv", 4000, electronicCategory);

            Cart.AddItem(electronicProduct, 3);
            Cart.ApplyDiscounts(electronicCampaign);
            var result = Cart.Print();

            Assert.AreEqual(
                @"   Category Name |     Product Name |         Quantity |       Unit Price |       TotalPrice | Campaign Discount | 
      Electronic |       Toshiba Tv |             3,00 |          4000,00 |         12000,00 |           100,00 | 

Cart Total Amount: 12000,00
Cart Net Amount: 11900,00
Campaign Discounts: 100,00
Coupon Discount: 0,00
Delivery Cost: 50,00
", result);

        }

        [Test]
        public void Print_ShouldBeExactValue_OneProductWithCoupon()
        {
            var electronicCategory = new Category("Electronic");
            var electronicProduct = new Product("Toshiba Tv", 4000, electronicCategory);

            Cart.AddItem(electronicProduct, 3);
            Cart.ApplyCoupon(new Coupon(100, 100, DiscountType.Amount));
            var result = Cart.Print();

            Assert.AreEqual(
                @"   Category Name |     Product Name |         Quantity |       Unit Price |       TotalPrice | Campaign Discount | 
      Electronic |       Toshiba Tv |             3,00 |          4000,00 |         12000,00 |             0,00 | 

Cart Total Amount: 12000,00
Cart Net Amount: 11900,00
Campaign Discounts: 0,00
Coupon Discount: 100,00
Delivery Cost: 50,00
", result);
        }

        [Test]
        public void Print_ShouldBeExactValue_TwoSameProductWithCampaign()
        {
            var electronicCategory = new Category("Electronic");
            var electronicCampaign = new Campaign(electronicCategory, 100, 1, DiscountType.Amount);
            var electronicProduct = new Product("Toshiba Tv", 4000, electronicCategory);

            Cart.AddItem(electronicProduct, 3);
            Cart.AddItem(electronicProduct, 2);
            Cart.ApplyDiscounts(electronicCampaign);
            var result = Cart.Print();

            Assert.AreEqual(
                @"   Category Name |     Product Name |         Quantity |       Unit Price |       TotalPrice | Campaign Discount | 
      Electronic |       Toshiba Tv |             5,00 |          4000,00 |         20000,00 |           100,00 | 

Cart Total Amount: 20000,00
Cart Net Amount: 19900,00
Campaign Discounts: 100,00
Coupon Discount: 0,00
Delivery Cost: 50,00
", result);
        }

        [Test]
        public void Print_ShouldBeExactValue_TwoSameProductWithCoupon()
        {
            var electronicCategory = new Category("Electronic");
            var electronicProduct = new Product("Toshiba Tv", 4000, electronicCategory);

            Cart.AddItem(electronicProduct, 3);
            Cart.AddItem(electronicProduct, 2);
            Cart.ApplyCoupon(new Coupon(100, 100, DiscountType.Amount));
            var result = Cart.Print();

            Assert.AreEqual(
                @"   Category Name |     Product Name |         Quantity |       Unit Price |       TotalPrice | Campaign Discount | 
      Electronic |       Toshiba Tv |             5,00 |          4000,00 |         20000,00 |             0,00 | 

Cart Total Amount: 20000,00
Cart Net Amount: 19900,00
Campaign Discounts: 0,00
Coupon Discount: 100,00
Delivery Cost: 50,00
", result);
        }

        [Test]
        public void Print_ShouldBeExactValue_TwoProductsWithDifferentCategories_NoCampaign()
        {
            var electronicProduct = new Product("Toshiba Tv", 4000, new Category("Electronic"));
            var diaperProduct = new Product("Diaper", 160, new Category("Supermarket"));

            Cart.AddItem(electronicProduct, 3);
            Cart.AddItem(diaperProduct, 2);
            var result = Cart.Print();

            Assert.AreEqual(
                @"   Category Name |     Product Name |         Quantity |       Unit Price |       TotalPrice | Campaign Discount | 
      Electronic |       Toshiba Tv |             3,00 |          4000,00 |         12000,00 |             0,00 | 
     Supermarket |           Diaper |             2,00 |           160,00 |           320,00 |             0,00 | 

Cart Total Amount: 12320,00
Cart Net Amount: 12320,00
Campaign Discounts: 0,00
Coupon Discount: 0,00
Delivery Cost: 50,00
", result);
        }

    }
}

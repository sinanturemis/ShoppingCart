using System;
using System.Collections.Generic;
using System.Text;
using Moq;
using NUnit.Framework;
using ShoppingCart.Business.Interfaces;
using ShoppingCart.Business.Objects;

namespace ShoppingCart.UnitTest.ShoppingCartTests
{
    [TestFixture]
    public class PrintTest : ShoppingCartTest
    {
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
      Electronic |       Toshiba Tv |                3 |             4000 |            12000 |              100 | 

Cart Total Amount: 12000
Cart Net Amount: 11900
Campaign Discounts: 100
Coupon Discount: 0
Delivery Cost: 50
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
      Electronic |       Toshiba Tv |                3 |             4000 |            12000 |                0 | 

Cart Total Amount: 12000
Cart Net Amount: 11900
Campaign Discounts: 0
Coupon Discount: 100
Delivery Cost: 50
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
      Electronic |       Toshiba Tv |                5 |             4000 |            20000 |              100 | 

Cart Total Amount: 20000
Cart Net Amount: 19900
Campaign Discounts: 100
Coupon Discount: 0
Delivery Cost: 50
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
      Electronic |       Toshiba Tv |                5 |             4000 |            20000 |                0 | 

Cart Total Amount: 20000
Cart Net Amount: 19900
Campaign Discounts: 0
Coupon Discount: 100
Delivery Cost: 50
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
      Electronic |       Toshiba Tv |                3 |             4000 |            12000 |                0 | 
     Supermarket |           Diaper |                2 |              160 |              320 |                0 | 

Cart Total Amount: 12320
Cart Net Amount: 12320
Campaign Discounts: 0
Coupon Discount: 0
Delivery Cost: 50
", result);
        }

    }
}

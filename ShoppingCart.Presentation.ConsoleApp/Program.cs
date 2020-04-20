using System;
using System.Configuration;
using ShoppingCart.Business.Helpers;
using ShoppingCart.Business.Interfaces;
using ShoppingCart.Business.Objects;

namespace ShoppingCart.Presentation.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var babyCategory = new Category("Baby");
            var electronicCategory = new Category("Electronic");
            var tvCategory = new Category("Tv", electronicCategory);
            var ledTvCategory = new Category("Led Tv", tvCategory);

            var diaper = new Product("Prima Diaper", 160, babyCategory);
            var toshibaTv = new Product("Toshiba Tv", 50, ledTvCategory);
            var samsungTv = new Product("Samsung UHD Tv", 250, tvCategory);

            var campaignForElectronic = new Campaign(electronicCategory, 5, 5, DiscountType.Rate);
            var campaignForLedTv = new Campaign(ledTvCategory, 150, 300, DiscountType.Amount);

            IDeliveryCostCalculator deliveryCostCalculator = new DeliveryCostCalculator(
                double.Parse(ConfigurationManager.AppSettings["CostPerDeliveryInTry"].Replace('.', ',')),
                double.Parse(ConfigurationManager.AppSettings["CostPerProductInTry"].Replace('.', ',')),
                double.Parse(ConfigurationManager.AppSettings["FixedCostInTry"].Replace('.', ',')));

            IShoppingCart cart = new Business.Objects.ShoppingCart(deliveryCostCalculator);
            cart.AddItem(diaper, 10);
            cart.AddItem(toshibaTv, 5);
            cart.AddItem(samsungTv, 1);

            cart.ApplyDiscounts(campaignForElectronic, campaignForLedTv);

            cart.ApplyCoupon(new Coupon(100, 400, DiscountType.Amount));

            Console.WriteLine(cart.Print());
        }
    }
}

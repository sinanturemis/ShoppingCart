using ShoppingCart.Business.Interfaces;

namespace ShoppingCart.Business.Helpers
{
    public class DeliveryCostCalculator : IDeliveryCostCalculator
    {
        public double CostPerDelivery { get; set; }
        public double CostPerProduct { get; set; }
        public double FixedCost { get; set; }

        public DeliveryCostCalculator(double costPerDelivery, double costPerProduct, double fixedCost)
        {
            CostPerDelivery = costPerDelivery;
            CostPerProduct = costPerProduct;
            FixedCost = fixedCost;
        }

        public double CalculateFor(IShoppingCart shoppingCart)
        {
            if (shoppingCart.GetNumberOfProducts() == 0)
                return 0.0;

            return CostPerDelivery * shoppingCart.GetNumberOfDeliveries() +
                   CostPerProduct * shoppingCart.GetNumberOfProducts() +
                   FixedCost;
        }
    }
}

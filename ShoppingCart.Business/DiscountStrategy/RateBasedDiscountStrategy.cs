namespace ShoppingCart.Business.DiscountStrategy
{
    public class RateBasedDiscountStrategy : DiscountBase, IDiscountStrategy
    {
        public RateBasedDiscountStrategy(double discountValue) : base(discountValue)
        {

        }

        public double CalculateDiscount(double amount)
        {
            return (amount * (100 - DiscountValue)) / 100;
        }
    }
}

namespace ShoppingCart.Business.DiscountStrategy
{
    public class RateBasedDiscountStrategy : DiscountBase
    {
        public RateBasedDiscountStrategy(double discountValue) : base(discountValue)
        {

        }

        public override double CalculateDiscount(double amount)
        {
            return (amount * DiscountValue) / 100;
        }
    }
}

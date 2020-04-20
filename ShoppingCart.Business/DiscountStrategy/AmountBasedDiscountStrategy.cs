namespace ShoppingCart.Business.DiscountStrategy
{
    public class AmountBasedDiscountStrategy : DiscountBase
    {
        public AmountBasedDiscountStrategy(double discountValue) : base(discountValue)
        {

        }

        public override double CalculateDiscount(double amount)
        {
            return DiscountValue;
        }
    }
}

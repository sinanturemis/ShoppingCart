namespace ShoppingCart.Business.DiscountStrategy
{
    public abstract class DiscountBase : IDiscountStrategy
    {
        protected double DiscountValue { get; set; }

        protected DiscountBase(double discountValue)
        {
            DiscountValue = discountValue;
        }

        public abstract double CalculateDiscount(double amount);
    }
}

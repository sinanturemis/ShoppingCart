namespace ShoppingCart.Business.DiscountStrategy
{
    public abstract class DiscountBase
    {
        protected double DiscountValue { get; set; }

        protected DiscountBase(double discountValue)
        {
            DiscountValue = discountValue;
        }
    }
}

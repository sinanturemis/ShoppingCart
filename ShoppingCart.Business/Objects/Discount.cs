using ShoppingCart.Business.DiscountStrategy;

namespace ShoppingCart.Business.Objects
{
    public abstract class Discount
    {
        public IDiscountStrategy DiscountStrategy { get; set; }

        protected Discount(IDiscountStrategy discountStrategy)
        {
            DiscountStrategy = discountStrategy;
        }

        public double CalculateDiscount(double amount)
        {
            var discount = this.DiscountStrategy.CalculateDiscount(amount);
            return discount < 0 ? 0 : discount;
        }

    }
}

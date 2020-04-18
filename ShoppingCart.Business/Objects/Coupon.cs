using ShoppingCart.Business.DiscountStrategy;
using ShoppingCart.Business.Factories;

namespace ShoppingCart.Business.Objects
{
    public class Coupon
    {
        public decimal MinimumPurchaseAmount { get; set; }
        public IDiscountStrategy DiscountStrategy { get; set; }

        public Coupon(decimal minimumPurchaseAmount, int discountValue, DiscountType discountType)
        {
            MinimumPurchaseAmount = minimumPurchaseAmount;
            DiscountStrategy = DiscountFactory.GetDiscountStrategy(discountType, discountValue);
        }

        public Coupon(decimal minimumPurchaseAmount, IDiscountStrategy discountStrategy)
        {
            MinimumPurchaseAmount = minimumPurchaseAmount;
            DiscountStrategy = discountStrategy;
        }

        public bool IsApplicable(decimal cartPurchaseAmount)
        {
            return this.MinimumPurchaseAmount <= cartPurchaseAmount;
        }
    }
}

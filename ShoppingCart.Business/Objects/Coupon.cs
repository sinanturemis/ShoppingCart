using ShoppingCart.Business.Factories;

namespace ShoppingCart.Business.Objects
{
    public class Coupon : Discount
    {
        public double MinimumPurchaseAmount { get; set; }

        public Coupon(double minimumPurchaseAmount, int discountValue, DiscountType discountType) :
            base(DiscountFactory.GetDiscountStrategy(discountType, discountValue))
        {
            MinimumPurchaseAmount = minimumPurchaseAmount;
        }

        public bool IsApplicable(double cartPurchaseAmount)
        {
            return this.MinimumPurchaseAmount <= cartPurchaseAmount;
        }
    }
}

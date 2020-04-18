using ShoppingCart.Business.DiscountStrategy;
using ShoppingCart.Business.Factories;

namespace ShoppingCart.Business.Objects
{
    public class Campaign
    {
        public Category Category { get; set; }
        public int MinimumQuantity { get; set; }
        public IDiscountStrategy DiscountStrategy { get; set; }

        public Campaign(Category category, double discountValue, int minimumQuantity, DiscountType discountType)
        {
            Category = category;
            MinimumQuantity = minimumQuantity;
            DiscountStrategy = DiscountFactory.GetDiscountStrategy(discountType, discountValue);
        }

        public Campaign(Category category, int minimumQuantity, IDiscountStrategy discountStrategy)
        {
            Category = category;
            MinimumQuantity = minimumQuantity;
            DiscountStrategy = discountStrategy;
        }

        public bool IsApplicable(int orderQuantity)
        {
            return this.MinimumQuantity < orderQuantity;
        }

        public double CalculateDiscount(double amount)
        {
            var discount = this.DiscountStrategy.CalculateDiscount(amount);
            return discount < 0 ? 0 : discount;
        }
    }
}

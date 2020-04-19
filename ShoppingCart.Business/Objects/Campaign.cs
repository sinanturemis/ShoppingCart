using ShoppingCart.Business.Factories;

namespace ShoppingCart.Business.Objects
{
    public class Campaign : Discount
    {
        public Category Category { get; set; }
        public double MinimumQuantity { get; set; }

        public Campaign(Category category, double discountValue, double minimumQuantity, DiscountType discountType) :
            base(DiscountFactory.GetDiscountStrategy(discountType, discountValue))
        {
            Category = category;
            MinimumQuantity = minimumQuantity;
        }

        public bool IsApplicable(double orderQuantity)
        {
            return this.MinimumQuantity < orderQuantity;
        }
    }
}

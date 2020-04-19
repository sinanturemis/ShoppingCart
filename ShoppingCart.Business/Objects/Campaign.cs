using ShoppingCart.Business.Factories;

namespace ShoppingCart.Business.Objects
{
    public class Campaign : Discount
    {
        public string Name { get; set; }
        public Category Category { get; set; }
        public int MinimumQuantity { get; set; }

        public Campaign(Category category, double discountValue, int minimumQuantity, DiscountType discountType) :
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

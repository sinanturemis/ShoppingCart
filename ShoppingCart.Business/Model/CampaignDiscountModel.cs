using ShoppingCart.Business.Objects;

namespace ShoppingCart.Business.Model
{
    public class CampaignDiscountModel
    {
        public Campaign Campaign { get; set; }
        public double DiscountAmount { get; set; }

        public CampaignDiscountModel Update(Campaign campaign, double discountAmount)
        {
            Campaign = campaign;
            DiscountAmount = discountAmount;
            return this;
        }

    }
}

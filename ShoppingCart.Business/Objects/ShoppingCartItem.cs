using System.Collections.Generic;
using System.Linq;
using ShoppingCart.Business.Model;

namespace ShoppingCart.Business.Objects
{
    public class ShoppingCartItem
    {
        public Product Product { get; set; }
        public double OrderQuantity { get; set; }
        public double TotalPrice => this.Product.UnitPrice * OrderQuantity;
        public CampaignDiscountModel BestCampaignDiscount { get; set; }

        public ShoppingCartItem(Product product, double orderQuantity)
        {
            this.Product = product;
            this.OrderQuantity = orderQuantity;
            this.BestCampaignDiscount = new CampaignDiscountModel();
        }

        public ShoppingCartItem AddQuantity(double quantity)
        {
            OrderQuantity += quantity;
            return this;
        }

        public void ApplyBestCampaign(IEnumerable<Campaign> campaigns)
        {
            // Get all categories of product (get current and its ancestors categories)
            var productCategories = this.Product.GetAllCategories();

            //Only campaigns that has same category with product can be applied. So, filter campaigns if they are in those categories.
            var applicableCampaigns = campaigns.Where(c => productCategories.Contains(c.Category));

            foreach (var campaign in applicableCampaigns)
            {
                //Check if applicable and calculate the discount if its applicable otherwise discount is 0.
                var discountAmount = campaign.IsApplicable(this.OrderQuantity) ? campaign.CalculateDiscount(this.OrderQuantity * this.Product.UnitPrice) : 0;

                if (this.BestCampaignDiscount.DiscountAmount < discountAmount)
                    this.BestCampaignDiscount = this.BestCampaignDiscount.Update(campaign, discountAmount);
            }
        }
    }
}

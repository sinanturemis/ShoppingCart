using System.Collections.Generic;
using System.Linq;
using ShoppingCart.Business.Model;

namespace ShoppingCart.Business.Objects
{
    public class ShoppingCartItem
    {
        public Product Product { get; set; }
        public double OrderQuantity { get; set; }
        public double TotalAmount { get; set; }
        public CampaignDiscountModel BestCampaignDiscount { get; set; }

        public ShoppingCartItem(Product product, double orderQuantity)
        {
            this.Product = product;
            this.OrderQuantity = orderQuantity;
            this.BestCampaignDiscount = new CampaignDiscountModel();
            UpdateTotalAmount();
        }


        public ShoppingCartItem AddQuantity(double quantity)
        {
            OrderQuantity += quantity;
            UpdateTotalAmount();
            return this;
        }

        private void UpdateTotalAmount()
        {
            this.TotalAmount = Product.Price * OrderQuantity;
        }

        public void ApplyBestCampaign(List<Campaign> campaigns)
        {
            var productCategories = this.Product.GetAllCategories();
            var validCampaigns = campaigns.Where(c => productCategories.Contains(c.Category));

            foreach (var campaign in validCampaigns)
            {
                var discountAmount = campaign.IsApplicable(this.OrderQuantity) ?
                    campaign.CalculateDiscount(this.OrderQuantity * this.Product.Price) : 0;

                if (this.BestCampaignDiscount.DiscountAmount < discountAmount)
                    this.BestCampaignDiscount = this.BestCampaignDiscount.Update(campaign, discountAmount);
            }
        }
    }
}

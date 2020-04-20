using System;
using System.Collections.Generic;
using System.Linq;
using ShoppingCart.Business.Interfaces;

namespace ShoppingCart.Business.Objects
{
    public class ShoppingCart : IShoppingCart
    {
        private Dictionary<string, ShoppingCartItem> CartItems { get; }

        private IDeliveryCostCalculator DeliveryCostCalculator { get; }

        public bool HasCampaignsApplied { get; set; }
        public List<Campaign> AssignedCampaigns { get; set; }

        public bool HasCouponApplied { get; set; }
        public Coupon AppliedCoupon { get; set; }
        public double CouponDiscount { get; set; }

        public ShoppingCart(IDeliveryCostCalculator deliveryCostCalculator)
        {
            CartItems = new Dictionary<string, ShoppingCartItem>();
            AssignedCampaigns = new List<Campaign>();
            DeliveryCostCalculator = deliveryCostCalculator;
        }

        #region Item

        public bool AddItem(Product product, double orderQuantity)
        {
            if (product == null || orderQuantity <= 0)
                return false; //Invalid Products

            if (CartItems.ContainsKey(product.Title))
                CartItems[product.Title] = CartItems[product.Title].AddQuantity(orderQuantity);
            else
                CartItems.Add(product.Title, new ShoppingCartItem(product, orderQuantity));

            return true;
        }

        public Dictionary<string, ShoppingCartItem> GetCartItems()
        {
            return CartItems;
        }

        #endregion Item

        #region Campaign Discounts

        public bool ApplyDiscounts(params Campaign[] campaigns)
        {
            if (HasCouponApplied)
                return false; //you cannot apply a campaign after coupon is applied. Please remove it first.

            //Get only campaigns that haven't applied previously.
            var newCampaigns = campaigns.Where(ac => !AssignedCampaigns.Contains(ac)).ToList();
            if (!newCampaigns.Any())
                return false;

            AssignedCampaigns.AddRange(newCampaigns);
            HasCampaignsApplied = true;
            return HasCampaignsApplied;
        }

        public double GetCampaignDiscount()
        {
            foreach (var shoppingCartItem in CartItems.Values)
            {
                shoppingCartItem.ApplyBestCampaign(AssignedCampaigns);
            }

            return CartItems.Values.Sum(x => x.BestCampaignDiscount.DiscountAmount);
        }

        #endregion

        #region Coupon

        public bool ApplyCoupon(Coupon coupon)
        {
            if (HasCouponApplied || CartItems.Count == 0)
                return false; //A coupon is already applied before. || There is item to apply a coupon.

            var amountAfterCampaignDiscount = GetTotalAmount() - GetCampaignDiscount();
            if (coupon.IsApplicable(amountAfterCampaignDiscount))
            {
                AppliedCoupon = coupon;
                CouponDiscount = coupon.CalculateDiscount(amountAfterCampaignDiscount);
                HasCouponApplied = true;
            }

            return this.HasCouponApplied;
        }

        public bool RemoveCoupon()
        {
            if (!HasCouponApplied)
                return false; //there is no coupon to remove.

            AppliedCoupon = null;
            CouponDiscount = 0;
            HasCouponApplied = false;

            return true;
        }

        public double GetCouponDiscount()
        {
            return CouponDiscount;
        }

        #endregion

        #region Delivery

        public double GetNumberOfDeliveries()
        {
            return CartItems.SelectMany(x => x.Value.Product.GetAllCategories()).Distinct().Count();
        }

        public int GetNumberOfProducts()
        {
            return CartItems.Count;
        }

        #endregion Delivery

        #region CartAmount

        public double GetTotalAmountAfterDiscounts()
        {
            var totalAmountAfterDiscount = GetTotalAmount() - GetCampaignDiscount() - GetCouponDiscount();
            return totalAmountAfterDiscount < 0 ? 0 : totalAmountAfterDiscount;
        }

        public double GetTotalAmount()
        {
            return CartItems.Values.Sum(x => x.TotalAmount);
        }

        #endregion

        #region Print

        public string Print()
        {
            var deliveryCost = DeliveryCostCalculator.CalculateFor(this);
            return string.Empty;
            //TODO:Think using builder pattern
        }

        #endregion

    }
}

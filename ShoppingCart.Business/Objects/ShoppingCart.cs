using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ShoppingCart.Business.Interfaces;

namespace ShoppingCart.Business.Objects
{
    public class ShoppingCart : IShoppingCart
    {
        private Dictionary<string, ShoppingCartItem> CartItems { get; }

        private IDeliveryCostCalculator DeliveryCostCalculator { get; }

        public bool HasCampaignsApplied { get; set; }
        public bool HasCouponApplied { get; set; }
        public double CouponDiscount { get; set; }

        public ShoppingCart(IDeliveryCostCalculator deliveryCostCalculator)
        {
            CartItems = new Dictionary<string, ShoppingCartItem>();
            DeliveryCostCalculator = deliveryCostCalculator;
        }

        #region Item

        public bool AddItem(Product product, double orderQuantity)
        {
            if (HasCouponApplied || HasCampaignsApplied)
                return false; //You cannot add a product in cart after applying campaign or coupon

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
            if (HasCouponApplied || HasCampaignsApplied)
                return false; //you cannot apply a campaign after coupon is applied. Please remove it first. || campaigns are already applied.

            foreach (var shoppingCartItem in CartItems.Values)
            {
                shoppingCartItem.ApplyBestCampaign(campaigns.ToList());
            }

            HasCampaignsApplied = true;
            return HasCampaignsApplied;
        }

        public double GetCampaignDiscount()
        {
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
                CouponDiscount = coupon.CalculateDiscount(amountAfterCampaignDiscount);
                HasCouponApplied = true;
            }

            return this.HasCouponApplied;
        }

        public bool RemoveCoupon()
        {
            if (!HasCouponApplied)
                return false; //there is no coupon to remove.

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
            return CartItems.Values.Sum(x => x.TotalPrice);
        }

        #endregion

        #region Print

        public string Print()
        {
            var groupedProductsByCategory = CartItems
                .GroupBy(x => x.Value.Product.Category.Title)
                .ToDictionary(x => x.Key, x => x.ToList());

            var builder = new StringBuilder();
            builder.AppendLine($"{FormatInfoField("Category Name")}" +
                               $"{FormatInfoField("Product Name")}" +
                               $"{FormatInfoField("Quantity")}" +
                               $"{FormatInfoField("Unit Price")}" +
                               $"{FormatInfoField("TotalPrice")}" +
                               $"{FormatInfoField("Campaign Discount")}");

            foreach (var categoryGroup in groupedProductsByCategory)
            {
                foreach (var product in categoryGroup.Value)
                {
                    builder.AppendLine($"{FormatInfoField(categoryGroup.Key)}" +
                                       $"{FormatInfoField(product.Value.Product.Title)}" +
                                       $"{FormatInfoField(product.Value.OrderQuantity)}" +
                                       $"{FormatInfoField(product.Value.Product.Price)}" +
                                       $"{FormatInfoField(product.Value.TotalPrice)}" +
                                       $"{FormatInfoField(product.Value.BestCampaignDiscount.DiscountAmount)}");
                    //We cannot add coupon discount here because it can be applied on cart - not on each shopping item
                }
            }

            builder.AppendLine($"{Environment.NewLine}" +
                               $"Cart Total Amount: {Math.Round(GetTotalAmount(), 2)}{Environment.NewLine}" +
                               $"Cart Net Amount: {Math.Round(GetTotalAmountAfterDiscounts(), 2)}{Environment.NewLine}" +
                               $"Campaign Discounts: {Math.Round(GetCampaignDiscount(), 2)}{Environment.NewLine}" +
                               $"Coupon Discount: {Math.Round(GetCouponDiscount(), 2)}{Environment.NewLine}" +
                               $"Delivery Cost: {Math.Round(DeliveryCostCalculator.CalculateFor(this), 2)}");

            return builder.ToString();
            //TODO:Think using builder pattern
        }

        private string FormatInfoField(string value)
        {
            return $"{value,Constants.FieldLengthOnDisplay}{Constants.FieldSeperatorOnDisplay}";
        }
        private string FormatInfoField(double value)
        {
            return $"{value,Constants.FieldLengthOnDisplay}{Constants.FieldSeperatorOnDisplay}";
        }

        #endregion

    }
}

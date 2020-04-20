using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ShoppingCart.Business.Extensions;
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
                return false; //Invalid item to add

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
                shoppingCartItem.ApplyBestCampaign(campaigns);
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
                return false; //A coupon is already applied before. || There is no item to apply a coupon.

            var netAmount = GetTotalAmount() - GetCampaignDiscount();

            if (coupon.IsApplicable(netAmount))
            {
                CouponDiscount = coupon.CalculateDiscount(netAmount);
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
            //This business can be moved in a different object or structure but there is no request to make that new object reusable/extensible etc. Can be implemented here for now.

            //Structure => <categoryTitle , products in this category>
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

            if (groupedProductsByCategory.Any())
            {
                foreach (var categoryGroup in groupedProductsByCategory)
                {
                    var shoppingCartItems = categoryGroup.Value.Select(x => x.Value);
                    foreach (var shoppingCartItem in shoppingCartItems)
                    {
                        builder.AppendLine($"{FormatInfoField(categoryGroup.Key)}" +
                                           $"{FormatInfoField(shoppingCartItem.Product.Title)}" +
                                           $"{FormatInfoField(shoppingCartItem.OrderQuantity.ToCurrencyString())}" +
                                           $"{FormatInfoField(shoppingCartItem.Product.UnitPrice.ToCurrencyString())}" +
                                           $"{FormatInfoField(shoppingCartItem.TotalPrice.ToCurrencyString())}" +
                                           $"{FormatInfoField(shoppingCartItem.BestCampaignDiscount.DiscountAmount.ToCurrencyString())}");
                        //In request, its wanted but we cannot add coupon discount here because a coupon can be applied on cart - not on each shopping item
                    }
                }
            }
            else
            {
                builder.AppendLine($"{"No item in the cart.",80}");
            }

            builder.AppendLine($"{Environment.NewLine}" +
                               $"{FormatFooterField("Cart Total Amount", GetTotalAmount(), true)}" +
                               $"{FormatFooterField("Cart Net Amount", GetTotalAmountAfterDiscounts(), true)}" +
                               $"{FormatFooterField("Campaign Discounts", GetCampaignDiscount(), true)}" +
                               $"{FormatFooterField("Coupon Discount", GetCouponDiscount(), true)}" +
                               $"{FormatFooterField("Delivery Cost", DeliveryCostCalculator.CalculateFor(this), false)}");

            return builder.ToString();
        }

        private string FormatInfoField(string value)
        {
            return $"{value,Constants.FieldLengthOnDisplay}{Constants.FieldSeparatorOnDisplay}";
        }

        private string FormatFooterField(string label, double amount, bool appendNewLine)
        {
            return $"{label}: {amount.ToCurrencyString()}{(appendNewLine ? Environment.NewLine : string.Empty)}";
        }

        #endregion

    }
}

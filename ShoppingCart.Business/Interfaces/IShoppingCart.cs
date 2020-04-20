using System.Collections.Generic;
using ShoppingCart.Business.Objects;

namespace ShoppingCart.Business.Interfaces
{
    public interface IShoppingCart
    {
        bool AddItem(Product product, double orderQuantity);
        Dictionary<string, ShoppingCartItem> GetCartItems();
        bool ApplyDiscounts(params Campaign[] campaigns);
        double GetCampaignDiscount();
        bool ApplyCoupon(Coupon coupon);
        bool RemoveCoupon();
        double GetCouponDiscount();
        double GetNumberOfDeliveries();
        int GetNumberOfProducts();
        double GetTotalAmountAfterDiscounts();
        double GetTotalAmount();
        string Print();
    }
}

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

        /* This one is not requested but the request says 'if a coupon has applied you cannot apply a campaign'. Not to be locked after adding any campaign, this ability is required. This is the reason why I implemented this.*/
        bool RemoveCoupon();
        double GetCouponDiscount();
        double GetNumberOfDeliveries();
        int GetNumberOfProducts();
        double GetTotalAmountAfterDiscounts();
        double GetTotalAmount();
        string Print();
    }
}

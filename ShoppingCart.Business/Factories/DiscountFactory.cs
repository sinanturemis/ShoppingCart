using System;
using ShoppingCart.Business.DiscountStrategy;
using ShoppingCart.Business.Objects;

namespace ShoppingCart.Business.Factories
{
    public static class DiscountFactory
    {
        public static IDiscountStrategy GetDiscountStrategy(DiscountType discountType, double discountValue)
        {
            switch (discountType)
            {
                case DiscountType.Rate: return new RateBasedDiscountStrategy(discountValue);
                case DiscountType.Amount: return new AmountBasedDiscountStrategy(discountValue);
                default:
                    throw new ArgumentOutOfRangeException(nameof(discountType), discountType, null);
            }
        }
    }
}

using System;

namespace ShoppingCart.Business.DiscountStrategy
{
    public class AmountBasedDiscountStrategy : DiscountBase, IDiscountStrategy
    {
        public AmountBasedDiscountStrategy(double discountValue) : base(discountValue)
        {

        }

        public double CalculateDiscount(double amount)
        {
            return amount - DiscountValue;
        }
    }
}

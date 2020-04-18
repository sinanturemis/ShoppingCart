namespace ShoppingCart.Business.DiscountStrategy
{
    public interface IDiscountStrategy
    {
        double CalculateDiscount(double amount);
    }
}

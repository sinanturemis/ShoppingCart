namespace ShoppingCart.Business.Interfaces
{
    public interface IDeliveryCostCalculator
    {
        double CalculateFor(IShoppingCart shoppingCart);
    }
}

namespace ShoppingCart.Business.Interfaces
{

    public interface IDeliveryCostCalculator
    {
        /*
         * Reason of using IShoppingCart instead of ShoppingCart:
         * Different kind of shopping cart implementation (currently there isn't but can be developed in future) can be passed as long as has same calculation logic. So, better to use it's abstracted version (interface).
         */
        double CalculateFor(IShoppingCart shoppingCart);
    }
}

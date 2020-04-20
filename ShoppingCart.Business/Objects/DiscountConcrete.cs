using ShoppingCart.Business.DiscountStrategy;

namespace ShoppingCart.Business.Objects
{
    /*
     * This class is just for unit testing. Not for using in development.
     * Because its not possible to create an instance from abstract classes while developing unit tests, this class is created to use in unit test to test abstract class.
     */
    public class DiscountConcrete : Discount
    {
        public DiscountConcrete(IDiscountStrategy discountStrategy) : base(discountStrategy)
        {
        }
    }
}

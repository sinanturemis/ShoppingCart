using System;
using System.Collections.Generic;
using System.Text;
using ShoppingCart.Business.DiscountStrategy;

namespace ShoppingCart.Business.Objects
{
    /*
     * This class is just for unit testing. Not for using in development.
     * Because its not possible to create an instance from abstract classes while developing unit tests, this class is created.
     */
    public class DiscountConcrete : Discount
    {
        public DiscountConcrete(IDiscountStrategy discountStrategy) : base(discountStrategy)
        {
        }
    }
}

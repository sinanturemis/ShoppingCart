namespace ShoppingCart.Business.Extensions
{
    public static class DoubleExtensions
    {
        public static string ToCurrencyString(this double value)
        {
            return value.ToString("0.00");
        }
    }
}

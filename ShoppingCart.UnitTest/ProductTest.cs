using System.Linq;
using NUnit.Framework;
using ShoppingCart.Business.Objects;

namespace ShoppingCart.UnitTest
{
    [TestFixture]
    public class ProductTest
    {
        #region GetAllCategories

        [Test]
        public void GetAllCategories_ShouldReturnOneCategory_HasRootCategory()
        {
            var product = new Product("Toshiba Tv", 4000, new Category("Electronic"));
            var actualCategories = product.GetAllCategories();

            Assert.AreEqual(1, actualCategories.Count);
            Assert.IsTrue(actualCategories.First().Title == "Electronic");
        }

        [Test]
        public void GetAllCategories_ShouldReturnAllParentCategories_HasLeafCategory()
        {
            var ledCategory = new Category("Led", new Category("Tv", new Category("Electronic")));

            var product = new Product("Toshiba Tv", 5.40, ledCategory);

            var actualCategories = product.GetAllCategories().ToList();

            Assert.AreEqual(3, actualCategories.Count);
            Assert.IsTrue(actualCategories[0].Title == "Led");
            Assert.IsTrue(actualCategories[1].Title == "Tv");
            Assert.IsTrue(actualCategories[2].Title == "Electronic");
        }

        #endregion
    }
}

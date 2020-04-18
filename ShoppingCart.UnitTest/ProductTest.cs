using System.Linq;
using Moq;
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
            var fruitCategoryMock = new Mock<Category>("Fruit", null);
            fruitCategoryMock.Setup(x => x.HasParent()).Returns(false);
            fruitCategoryMock.Setup(x => x.GetParent()).Returns((Category)null);

            var product = new Product("Apple", 5.40, fruitCategoryMock.Object);

            var actualCategories = product.GetAllCategories();
            Assert.AreEqual(1, actualCategories.Count);
            Assert.IsTrue(actualCategories.First().Title == "Fruit");
        }

        [Test]
        public void GetAllCategories_ShouldReturnAllParentCategories_HasLeafCategory()
        {
            var supermarketCategoryMock = new Mock<Category>("Supermarket", null);
            supermarketCategoryMock.Setup(x => x.HasParent()).Returns(false);
            supermarketCategoryMock.Setup(x => x.GetParent()).Returns((Category)null);

            var foodCategoryMock = new Mock<Category>("Food", supermarketCategoryMock.Object);
            foodCategoryMock.Setup(x => x.HasParent()).Returns(true);
            foodCategoryMock.Setup(x => x.GetParent()).Returns(supermarketCategoryMock.Object);

            var fruitCategoryMock = new Mock<Category>("Fruit", foodCategoryMock.Object);
            fruitCategoryMock.Setup(x => x.HasParent()).Returns(true);
            fruitCategoryMock.Setup(x => x.GetParent()).Returns(foodCategoryMock.Object);

            var product = new Product("Apple", 5.40, fruitCategoryMock.Object);

            var actualCategories = product.GetAllCategories().ToList();

            Assert.AreEqual(3, actualCategories.Count);

            Assert.IsTrue(actualCategories[0].Title == "Fruit");
            Assert.IsTrue(actualCategories[1].Title == "Food");
            Assert.IsTrue(actualCategories[2].Title == "Supermarket");
        }

        #endregion
    }
}

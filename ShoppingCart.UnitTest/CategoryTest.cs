using NUnit.Framework;
using ShoppingCart.Business.Objects;

namespace ShoppingCart.UnitTest
{
    [TestFixture]
    public class CategoryTest
    {
        #region HasParent

        [Test]
        public void HasParent_ShouldHave_WithParentCategory()
        {
            var category = new Category("Child Category", new Category("Parent Category"));
            Assert.IsTrue(category.HasParent());
        }

        [Test]
        public void HasParent_ShouldntHave_WithoutParentCategory()
        {
            var category = new Category("Child Category");
            Assert.IsFalse(category.HasParent());
        }

        #endregion

        #region GetParent

        [Test]
        public void GetParent_ShouldReturn_WithParentCategory()
        {
            var category = new Category("Child Category", new Category("Parent Category"));
            Assert.NotNull(category.GetParent());
        }

        [Test]
        public void GetParent_ShouldntReturn_WithoutParentCategory()
        {
            var category = new Category("Child Category");
            Assert.IsNull(category.GetParent());
        }


        #endregion
    }
}

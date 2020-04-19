using NUnit.Framework;
using ShoppingCart.Business.Objects;

namespace ShoppingCart.UnitTest
{
    [TestFixture]
    public class CategoryTest
    {
        private Category _categoryWithParentCategory;
        private Category _categoryWithoutParentCategory;

        [SetUp]
        public void SetUp()
        {
            _categoryWithParentCategory = new Category("Child Category", new Category("Parent Category"));
            _categoryWithoutParentCategory = new Category("Child Category");
        }

        #region HasParent

        [Test]
        public void HasParent_ShouldHave_WithParentCategory()
        {
            Assert.IsTrue(_categoryWithParentCategory.HasParent());
        }

        [Test]
        public void HasParent_ShouldntHave_WithoutParentCategory()
        {
            Assert.IsFalse(_categoryWithoutParentCategory.HasParent());
        }

        #endregion

        #region GetParent

        [Test]
        public void GetParent_ShouldReturn_WithParentCategory()
        {
            Assert.NotNull(_categoryWithParentCategory.GetParent());
        }

        [Test]
        public void GetParent_ShouldntReturn_WithoutParentCategory()
        {
            Assert.IsNull(_categoryWithoutParentCategory.GetParent());
        }


        #endregion
    }
}

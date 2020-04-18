using System.Runtime.CompilerServices;

namespace ShoppingCart.Business.Objects
{
    public class Category
    {
        public string Title { get; set; }

        public Category ParentCategory { get; set; }

        //if parent category is null, so its a root category
        public Category(string title, Category parentCategory = null)
        {
            Title = title;
            ParentCategory = parentCategory;
        }

        public virtual bool HasParent()
        {
            return ParentCategory != null;
        }

        public virtual Category GetParent()
        {
            return ParentCategory;
        }
    }
}

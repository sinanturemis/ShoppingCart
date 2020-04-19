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

        public bool HasParent()
        {
            return ParentCategory != null;
        }

        public Category GetParent()
        {
            return ParentCategory;
        }
    }
}

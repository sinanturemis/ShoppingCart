using System.Collections.Generic;

namespace ShoppingCart.Business.Objects
{
    public class Product
    {
        public string Title { get; set; }
        public double UnitPrice { get; set; }
        public Category Category { get; set; }

        public Product(string title, double price, Category category)
        {
            this.Title = title;
            this.UnitPrice = price;
            this.Category = category;
        }

        public ICollection<Category> GetAllCategories()
        {
            var currentCategory = this.Category;
            var list = new List<Category> { currentCategory };

            while (currentCategory.HasParent())
            {
                currentCategory = currentCategory.GetParent();
                list.Add(currentCategory);
            }

            return list;
        }


    }
}

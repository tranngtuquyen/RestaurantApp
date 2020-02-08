using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RestaurantApp
{
    static class MenuManager
    {
        public static Category CreateCategory(string categoryName)
        {
            var category = new Category
            {
                Name = categoryName
            };

            return category;
        }

        public static MenuItem CreateMenuItem(string itemName, string itemDescription, decimal price, Category category)
        {
            var menuItem = new MenuItem
            {
                Name = itemName,
                Description = itemDescription,
                Price = price,
                Category = category
            };

            return menuItem;
        }

        public static Category GetCategoryByID(int id, List<Category> categories)
        {
            return categories.SingleOrDefault(c => c.ID == id);
        }
        public static MenuItem GetMenuItemByID(int id, List<MenuItem> menuItems)
        {
            return menuItems.SingleOrDefault(i => i.ID == id);
        }
    }
}

using System;
using System.Collections.Generic;
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
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantApp
{
    class Menu
    {
        private static int lastIDNumber = 0;
        public int ID { get; private set; }
        public List<MenuItem> MenuItems { get; private set; }
        public List<Category> Categories { get; private set; }

        public Menu()
        {
            ID = ++lastIDNumber;
            MenuItems = new List<MenuItem>();
            Categories = new List<Category>();
        }

        public void AddCategory(Category category)
        {
            Categories.Add(category);
        }

        public void AddMenuItem(MenuItem menuItem)
        {
            MenuItems.Add(menuItem);
        }
    }
}

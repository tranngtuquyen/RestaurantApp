using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RestaurantApp
{
    static class MenuManager
    {
        public static RestaurantContext DB;

        public static Menu CreateMenu(string name)
        {
            var menu = new Menu
            {
                Name = name
            };

            DB.Menus.Add(menu);
            DB.SaveChanges();
            return DB.Menus.SingleOrDefault(m => m.ID == menu.ID);
        }

        public static Category CreateCategory(string categoryName, Menu menu)
        {
            var checkNull = DB.Categories.SingleOrDefault(c => c.Name == categoryName && c.Menu == menu);
            
            if (checkNull != null)
            {
                throw new ArgumentException($"{categoryName} has been used! Try again.");
            }

            var category = new Category
            {
                Name = categoryName,
                Menu = menu,
                MenuID = menu.ID
            };

            DB.Categories.Add(category);
            DB.SaveChanges();
            return DB.Categories.SingleOrDefault(c => c.ID == category.ID);
        }

        public static MenuItem CreateMenuItem(string itemName, string itemDescription, decimal price, Category category, Menu menu)
        {
            if (price < 0 )
            {
                throw new ArgumentOutOfRangeException("price", "Menu item price cannot be negative!");
            }

            var checkNull = DB.MenuItems.SingleOrDefault(i => i.Name == itemName && i.Category == category && i.Menu == menu);

            if (checkNull != null)
            {
                throw new ArgumentException("Item name has been existed.");
            }
            var menuItem = new MenuItem
            {
                Name = itemName,
                Description = itemDescription,
                Price = price,
                Category = category,
                CategoryID = category.ID,
                Menu = menu,
                MenuID = menu.ID
            };
            
            DB.MenuItems.Add(menuItem);
            DB.SaveChanges();
            return DB.MenuItems.SingleOrDefault(i => i.ID == menuItem.ID);
        }
        /// <summary>
        /// Get menu by id
        /// </summary>
        /// <param name="id"></param>
        /// <exception cref="ArgumentException"></exception>
        /// <returns></returns>
        public static Menu GetMenuByID(int id)
        {
            var menu = DB.Menus.SingleOrDefault(m => m.ID == id);
            if (menu == null)
            {
                throw new ArgumentException("Invalid menu id");
            }
            return menu;
        }

        /// <summary>
        /// Get category by ID
        /// </summary>
        /// <param name="id"></param>
        /// <param name="categories"></param>
        /// <exception cref="ArgumentException"></exception>
        /// <returns></returns>
        public static Category GetCategoryByID(int id)
        {
            var category = DB.Categories.SingleOrDefault(c => c.ID == id);
            if (category == null)
            {
                throw new ArgumentException("Invalid category id!");
            }
            return category;
        }
        /// <summary>
        /// Get menu item by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="menuItems"></param>
        /// <exception cref="ArgumentException"></exception>
        /// <returns></returns>
        public static MenuItem GetMenuItemByID(int id)
        {
            var menuItem = DB.MenuItems.SingleOrDefault(i => i.ID == id);
            if (menuItem == null)
            {
                throw new ArgumentException("Invalid menu item id!");
            }
            return menuItem;
        }

        public static List<Menu> GetAllMenus()
        {
            return DB.Menus.ToList();
        }

        public static List<Category> GetAllCategory()
        {
            return DB.Categories.ToList();
        }

        public static List<MenuItem> GetAllMenuItems()
        {
            return DB.MenuItems.ToList();
        }

        public static List<Category> GetAllCategoriesByMenu(Menu menu)
        {
            return DB.Categories.Where(c => c.Menu == menu).ToList();
        }

        public static List<MenuItem> GetAllMenuItemsByCategoryInMenu(Category category, Menu menu)
        {
            return DB.MenuItems.Where(i => i.Category == category && i.Menu == menu).ToList();
        }
    }
}

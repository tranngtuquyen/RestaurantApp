using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RestaurantApp
{
    public class MenuManager
    {
        public readonly RestaurantContext _context;
                
        public MenuManager(RestaurantContext context)
        {
            this._context = context;
        }

        public Menu CreateMenu(string name, string userID)
        {
            if (userID == null)
            {
                throw new ArgumentNullException();
            }

            var menuIsExisted = _context.Menus.SingleOrDefault(m => m.UserID == userID && m.Name == name);
            if (menuIsExisted != null)
            {
                throw new ArgumentException();
            }
            
            var menu = new Menu
            {
                Name = name,
                UserID = userID
            };

            _context.Menus.Add(menu);
            _context.SaveChanges();
            return _context.Menus.SingleOrDefault(m => m.ID == menu.ID);
        }

        public Category CreateCategory(string categoryName, Menu menu, string userID)
        {
            var checkNull = _context.Categories.SingleOrDefault(c => c.Name == categoryName && c.Menu == menu && c.UserID == userID);
            
            if (checkNull != null)
            {
                throw new ArgumentException($"{categoryName} has been used! Try again.");
            }

            var category = new Category
            {
                Name = categoryName,
                Menu = menu,
                MenuID = menu.ID,
                UserID = userID
            };

            _context.Categories.Add(category);
            _context.SaveChanges();
            return _context.Categories.SingleOrDefault(c => c.ID == category.ID);
        }

        public MenuItem CreateMenuItem(string itemName, string itemDescription, decimal price, Category category, Menu menu, string userID)
        {
            if (price < 0 )
            {
                throw new ArgumentOutOfRangeException("price", "Menu item price cannot be negative!");
            }

            var checkNull = _context.MenuItems.SingleOrDefault(i => i.Name == itemName && i.Category == category && i.Menu == menu && i.UserID == userID);

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
                MenuID = menu.ID,
                UserID = userID
            };
            
            _context.MenuItems.Add(menuItem);
            _context.SaveChanges();
            return _context.MenuItems.SingleOrDefault(i => i.ID == menuItem.ID);
        }
        /// <summary>
        /// Get menu by id
        /// </summary>
        /// <param name="id"></param>
        /// <exception cref="ArgumentException"></exception>
        /// <returns></returns>
        public Menu GetMenuByID(int id)
        {
            var menu = _context.Menus.SingleOrDefault(m => m.ID == id);
            
            return menu;
        }

        /// <summary>
        /// Get category by ID
        /// </summary>
        /// <param name="id"></param>
        /// <param name="categories"></param>
        /// <exception cref="ArgumentException"></exception>
        /// <returns></returns>
        public Category GetCategoryByID(int id)
        {
            var category = _context.Categories.SingleOrDefault(c => c.ID == id);
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
        public MenuItem GetMenuItemByID(int id)
        {
            var menuItem = _context.MenuItems.SingleOrDefault(i => i.ID == id);
            if (menuItem == null)
            {
                throw new ArgumentException("Invalid menu item id!");
            }
            return menuItem;
        }

        public List<Menu> GetAllMenus(string userID)
        {
            if (string.IsNullOrEmpty(userID))
            {
                throw new ArgumentNullException(nameof(userID));
            }
            var listMenus = _context.Menus.Where(m => m.UserID == userID).OrderBy(m => m.Name).ToList();
            return listMenus;
        }

        public List<Category> GetAllCategory()
        {
            return _context.Categories.ToList();
        }

        public List<MenuItem> GetAllMenuItems(string userID)
        {
            return _context.MenuItems.Where(i => i.UserID == userID).ToList();
        }

        public List<Category> GetAllCategoriesByMenu(Menu menu)
        {
            return _context.Categories.Where(c => c.Menu == menu).OrderBy(c => c.Name).ToList();
        }

        public List<MenuItem> GetAllMenuItemsByCategoryInMenu(Category category, Menu menu)
        {
            return _context.MenuItems.Where(i => i.Category == category && i.Menu == menu).OrderBy(i => i.Name).ToList();
        }

        public List<MenuItem> GetAllMenuItemsByMenu(Menu menu)
        {
            return _context.MenuItems.Where(i => i.Menu == menu).OrderBy(i => i.Name).ToList();
        }

        public void MenuUpdates(Menu editMenu)
        {
            var oldMenu = _context.Menus.SingleOrDefault(m => m.ID == editMenu.ID);
            oldMenu.Name = editMenu.Name;
            _context.SaveChanges();
        }

        public MenuItem MenuItemUpdates(MenuItem menuItem)
        {
            var oldMenuItem = _context.MenuItems.SingleOrDefault(i => i.ID == menuItem.ID);
            oldMenuItem.Name = menuItem.Name;
            oldMenuItem.Description = menuItem.Description;
            var price = menuItem.Price;
            if (price < 0)
            {
                throw new ArgumentOutOfRangeException("Price cannot be negative");
            }
            oldMenuItem.Price = menuItem.Price;
            _context.SaveChanges();
            return oldMenuItem;
        }

        public Category CategoryUpdates(Category category)
        {
            var oldCategory = _context.Categories.SingleOrDefault(c => c.ID == category.ID);
            oldCategory.Name = category.Name;
            _context.SaveChanges();
            return oldCategory;
        }
    }
    
}

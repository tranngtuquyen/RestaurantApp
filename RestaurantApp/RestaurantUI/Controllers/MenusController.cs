using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RestaurantApp;
using RestaurantUI.Models;

namespace RestaurantUI.Controllers
{
    [Authorize]
    public class MenusController : Controller
    {
        private readonly MenuManager _menuManager;
        public string UserID;
        public MenusController(MenuManager menuManager)
        {
            _menuManager = menuManager;
        }

        // GET: Menus
        public IActionResult Index()
        {
            if (HttpContext != null && !string.IsNullOrEmpty(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)))
            {
                UserID = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            }
            
            return View(_menuManager.GetAllMenus(UserID));
        }

        // GET: Menus/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var menu = _menuManager.GetMenuByID(id.Value);
            if (menu == null)
            {
                return NotFound();
            }
            MenuDetailsViewModel viewModel = new MenuDetailsViewModel();
            viewModel.Menu = menu;
            viewModel.Categories = _menuManager.GetAllCategoriesByMenu(menu);
            viewModel.MenuItems = _menuManager.GetAllMenuItemsByMenu(menu);

            return View(viewModel);
        }

        // GET: Menus/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Menus/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("ID,Name")] Menu menu)
        {
            try
            {
                if (HttpContext != null && string.IsNullOrEmpty(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)))
                {
                    UserID = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
                }

                if (string.IsNullOrEmpty(UserID))
                {
                    return Unauthorized();
                }

                _menuManager.CreateMenu(menu.Name, UserID);
                return RedirectToAction(nameof(Index));
            }
            catch (ArgumentException)
            {
                ViewBag.ErrorMessage = "Menu name has been used";
                return View();
            }
        }

        // GET: Menus/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var menu = _menuManager.GetMenuByID(id.Value);
            if (menu == null)
            {
                return NotFound();
            }
            return View(menu);
        }

        // POST: Menus/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("ID,Name")] Menu menu)
        {
            if (id != menu.ID)
            {
                return NotFound();
            }

            _menuManager.MenuUpdates(menu);
            return RedirectToAction(nameof(Index));
        }
        //GET
        public IActionResult EditMenuItem(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var menuItem = _menuManager.GetMenuItemByID(id.Value);
            if (menuItem == null)
            {
                return NotFound();
            }
            return View(menuItem);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditMenuItem(int id, [Bind("ID,Name,Description,Price")] MenuItem menuItem)
        {
            if (id != menuItem.ID)
            {
                return NotFound();
            }
            try
            {
                var updatedMenuItem = _menuManager.MenuItemUpdates(menuItem);

                int menuID = updatedMenuItem.MenuID;
                return RedirectToAction(nameof(Details), new { id = menuID });
            }
            catch (ArgumentOutOfRangeException)
            {
                ViewBag.ErrorMessage = "Price cannot be negative";
                var newMenuItem = _menuManager.GetMenuItemByID(menuItem.ID);
                
                return View(newMenuItem);
            }
        }

        //GET
        public IActionResult EditCategory(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var category = _menuManager.GetCategoryByID(id.Value);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditCategory(int id, [Bind("ID,Name")] Category category)
        {
            if (id != category.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var updateCategory = _menuManager.CategoryUpdates(category);
                int menuID = updateCategory.MenuID;
                return RedirectToAction(nameof(Details), new { id = menuID });
            }
            return View(category);
        }

        // GET
        public IActionResult CreateCategory(int? menuID)
        {
            if (menuID == null)
            {
                return NotFound();
            }
            var viewModel = new MenuDetailsViewModel();
            viewModel.Menu = _menuManager.GetMenuByID(menuID.Value);
            return View(viewModel);
        }

        // POST: Menus/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateCategory(MenuDetailsViewModel viewModel)
        {
            var userID = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var menu = _menuManager.GetMenuByID(viewModel.Menu.ID);
            var categoryName = viewModel.Category.Name;
            _menuManager.CreateCategory(categoryName, menu, userID);
            return RedirectToAction(nameof(Details), new { id = menu.ID });
        }

        // GET
        public IActionResult CreateMenuItem(int? menuID, int? categoryID)
        {
            if (menuID == null || categoryID == null)
            {
                return NotFound();
            }
            var viewModel = new MenuDetailsViewModel();
            viewModel.Menu = _menuManager.GetMenuByID(menuID.Value);
            viewModel.Category = _menuManager.GetCategoryByID(categoryID.Value);
            return View(viewModel);
        }

        // POST: Menus/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateMenuItem(MenuDetailsViewModel viewModel)
        {
            var userID = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var menu = _menuManager.GetMenuByID(viewModel.Menu.ID);
            var category = _menuManager.GetCategoryByID(viewModel.Category.ID);
            try
            {
                var itemName = viewModel.MenuItem.Name;
                var itemDescription = viewModel.MenuItem.Description;
                var itemPrice = viewModel.MenuItem.Price;
                _menuManager.CreateMenuItem(itemName, itemDescription, itemPrice, category, menu, userID);
                return RedirectToAction(nameof(Details), new { id = menu.ID });
            }
            catch (ArgumentOutOfRangeException)
            {
                ViewBag.ErrorMessage = "Price cannot negative";
                var newViewModel = new MenuDetailsViewModel();
                newViewModel.Menu = menu;
                newViewModel.Category = category;
                return View(newViewModel);
            }
        }
    }
}

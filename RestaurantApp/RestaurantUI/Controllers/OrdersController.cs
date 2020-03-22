using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RestaurantApp;
using RestaurantUI.Models;

namespace RestaurantUI.Controllers
{
    [Authorize]
    public class OrdersController : Controller
    {
        private readonly OrderManager _orderManager;
        private readonly MenuManager _menuManager;

        public OrdersController(OrderManager orderManager, MenuManager menuManager)
        {
            _orderManager = orderManager;
            _menuManager = menuManager;
        }

        // GET: Orders
        public IActionResult Index()
        {
            var userID = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var orders = _orderManager.GetAllOrders(userID);
            foreach (var order in orders)
            {
                _orderManager.CalculateTotalOrderPrice(order);
            }
            return View(orders);
        }

        // GET: Orders/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = _orderManager.GetOrderByID(id.Value);
            if (order == null)
            {
                return NotFound();
            }
            _orderManager.CalculateTotalOrderPrice(order);
            var viewModel = new OrderDetailsViewModel();
            viewModel.Order = order;
            viewModel.OrderItems = _orderManager.GetAllOrderItemsInOrder(order);
            var userID = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            viewModel.MenuItems = _menuManager.GetAllMenuItems(userID);
            return View(viewModel);
        }


        // GET: Orders/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("TableNumber,CustomerNote")] Order order)
        {
            if (ModelState.IsValid)
            {
                var userID = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
                _orderManager.CreateOrder(order.TableNumber, userID, order.CustomerNote);
                return RedirectToAction(nameof(Index));
            }
            return View(order);
        }

        // GET: Orders/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = _orderManager.GetOrderByID(id.Value);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("ID,TableNumber,CustomerNote,Status")] Order order)
        {
            if (id != order.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var updatedOrder = _orderManager.OrderUpdates(order);
                return RedirectToAction(nameof(Index));
            }
            return View(order);
        }

        // GET
        public IActionResult CreateOrderItemStep1(int? orderID)
        {
            if (orderID == null)
            {
                return NotFound();
            }
            var order = _orderManager.GetOrderByID(orderID.Value);
            if (order == null)
            {
                return NotFound();
            }
            var viewModel = new OrderDetailsViewModel();
            viewModel.Order = order;
            var userID = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var menu = _menuManager.GetAllMenus(userID);
            var selectListMenu = menu.Select(m => new SelectListItem(m.Name, m.ID.ToString()));
            viewModel.SelectMenu = selectListMenu;
            return View(viewModel);
        }

        // POST: Orders/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateOrderItemStep1(OrderDetailsViewModel viewModel)
        {
            var mID = viewModel.MenuID;
            var oID = viewModel.Order.ID;
            return RedirectToAction(nameof(CreateOrderItemStep2), new { menuID = mID, orderID = oID });

        }

        // GET
        public IActionResult CreateOrderItemStep2(int? menuID, int? orderID)
        {
            if (orderID == null || menuID == null)
            {
                return NotFound();
            }
            var order = _orderManager.GetOrderByID(orderID.Value);
            if (order == null)
            {
                return NotFound();
            }
            var viewModel = new OrderDetailsViewModel();
            viewModel.Order = order;
            viewModel.MenuID = menuID.Value;
            var menu = _menuManager.GetMenuByID(menuID.Value);
            var menuItems = _menuManager.GetAllMenuItemsByMenu(menu);
            var selectListMenuItems = menuItems.Select(p => new SelectListItem(p.Name, p.ID.ToString()));
            viewModel.SelectMenuItems = selectListMenuItems;
            return View(viewModel);
        }

        // POST: Orders/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateOrderItemStep2(OrderDetailsViewModel viewModel)
        {   
            var menuItemID = viewModel.OrderItem.MenuItemID;
            var menuItem = _menuManager.GetMenuItemByID(menuItemID);
            var orderID = viewModel.Order.ID;
            var order = _orderManager.GetOrderByID(orderID);
            var userID = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            
            try
            {
                _orderManager.CreateOrderItem(menuItem, order, viewModel.OrderItem.Quantity, userID);
                _orderManager.CalculateTotalOrderPrice(order);
                return RedirectToAction(nameof(Details), new { id = orderID });
            }
            catch (ArgumentOutOfRangeException)
            {
                ViewBag.ErrorMessage = "Quantity cannot be negative";
                var newViewModel = new OrderDetailsViewModel();
                newViewModel.Order = order;
                var menuID = viewModel.MenuID;
                viewModel.MenuID = menuID;
                var menu = _menuManager.GetMenuByID(menuID);
                var menuItems = _menuManager.GetAllMenuItemsByMenu(menu);
                var selectListMenuItems = menuItems.Select(p => new SelectListItem(p.Name, p.ID.ToString()));
                newViewModel.SelectMenuItems = selectListMenuItems;
                return View(newViewModel);
            }
        }

        // GET:
        public IActionResult EditOrderItem(int? itemID)
        {
            if (itemID == null)
            {
                return NotFound();
            }

            var orderItem = _orderManager.GetOrderItemByID(itemID.Value);
            if (orderItem == null)
            {
                return NotFound();
            }

            return View(orderItem);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditOrderItem(int id, [Bind("ID,Quantity,Status")] OrderItem orderItem)
        {
            if (id != orderItem.ID)
            {
                return NotFound();
            }
            try
            {
                var updatedOrderItem = _orderManager.OrderItemUpdate(orderItem);
                var orderID = updatedOrderItem.OrderID;
                return RedirectToAction(nameof(Details), new { id = orderID });
            }
            catch(ArgumentOutOfRangeException)
            {
                ViewBag.ErrorMessage = "Quantity cannot be negative";
                var newOrderItem = _orderManager.GetOrderItemByID(orderItem.ID);
                return View(orderItem);
            }
        }
    }
}

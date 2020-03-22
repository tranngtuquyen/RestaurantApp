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
    public class ReceiptsController : Controller
    {
        private readonly ReceiptManager _receiptManager;
        private readonly OrderManager _orderManager;
        private readonly RestaurantManager _restaurantManager;

        public ReceiptsController(ReceiptManager receiptManager, OrderManager orderManager, RestaurantManager restaurantManager)
        {
            _receiptManager = receiptManager;
            _orderManager = orderManager;
            _restaurantManager = restaurantManager;
        }

        // GET: Receipts
        public IActionResult Index()
        {
            var viewModel = new ReceiptDetailsViewModel();
            var userID = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            viewModel.Receipts = _receiptManager.GetAllReceipts(userID);
            viewModel.Orders = _orderManager.GetAllOrders(userID);
            return View(viewModel);
        }

        // GET: Receipts/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var receipt = _receiptManager.GetReceiptByID(id.Value);
            if (receipt == null)
            {
                return NotFound();
            }
            var orderID = receipt.OrderID;
            var order = _orderManager.GetOrderByID(orderID);

            var viewModel = new ReceiptDetailsViewModel();
            viewModel.Receipt = receipt;
            viewModel.Order = order;

            return View(viewModel);
        }

        // GET: Receipts/Create
        public IActionResult Create()
        {
            var viewModel = new ReceiptDetailsViewModel();
            var userID = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            viewModel.Orders = _orderManager.GetOrdersPendingReceipts(userID);

            return View(viewModel);
        }

        // GET: Receipts/Create
        public IActionResult QuickCreate(int orderID)
        {
            var viewModel = new ReceiptDetailsViewModel();
            viewModel.Order = _orderManager.GetOrderByID(orderID);
            viewModel.OrderID = orderID;
            return View(viewModel);
        }

        // POST: Receipts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ReceiptDetailsViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var userID = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
                var restaurant = _restaurantManager.GetRestaurantByUserID(userID);
                var tax = restaurant.Tax;
                var orderID = viewModel.OrderID;
                var order = _orderManager.GetOrderByID(orderID);
                var receipt = _receiptManager.CreateReceipt(order, userID, tax);
                return RedirectToAction(nameof(Index));
            }
            return View(viewModel);
        }

        // GET: Receipts/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var receipt = _receiptManager.GetReceiptByID(id.Value);
            if (receipt == null)
            {
                return NotFound();
            }

            return View(receipt);
        }

        // POST: Receipts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("ID,Status")] Receipt receipt)
        {
            if (id != receipt.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var updateReceipt = _receiptManager.UpdateReceipt(receipt);
                var receiptStatus = updateReceipt.Status;
                var orderID = updateReceipt.OrderID;
                var order = _orderManager.GetOrderByID(orderID);
                if (receiptStatus == ReceiptStatus.Completed)
                {
                    order.Status = OrderStatus.Completed;
                }
                else if (receiptStatus == ReceiptStatus.Cancelled)
                {
                    order.Status = OrderStatus.ReadyToBill;
                }
                _orderManager.OrderUpdates(order);
                return RedirectToAction(nameof(Index));
            }
            return View(receipt);
        }
    }
}

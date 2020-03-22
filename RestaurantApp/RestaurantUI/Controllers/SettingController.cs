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

namespace RestaurantUI.Controllers
{
    [Authorize]
    public class SettingController : Controller
    {
        private readonly RestaurantManager _restaurantManager;

        public SettingController(RestaurantManager restaurantManager )
        {
            _restaurantManager = restaurantManager;
        }

        // GET: Restaurants
        public IActionResult Index()
        {
            var userID = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var restaurant = _restaurantManager.GetRestaurantByUserID(userID);
            if (restaurant == null)
            {
                return RedirectToAction(nameof(Initialize));
            } 

            return View(restaurant);
        }
        public IActionResult Initialize()
        {
            return View();
        }

        // GET: Restaurants/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Restaurants/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("ID,Name,Address,EmailAddress,PhoneNumber,Tax")] 
        Restaurant restaurant)
        {
            try
            {
                var userID = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
                var name = restaurant.Name;
                var address = restaurant.Address;
                var email = restaurant.EmailAddress;
                var phone = restaurant.PhoneNumber;
                var tax = restaurant.Tax;
                _restaurantManager.CreateRestaurant(name, address, email, phone, tax, userID);
                return RedirectToAction(nameof(Index));
            }
            catch (ArgumentOutOfRangeException)
            {
                ViewBag.ErrorMessage = "Tax cannot be negative";
                return View();
            }
        }

        // GET: Restaurants/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var restaurant = _restaurantManager.GetRestaurantByID(id.Value);
            if (restaurant == null)
            {
                return NotFound();
            }
            return View(restaurant);
        }

        // POST: Restaurants/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, 
            [Bind("ID,Name,Address,EmailAddress,PhoneNumber,Tax")] Restaurant restaurant)
        {
            if (id != restaurant.ID)
            {
                return NotFound();
            }
            try
            {
                _restaurantManager.RestaurantUpdate(restaurant);
                return RedirectToAction(nameof(Index));
            }
            catch (ArgumentOutOfRangeException)
            {
                ViewBag.ErrorMessage = "Tax cannot be negative";
                var newRestaurant = _restaurantManager.GetRestaurantByID(restaurant.ID);
                return View(newRestaurant);
            }
        }
    }
}

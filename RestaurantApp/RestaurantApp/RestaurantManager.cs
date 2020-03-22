using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RestaurantApp
{
    public class RestaurantManager
    {
        private readonly RestaurantContext _context;
        public RestaurantManager(RestaurantContext context)
        {
            _context = context;
        }
        public Restaurant CreateRestaurant(string name, string address, string email, string phoneNumber, decimal tax, string userID)
        {
            if (tax < 0)
            {
                throw new ArgumentOutOfRangeException();
            }
            var restaurant = new Restaurant
            {
                Name = name,
                Address = address,
                EmailAddress = email,
                PhoneNumber = phoneNumber,
                Tax = tax,
                UserID = userID
            };
            _context.Restaurant.Add(restaurant);
            _context.SaveChanges();
            return _context.Restaurant.SingleOrDefault(r => r.ID == restaurant.ID);
        }
        public Restaurant GetRestaurantByUserID(string userID)
        {
            return _context.Restaurant.SingleOrDefault(r => r.UserID == userID);
        }
        public Restaurant GetRestaurantByID(int id)
        {
            return _context.Restaurant.SingleOrDefault(r => r.ID == id);
        }

        public Restaurant RestaurantUpdate(Restaurant newRestaurant)
        {
            var oldRestaurant = _context.Restaurant.SingleOrDefault(r => r.ID == newRestaurant.ID);
            oldRestaurant.Name = newRestaurant.Name;
            oldRestaurant.Address = newRestaurant.Address;
            oldRestaurant.EmailAddress = newRestaurant.EmailAddress;
            oldRestaurant.PhoneNumber = newRestaurant.PhoneNumber;
            var tax = newRestaurant.Tax;
            if (tax < 0)
            {
                throw new ArgumentOutOfRangeException();
            }
            oldRestaurant.Tax = tax;
            _context.SaveChanges();
            return oldRestaurant;
        }

    }
}

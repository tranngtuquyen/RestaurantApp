using Microsoft.AspNetCore.Mvc.Rendering;
using RestaurantApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantUI.Models
{
    public class OrderDetailsViewModel
    {
        public Order Order { get; set; }
        public IEnumerable<OrderItem> OrderItems { get; set; }
        public OrderItem OrderItem { get; set; }
        public IEnumerable<SelectListItem> SelectMenuItems { get; set; }
        public IEnumerable<SelectListItem> SelectMenu { get; set; }
        public IEnumerable<MenuItem> MenuItems { get; set; }
        public int MenuID { get; set; }
    }
}

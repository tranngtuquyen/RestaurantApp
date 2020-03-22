using RestaurantApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantUI.Models
{
    public class MenuDetailsViewModel
    {
        public Menu Menu { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<MenuItem> MenuItems { get; set; }
        public Category Category { get; set; }
        public MenuItem MenuItem { get; set; }
    }
}

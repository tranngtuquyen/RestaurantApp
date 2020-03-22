using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RestaurantApp
{
    public class Menu
    {
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }
        public string UserID { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RestaurantApp
{
    public class Restaurant
    {
        public int ID { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Address { get; set; }

        [Required][DataType(DataType.EmailAddress)]
        public string EmailAddress { get; set; }

        [Required][DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        [Required]
        public decimal Tax { get; set; }
        public string UserID { get; set; }
    }
}

using RestaurantApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantUI.Models
{
    public class ReceiptDetailsViewModel
    {
        public Order Order { get; set; }
        public IEnumerable<Order> Orders { get; set; }
        public IEnumerable<Receipt> Receipts { get; set; }
        public Receipt Receipt {get; set; }
        public int OrderID { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RestaurantApp
{
    static class ReceiptManager
    {
        public static RestaurantContext DB;
        /// <summary>
        /// Create receipt for an order
        /// </summary>
        /// <param name="order">Order</param>
        /// <returns>Newly created receipt</returns>
        public static Receipt CreateReceipt(Order order)
        {
            var receipt = new Receipt
            {
                Order = order,
                OrderID = order.ID,
                Status = ReceiptStatus.Pending,
                SubTotal = order.Price,
                Tax = 0.1M,
            };
            
            receipt.Total = receipt.SubTotal * (1 + receipt.Tax);

            DB.Receipts.Add(receipt);
            DB.SaveChanges();

            return DB.Receipts.SingleOrDefault(r => r.ID == receipt.ID);
        }
    }
}

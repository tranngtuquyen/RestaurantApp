using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantApp
{
    static class ReceiptManager
    {
        /// <summary>
        /// Create receipt for an order
        /// </summary>
        /// <param name="order">Order</param>
        /// <returns>Newly created receipt</returns>
        public static Receipt CreateReceipt(Order order)
        {
            //Pending: Receipt only be created if order status is New or InProgress
            var receipt = new Receipt
            {
                Order = order,
                Status = ReceiptStatus.Pending,
                SubTotal = order.Price,
                Tax = 0.1M,
            };

            var restaurant = new Restaurant
            {
                Name = "Sushi Samurai",
                Address = "100 1st Seattle",
                PhoneNumber = "425 000 0000"
            };

            receipt.Restaurant = restaurant;

            receipt.Total = receipt.SubTotal * (1 + receipt.Tax);

            return receipt;
        }
    }
}

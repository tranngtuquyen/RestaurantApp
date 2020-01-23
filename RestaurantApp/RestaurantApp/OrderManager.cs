using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantApp
{
    static class OrderManager
    {
        /// <summary>
        /// Create order for a table
        /// </summary>
        /// <param name="tableNumber">Table Number</param>
        /// <param name="customerNote">Customer Note</param>
        /// <returns>Newly created order</returns>
        public static Order CreateOrder(string tableNumber, string customerNote = "None")
        {
            var order = new Order
            {
                TableNumber = tableNumber,
                Status = OrderStatus.New,
                CustomerNote = customerNote,
            };
            return order;
        }

        /// <summary>
        /// Create order item for an order
        /// </summary>
        /// <param name="menuItem">Menu Item</param>
        /// <param name="quantity">Quantity</param>
        /// <returns>Newly created order item</returns>
        public static OrderItem CreateOrderItem(MenuItem menuItem, int quantity = 1)
        {
            if (quantity < 0)
            {
                quantity = 0;
            }

            var orderItem = new OrderItem
            {
                Status = OrderItemStatus.Open,
                MenuItem = menuItem,
                Quantity = quantity,
                Price = menuItem.Price * quantity
            };

            return orderItem;
        }
    }
}

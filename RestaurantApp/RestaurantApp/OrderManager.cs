using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RestaurantApp
{
    static class OrderManager
    {
        public static RestaurantContext DB;
        
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
                Price = 0
            };

            DB.Orders.Add(order);
            DB.SaveChanges();

            return DB.Orders.SingleOrDefault(o => o.ID == order.ID);
        }

        /// <summary>
        /// Create order item for an order
        /// </summary>
        /// <param name="menuItem">Menu Item</param>
        /// <param name="quantity">Quantity</param>
        /// <returns>Newly created order item</returns>
        public static OrderItem CreateOrderItem(MenuItem menuItem, Order order, int quantity)
        {
            if (quantity < 0)
            {
                throw new ArgumentOutOfRangeException("quantity", "Quantity cannot be negative");
            }

            var orderItem = new OrderItem
            {
                Status = OrderItemStatus.Open,
                Quantity = quantity,
                MenuItem = menuItem,
                Price = menuItem.Price * quantity,
                MenuItemID = menuItem.ID,
                Order = order,
                OrderID = order.ID
            };

            DB.OrderItems.Add(orderItem);
            DB.SaveChanges();

            return DB.OrderItems.SingleOrDefault(p => p.ID == orderItem.ID);
        }

        public static void CalculateTotalOrderPrice(Order order)
        {
            var orderItems = DB.OrderItems.Where(i => i.Order == order).ToList();
            order.Price = 0;
            foreach (var item in orderItems)
            {
                order.Price += item.Price;
            }
            DB.SaveChanges();
        }

        /// <summary>
        /// Get pending order by table number
        /// </summary>
        /// <param name="tableNumber"></param>
        /// <exception cref="ArgumentException"></exception>
        /// <returns></returns>
        public static Order GetPendingOrderByTableNumber(string tableNumber)
        {
            var order = DB.Orders.SingleOrDefault(o => o.TableNumber == tableNumber && o.Status != OrderStatus.Cancelled && o.Status != OrderStatus.Completed);
            if (order == null)
            {
                throw new ArgumentException("Invalid table number!");
            }
            return order;
        }

        /// <summary>
        /// Get order item by id
        /// </summary>
        /// <param name="order"></param>
        /// <param name="id"></param>
        /// <exception cref="ArgumentException"></exception>
        /// <returns></returns>
        public static OrderItem GetOrderItemByID(int id)
        {
            var orderItem = DB.OrderItems.SingleOrDefault(i => i.ID == id);
            if (orderItem == null)
            {
                throw new ArgumentException("Invalid order item id!");
            }
            return orderItem;
        }

        public static List<OrderItem> GetAllOrderItemsInOrder(Order order)
        {
            var orderItems = DB.OrderItems.Where(i => i.Order == order).ToList();
            return orderItems;
        }

        public static void SaveChangesOnOrder(Order editedOrder)
        {
            var order = DB.Orders.SingleOrDefault(o => o.ID == editedOrder.ID);
            order = editedOrder;
            DB.SaveChanges();
        }

        public static void SaveChangesOnOrderItem(OrderItem editedOrderItem)
        {
            var orderItem = DB.OrderItems.SingleOrDefault(i => i.ID == editedOrderItem.ID);
            orderItem = editedOrderItem;
            DB.SaveChanges();
        }

        public static List<Order> GetOrdersPendingReceipts()
        {
            var orders = DB.Orders.Where(o => o.Status == OrderStatus.New || o.Status == OrderStatus.InProgress || o.Status == OrderStatus.ReadyToBill).ToList();
            return orders;
        }
    }
}

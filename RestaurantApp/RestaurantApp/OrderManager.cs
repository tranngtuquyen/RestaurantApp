using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RestaurantApp
{
    public class OrderManager
    {
        public RestaurantContext _context;
        public OrderManager(RestaurantContext context)
        {
            _context = context;
        }
        
        /// <summary>
        /// Create order for a table
        /// </summary>
        /// <param name="tableNumber">Table Number</param>
        /// <param name="customerNote">Customer Note</param>
        /// <returns>Newly created order</returns>
        public Order CreateOrder(string tableNumber, string userID, string customerNote = "None")
        {
            var order = new Order
            {
                TableNumber = tableNumber,
                Status = OrderStatus.New,
                CustomerNote = customerNote,
                Price = 0,
                UserID = userID
            };

            _context.Orders.Add(order);
            _context.SaveChanges();

            return _context.Orders.SingleOrDefault(o => o.ID == order.ID);
        }

        /// <summary>
        /// Create order item for an order
        /// </summary>
        /// <param name="menuItem">Menu Item</param>
        /// <param name="quantity">Quantity</param>
        /// <returns>Newly created order item</returns>
        public OrderItem CreateOrderItem(MenuItem menuItem, Order order, int quantity, string userID)
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
                OrderID = order.ID,
                UserID = userID
            };

            _context.OrderItems.Add(orderItem);
            _context.SaveChanges();

            return _context.OrderItems.SingleOrDefault(p => p.ID == orderItem.ID);
        }

        public void CalculateTotalOrderPrice(Order order)
        {
            var orderItems = _context.OrderItems.Where(i => i.Order == order).ToList();
            order.Price = 0;
            foreach (var item in orderItems)
            {
                CalculateOrderItemPrice(item);
                order.Price += item.Price;
            }
            _context.SaveChanges();
        }

        /// <summary>
        /// Get pending order by table number
        /// </summary>
        /// <param name="tableNumber"></param>
        /// <exception cref="ArgumentException"></exception>
        /// <returns></returns>
        public Order GetPendingOrderByTableNumber(string tableNumber)
        {
            var order = _context.Orders.SingleOrDefault(o => o.TableNumber == tableNumber && o.Status != OrderStatus.Cancelled && o.Status != OrderStatus.Completed);
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
        public OrderItem GetOrderItemByID(int id)
        {
            var orderItem = _context.OrderItems.SingleOrDefault(i => i.ID == id);
            if (orderItem == null)
            {
                throw new ArgumentException("Invalid order item id!");
            }
            return orderItem;
        }

        public List<OrderItem> GetAllOrderItemsInOrder(Order order)
        {
            var orderItems = _context.OrderItems.Where(i => i.Order == order).ToList();
            return orderItems;
        }

        public Order OrderUpdates(Order editedOrder)
        {
            var order = _context.Orders.SingleOrDefault(o => o.ID == editedOrder.ID);
            order.TableNumber = editedOrder.TableNumber;
            order.Status = editedOrder.Status;
            order.CustomerNote = editedOrder.CustomerNote;
            _context.SaveChanges();
            return order;
        }

        public OrderItem OrderItemUpdate(OrderItem editedOrderItem)
        {
            var orderItem = _context.OrderItems.SingleOrDefault(i => i.ID == editedOrderItem.ID);
            orderItem.Status = editedOrderItem.Status;
            var quantity = editedOrderItem.Quantity;
            if (quantity < 0)
            {
                throw new ArgumentOutOfRangeException("Quantity cannot be negative");
            }
            orderItem.Quantity = quantity;
            CalculateOrderItemPrice(orderItem);
            return orderItem;
        }

        public void CalculateOrderItemPrice(OrderItem orderItem)
        {
            var menuItemID = orderItem.MenuItemID;
            var menuItem = _context.MenuItems.SingleOrDefault(i => i.ID == menuItemID);
            orderItem.Price = menuItem.Price * orderItem.Quantity;
            _context.SaveChanges();
        }

        public List<Order> GetOrdersPendingReceipts(string userID)
        {
            var orders = _context.Orders.Where(o => o.UserID == userID)
                .Where(o => o.Status == OrderStatus.New || o.Status == OrderStatus.InProgress || o.Status == OrderStatus.ReadyToBill)
                .OrderByDescending(o => o.CreatedDateTime).ToList();
            return orders;
        }
        public List<Order> GetAllOrders(string userID)
        {
            return _context.Orders.Where(o => o.UserID == userID).OrderByDescending(o => o.CreatedDateTime).ToList();
        }
        public Order GetOrderByID(int id)
        {
            return _context.Orders.SingleOrDefault(o => o.ID == id);
        }
    }
}

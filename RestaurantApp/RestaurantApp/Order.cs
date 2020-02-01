using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantApp
{
    enum OrderStatus
    {
        New,
        InProgress,
        Cancelled,
        Billed,
        Completed
    }
    /// <summary>
    /// Defines an order
    /// </summary>
    class Order
    {
        #region Properties
        private static int lastIDNumber = 0;
        public int ID { get; private set; }
        public string TableNumber { get; set; }
        public string CustomerNote { get; set; }
        public DateTime CreatedDateTime { get; private set; }
        public OrderStatus Status { get; set; }
        public decimal Price { get; set; } //Pending: Calculate total price of order items in an order
        public List<OrderItem> OrderItems { get; set; }
        #endregion

        #region Constructor
        public Order()
        {
            ID = ++lastIDNumber;
            CreatedDateTime = DateTime.UtcNow;
            OrderItems = new List<OrderItem>();
        }
        #endregion

        #region Methods
        public decimal GetOrderPrice()
        {
            foreach (var orderItem in OrderItems)
            {
                Price += orderItem.Price;
            }

            return Price;
        }
        #endregion
    }
}

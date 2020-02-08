using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantApp
{
    enum OrderStatus
    {
        New,
        InProgress,
        ReadyToBill,
        Completed,
        Cancelled
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
        public decimal Price { get; set; }
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
        public decimal CalculateOrderPrice()
        {
            Price = 0;

            foreach (var orderItem in OrderItems)
            {
                Price += orderItem.Price;
            }

            return Price;
        }
        #endregion
    }
}

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
        public int ID { get; set; }
        public string TableNumber { get; set; }
        public string CustomerNote { get; set; }
        public DateTime CreatedDateTime { get; private set; }
        public OrderStatus Status { get; set; }
        public decimal Price { get; set; }
        #endregion

        #region Constructor
        public Order()
        {
            CreatedDateTime = DateTime.UtcNow;
        }
        #endregion
    }
}

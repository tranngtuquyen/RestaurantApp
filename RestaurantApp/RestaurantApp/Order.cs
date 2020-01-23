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
        //Pending: list of order items
        #endregion

        #region Constructor
        public Order()
        {
            ID = ++lastIDNumber;
            CreatedDateTime = DateTime.UtcNow;
        }
        #endregion

        #region Methods

        #endregion
    }
}

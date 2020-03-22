using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RestaurantApp
{
    public enum OrderStatus
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
    public class Order
    {
        #region Properties
        public int ID { get; set; }
        [Required]
        public string TableNumber { get; set; }
        public string CustomerNote { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public OrderStatus Status { get; set; }

        [Required][DataType(DataType.CreditCard)]
        public decimal Price { get; set; }
        public string UserID { get; set; }
        #endregion

        #region Constructor
        public Order()
        {
            CreatedDateTime = DateTime.UtcNow;
        }
        #endregion
    }
}

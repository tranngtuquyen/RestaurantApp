using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RestaurantApp
{
    public enum OrderItemStatus
    {
        Open,
        InProgress,
        ReadyToServe,
        Served
    }
    /// <summary>
    /// Defines each item in an order
    /// </summary>
    public class OrderItem
    {
        #region Properties
        public int ID { get; set; }
        public MenuItem MenuItem { get; set; }
        public int MenuItemID { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required][DataType(DataType.Currency)]
        public decimal Price { get; set; }
        public OrderItemStatus Status { get; set; }

        public Order Order { get; set; }
        public int OrderID { get; set; }
        public string UserID { get; set; }
        #endregion

        #region Methods
        /// <summary>
        /// Changes quantity of order item
        /// </summary>
        /// <param name="quantity"></param>
        /// <returns></returns>
        public int ChangeQuantity(int quantity)
        {
            //Pending: quantity must be positive
            Quantity = quantity;
            return Quantity;
        }
        #endregion
    }
}

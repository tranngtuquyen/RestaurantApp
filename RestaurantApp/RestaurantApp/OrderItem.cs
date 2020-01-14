using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantApp
{
    /// <summary>
    /// Defines each item in an order
    /// </summary>
    class OrderItem
    {
        #region Properties
        public int ID { get; set; }
        public int Quantity { get; set; } //Pending: Quantity cannot be negative
        public decimal Price { get; set; }
        //Pending: declare Menu Item 
        //Pending: set OrderLine price equal to multiplication of Menu Item and Quantity
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

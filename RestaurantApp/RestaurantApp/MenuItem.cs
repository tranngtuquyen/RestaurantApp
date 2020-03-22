using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RestaurantApp
{
    /// <summary>
    /// Defines each item in the menu
    /// </summary>
    public class MenuItem
    {
        #region Properties
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required][DataType(DataType.Currency)]
        public decimal Price { get; set; }
       
        public Category Category { get; set; }
        public int CategoryID { get; set; }
        public Menu Menu { get; set; }
        public int MenuID { get; set; }
        public string UserID { get; set; }
        // Pending: image

        #endregion

        #region Methods
        /// <summary>
        /// Changes price of menu item
        /// </summary>
        /// <param name="price">New price of menu item</param>
        /// <returns></returns>
        public decimal ChangePrice(decimal price)
        {
            // Pending: price cannot be negative
            Price = price;
            return Price;
        }

        /// <summary>
        /// Changes price of menu item by a certain percentage
        /// </summary>
        /// <param name="percent"></param>
        /// <returns></returns>
        public decimal ChangePriceByPercent(decimal percent)
        {
            // Pending: percent cannot be smaller than -100
            Price *= (1 + percent / 100);
            return Price;
        }
        #endregion
    }
}

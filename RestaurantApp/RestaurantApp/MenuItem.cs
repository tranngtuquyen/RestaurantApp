using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantApp
{
    /// <summary>
    /// Defines each item in the menu
    /// </summary>
    class MenuItem
    {
        #region Properties
        public int ID { get; private set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
       
        public Category Category { get; set; }
        public int CategoryID { get; set; }
        public Menu Menu { get; set; }
        public int MenuID { get; set; }
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

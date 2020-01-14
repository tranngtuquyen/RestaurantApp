using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantApp
{
    /// <summary>
    /// Defines an order
    /// </summary>
    class Order
    {
        #region Properties
        public int ID { get; set; }
        public string TableNumber { get; set; }
        public string CustomerNote { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public string Status { get; set; }
        //Pending: list of order lines
        #endregion

        #region Methods

        #endregion
    }
}

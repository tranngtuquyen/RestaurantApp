using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantApp
{
    /// <summary>
    /// Defines a receipt
    /// </summary>
    class Receipt
    {
        #region Properties
        public int ID { get; set; }
        public string Status { get; set; }
        /// <summary>
        /// Total cost before tax
        /// </summary>
        public decimal SubTotal { get; set; }        
        public decimal Tax { get; set; }
        /// <summary>
        /// Total cost after tax
        /// </summary>
        public decimal Total { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public string RestaurantName { get; set; }
        public string RestaurantAddress { get; set; }
        public string RestaurantPhone { get; set; }
        //Pending: image of restaurant logo
        //Pending: link to order number
        #endregion
    }
}

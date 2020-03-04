using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantApp
{
    enum ReceiptStatus
    {
        Pending,
        Completed,
        Cancelled
    }

    /// <summary>
    /// Defines a receipt
    /// </summary>
    class Receipt
    {
        #region Properties
        public int ID { get; private set; }
        public Order Order { get; set; }
        public int OrderID { get; set; }
        public ReceiptStatus Status { get; set; }
        /// <summary>
        /// Total cost before tax
        /// </summary>
        public decimal SubTotal { get; set; }        
        public decimal Tax { get; set; }
        /// <summary>
        /// Total cost after tax
        /// </summary>
        public decimal Total { get; set; }
        public DateTime CreatedDateTime { get; private set; }
        #endregion

        #region Construction
        public Receipt()
        {
            CreatedDateTime = DateTime.UtcNow;
        }
        #endregion
    }
}

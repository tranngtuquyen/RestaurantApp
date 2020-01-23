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
        private static int lastIDNumber = 0;
        public int ID { get; private set; }
        public Order Order { get; set; }
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
        public Restaurant Restaurant { get; set; }
        #endregion

        #region Construction
        public Receipt()
        {
            ID = ++lastIDNumber;
            CreatedDateTime = DateTime.UtcNow;
        }
        #endregion
    }
}

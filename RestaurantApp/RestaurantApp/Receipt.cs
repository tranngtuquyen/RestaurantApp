using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantApp
{
    public enum ReceiptStatus
    {
        Pending,
        Completed,
        Cancelled
    }

    /// <summary>
    /// Defines a receipt
    /// </summary>
    public class Receipt
    {
        #region Properties
        public int ID { get; set; }
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
        public DateTime CreatedDateTime { get; set; }
        public string UserID { get; set; }
        #endregion

        #region Construction
        public Receipt()
        {
            CreatedDateTime = DateTime.UtcNow;
        }
        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RestaurantApp
{
    public class ReceiptManager
    {
        private readonly RestaurantContext _context;
        public ReceiptManager(RestaurantContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Create receipt for an order
        /// </summary>
        /// <param name="order">Order</param>
        /// <returns>Newly created receipt</returns>
        public Receipt CreateReceipt(Order order, string userID, decimal tax)
        {
            var receipt = new Receipt
            {
                Order = order,
                OrderID = order.ID,
                Status = ReceiptStatus.Pending,
                SubTotal = order.Price,
                Tax = tax,
                UserID = userID
            };
            
            receipt.Total = receipt.SubTotal * (1 + receipt.Tax / 100);

            _context.Receipts.Add(receipt);
            _context.SaveChanges();

            return _context.Receipts.SingleOrDefault(r => r.ID == receipt.ID);
        }

        public List<Receipt> GetAllReceipts(string userID)
        {
            return _context.Receipts.Where(r => r.UserID == userID).OrderByDescending(o => o.CreatedDateTime).ToList();
        }

        public Receipt GetReceiptByID(int id)
        {
            return _context.Receipts.SingleOrDefault(r => r.ID == id);
        }

        public Receipt UpdateReceipt(Receipt newReceipt)
        {
            var oldReceipt = _context.Receipts.SingleOrDefault(r => r.ID == newReceipt.ID);
            oldReceipt.Status = newReceipt.Status;
            _context.SaveChanges();
            return oldReceipt;
        }
    }
}

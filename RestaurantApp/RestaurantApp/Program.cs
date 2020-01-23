using System;

namespace RestaurantApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var menuItem1 = new MenuItem {
                Name = "Sushi",
                Price = 10
            };
            
            var menuItem2 = new MenuItem {
                Name = "Sashimi",
                Price = 20
            };
            
            var order1 = OrderManager.CreateOrder("A1", "Less spicy");
            Console.WriteLine($"Order ID: {order1.ID}, Table Number: {order1.TableNumber}, Order Status: {order1.Status}, Customer Note: {order1.CustomerNote}, Date: {order1.CreatedDateTime}");

            var order2 = OrderManager.CreateOrder("B2");
            Console.WriteLine($"Order ID: {order2.ID}, Table Number: {order2.TableNumber}, Order Status: {order2.Status}, Customer Note: {order2.CustomerNote}, Date: {order2.CreatedDateTime}");

            var orderItem1 = OrderManager.CreateOrderItem(menuItem2, 2);
            Console.WriteLine($"OrderItem ID: {orderItem1.ID}, Menu Item Name: {orderItem1.MenuItem.Name}, Unit Price: {orderItem1.MenuItem.Price}, Quantity: {orderItem1.Quantity}, Price: {orderItem1.Price}");
           
            var receipt1 = ReceiptManager.CreateReceipt(order2);
            Console.WriteLine($"Receipt ID: {receipt1.ID}, OrderID link to receipt: {receipt1.Order.ID}, Receipt Status: {receipt1.Status}, Restaurant Name: {receipt1.Restaurant.Name}. Date: {receipt1.CreatedDateTime}");
        }
    }
}

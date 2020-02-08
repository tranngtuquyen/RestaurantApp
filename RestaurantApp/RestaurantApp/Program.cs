using System;
using System.Collections.Generic;
using System.Linq;

namespace RestaurantApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var menu = new Menu();
            var categories = menu.Categories;
            var menuItems = menu.MenuItems;

            OrderManager.Orders = new List<Order>();
            var orders = OrderManager.Orders;
            
            while (true)
            {

                const string exit = "0";
                const string editMenu = "1";
                const string viewMenu = "2";
                const string createOrder = "3";
                const string editOrder = "4";
                const string createReceipt = "5";
                
                Console.WriteLine("**********");
                Console.WriteLine($"{exit}. Exit");
                Console.WriteLine($"{editMenu}. Create category/Create menu item");
                Console.WriteLine($"{viewMenu}. View menu");
                Console.WriteLine($"{createOrder}. Create new order");
                Console.WriteLine($"{editOrder}. Edit existing order");
                Console.WriteLine($"{createReceipt}. Create new receipt");
                Console.WriteLine("**********");

                Console.Write("Select your option: ");

                var input = Console.ReadLine();

                switch (input)
                {
                    case exit:
                        {
                            return;
                        }
                    case editMenu:
                        {
                            EditMenu(menu, categories);
                            break;
                        }
                    case viewMenu:
                        {
                            Console.WriteLine("Menu");
                            foreach (var category in categories)
                            {   
                                Console.WriteLine($"---Category: {category.Name}");

                                var items = (menuItems.Where(m => m.Category == category));
                                foreach (var menuItem in items)
                                {
                                    PrintMenuItem(menuItem);    
                                }
                            }
                            break;
                        }
                    case createOrder:
                        {
                            Console.Write("Enter table number: ");
                            var tableNumber = Console.ReadLine();

                            Console.Write("Enter customer note: ");
                            var customerNote = Console.ReadLine();

                            var order = OrderManager.CreateOrder(tableNumber, customerNote);
                            AddOrderItemToNewOrder(menuItems, order);

                            order.CalculateOrderPrice();

                            Console.WriteLine("-----");
                            Console.WriteLine("Successfully created new order!");
                            PrintOrderWithOrderItems(order);

                            orders.Add(order);
                            break;
                        }
                    case editOrder:
                        {
                            EditOrder(menuItems);
                            break;
                        }
                    case createReceipt:
                        {
                            Console.Write("Table number: ");
                            var tableNumber = Console.ReadLine();
                            var order = OrderManager.GetPendingOrderByTableNumber(tableNumber);

                            var receipt = ReceiptManager.CreateReceipt(order);

                            Console.WriteLine("-----");
                            Console.WriteLine("Successfully created receipt");
                            Console.WriteLine($"ReceiptID: {receipt.ID}, Subtotal: {receipt.SubTotal:C}, Tax: {receipt.Tax:P}, Total Price: {receipt.Total:C}, Status: {receipt.Status}, Date: {receipt.CreatedDateTime}, Restaurant Name: {receipt.Restaurant.Name}");
                            PrintOrderWithOrderItems(order);
                            break;                           
                        }
                    default:
                        {
                            Console.WriteLine("Invalid option. Try again!");
                            break;
                        }
                }
            }
        }

        private static void EditOrder(List<MenuItem> menuItems)
        {
            Console.Write("Table number: ");
            var tableNumber = Console.ReadLine();
            var order = OrderManager.GetPendingOrderByTableNumber(tableNumber);
            PrintOrderWithOrderItems(order);

            Console.WriteLine("Select option to edit");

            const string back = "0";
            const string editCustomerNote = "1";
            const string editOrderStatus = "2";
            const string addOrderItem = "3";

            var input = "";
            while (input != back)
            {
                Console.WriteLine("*****");
                Console.WriteLine($"{back}. Back");
                Console.WriteLine($"{editCustomerNote}. Edit customer note");
                Console.WriteLine($"{editOrderStatus}. Edit order status");
                Console.WriteLine($"{addOrderItem}. Add new order item");
                Console.WriteLine("*****");

                Console.Write("Enter your option: ");
                input = Console.ReadLine();

                switch (input)
                {
                    case back:
                        {
                            Console.WriteLine("Done editing!");

                            order.CalculateOrderPrice();

                            PrintOrderWithOrderItems(order);
                            break;
                        }
                    case editCustomerNote:
                        {
                            Console.Write("Enter new customer note: ");
                            var note = Console.ReadLine();
                            order.CustomerNote = note;
                            break;
                        }
                    case editOrderStatus:
                        {
                            Console.WriteLine("---List Order Status ---");
                            var listStatus = Enum.GetNames(typeof(OrderStatus));
                            for (var i = 0; i < listStatus.Length; i++)
                            {
                                Console.WriteLine($"{i}. {listStatus[i]}");
                            }
                            Console.Write("Select your option: ");
                            var status = Enum.Parse<OrderStatus>(Console.ReadLine());
                            order.Status = status;
                            break;
                        }
                    case addOrderItem:
                        {
                            OrderItem orderItem = AddOrderItem(menuItems, order);

                            PrintOrderItem(orderItem);

                            break;
                        }
                    default:
                        {
                            Console.WriteLine("Invalid option. Try again!");
                            break;
                        }
                }
            }
        }
        private static void EditMenu(Menu menu, List<Category> categories)
        {
            const string back = "0";
            const string createCategory = "1";
            const string createMenuItem = "2";

            var input = "";
            while (input != back)
            {
                Console.WriteLine("*****");
                Console.WriteLine($"{back}. Back");
                Console.WriteLine($"{createCategory}. Create new category");
                Console.WriteLine($"{createMenuItem}. Create new menu item");
                Console.WriteLine("*****");
                Console.Write("Select option: ");
                input = Console.ReadLine();

                switch (input)
                {
                    case back:
                        {
                            Console.WriteLine("Done adding new item to menu");
                            break;
                        }
                    case createCategory:
                        {
                            Console.Write("Enter category name: ");
                            var categoryName = Console.ReadLine();

                            var category = MenuManager.CreateCategory(categoryName);
                            menu.AddCategory(category);

                            Console.WriteLine("-----");
                            Console.WriteLine("Successfully created new category");
                            Console.WriteLine($"Category ID: {category.ID}, Category Name: {category.Name}");
                            break;
                        }
                    case createMenuItem:
                        {
                            Console.Write("Enter item name: ");
                            var itemName = Console.ReadLine();

                            Console.Write("Enter item description: ");
                            var itemDescription = Console.ReadLine();

                            Console.Write("Enter item price: ");
                            var itemPrice = Convert.ToDecimal(Console.ReadLine());

                            Console.WriteLine("Select category");
                            Console.WriteLine("--- List Categories ---");
                            foreach (var category in categories)
                            {
                                Console.WriteLine($"{category.ID}. {category.Name}");
                            }

                            Console.Write("Enter your option: ");

                            var id = Convert.ToInt32(Console.ReadLine());
                            var itemCategory = MenuManager.GetCategoryByID(id, categories);

                            var menuItem = MenuManager.CreateMenuItem(itemName, itemDescription, itemPrice, itemCategory);
                            menu.AddMenuItem(menuItem);

                            Console.WriteLine("-----");
                            Console.WriteLine("Successfully created new menu item");
                            PrintMenuItem(menuItem);

                            break;
                        }

                    default:
                        {
                            Console.WriteLine("Invalid option. Try again!");
                            break;
                        }
                }
            }
        }

        private static void PrintMenuItem(MenuItem menuItem)
        {
            Console.WriteLine($"{menuItem.ID}. Item name: {menuItem.Name}, Price: {menuItem.Price:C}, Description: {menuItem.Description}, Category: {menuItem.Category.Name}");
        }
        
        private static OrderItem AddOrderItem(List<MenuItem> menuItems, Order order)
        {
            Console.WriteLine("--- List Menu Items ---");
            
            foreach (var item in menuItems)
            {
                PrintMenuItem(item);
            }
            Console.Write("Select menu item: ");
            var id = Convert.ToInt32(Console.ReadLine());
            var menuItem = MenuManager.GetMenuItemByID(id, menuItems);

            Console.Write("Enter quantity: ");
            var quantity = Convert.ToInt32(Console.ReadLine());

            var orderItem = OrderManager.CreateOrderItem(menuItem, quantity);
            order.OrderItems.Add(orderItem);

            Console.WriteLine("-----");
            Console.WriteLine("Successfully created new order item");

            return orderItem;
        }
        private static void AddOrderItemToNewOrder(List<MenuItem> menuItems, Order order)
        {
            const string back = "0";
            const string addOrderItem = "1";

            var input = "";
            while (input != back)
            {
                Console.WriteLine("*****");
                Console.WriteLine($"{back}. Back");
                Console.WriteLine($"{addOrderItem}. Add new order item");
                Console.WriteLine("*****");
                Console.Write("Select your option: ");
                input = Console.ReadLine();

                switch (input)
                {
                    case back:
                        {
                            Console.WriteLine("Done adding!");
                            break;
                        }
                    case addOrderItem:
                        {
                            OrderItem orderItem = AddOrderItem(menuItems, order);

                            PrintOrderItem(orderItem);
                            break;

                        }
                    default:
                        {
                            Console.WriteLine("Invalid option. Try again!");
                            break;
                        }
                }
            }
        }

        private static void PrintOrderItem(OrderItem orderItem)
        {
            Console.WriteLine($"OrderItemID: {orderItem.ID}, MenuItem: {orderItem.MenuItem.Name}, Quantity: {orderItem.Quantity}, MenuItem Price: {orderItem.MenuItem.Price:C}, Total Price: {orderItem.Price:C}");
        }

        private static void PrintOrderWithOrderItems(Order order)
        {
            Console.WriteLine($"---OrderID: {order.ID}, Table Number: {order.TableNumber}, Customer Note: {order.CustomerNote}, Total Price: {order.Price:C}, Status: {order.Status}, Date: {order.CreatedDateTime}");
            var orderItems = order.OrderItems;
            foreach (var item in orderItems)
            {
                PrintOrderItem(item);
            }
        }
    }
}

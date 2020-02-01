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
                Console.WriteLine("**********");
                Console.WriteLine("0. Exit");
                Console.WriteLine("1. Create category/Create menu item");
                Console.WriteLine("2. View menu");
                Console.WriteLine("3. Create new order");
                Console.WriteLine("4. Edit existing order");
                Console.WriteLine("5. Cancel order");
                Console.WriteLine("6. Create new receipt");
                Console.WriteLine("**********");

                Console.Write("Select your option: ");
                var option = Console.ReadLine();

                switch (option)
                {
                    case "0":
                        {
                            return;
                        }
                    case "1":
                        {
                            var option1 = "";
                            while (option1 != "0")
                            {
                                Console.WriteLine("*****");
                                Console.WriteLine("0. Done modifying menu");
                                Console.WriteLine("1. Create new category");
                                Console.WriteLine("2. Create new menu item");
                                Console.WriteLine("*****");
                                Console.Write("Select option: ");
                                option1 = Console.ReadLine();

                                switch (option1)
                                {
                                    case "0":
                                        {
                                            Console.WriteLine("Done adding new item to menu");
                                            break;
                                        }
                                    case "1":
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
                                    case "2":
                                        {
                                            Console.Write("Enter item name: ");
                                            var itemName = Console.ReadLine();

                                            Console.Write("Enter item description: ");
                                            var itemDescription = Console.ReadLine();

                                            Console.Write("Enter item price: ");
                                            var itemPrice = Convert.ToDecimal(Console.ReadLine());

                                            Console.WriteLine("--- List Categories ---");
                                            for (int i = 0; i < categories.Count; i++)
                                            {
                                                Console.WriteLine($"{i}. {categories[i].Name}");
                                            }
                                            Console.Write("Select category: ");
                                            var index = Convert.ToInt32(Console.ReadLine());
                                            var itemCategory = categories[index];
                                            var menuItem = MenuManager.CreateMenuItem(itemName, itemDescription, itemPrice, itemCategory);
                                            menu.AddMenuItem(menuItem);

                                            Console.WriteLine("-----");
                                            Console.WriteLine("Successfully created new menu item");
                                            Console.WriteLine($"ItemID: {menuItem.ID}, Item name: {menuItem.Name}, Description: {menuItem.Description}, Price: {menuItem.Price:C}, Category: {menuItem.Category.Name}");

                                            break;
                                        }

                                    default:
                                        {
                                            Console.WriteLine("Invalid option. Try again!");
                                            break;
                                        }
                                }
                            }
                            break;
                        }
                    case "2":
                        {
                            Console.WriteLine("Menu");
                            for (int i = 0; i < categories.Count; i++)
                            {
                                var items = (menuItems.Where(m => m.Category == categories[i]));
                                Console.WriteLine($"---Category: {categories[i].Name}");
                                foreach (var menuItem in items)
                                {
                                    Console.WriteLine($"------Item: {menuItem.Name}, Description: {menuItem.Description}, Price: {menuItem.Price:C}");
                                }
                            }
                            break;
                        }
                    
                    case "3":
                        {
                            Console.Write("Enter the table number: ");
                            var tableNumber = Console.ReadLine();

                            Console.Write("Enter customer note: ");
                            var customerNote = Console.ReadLine();

                            var order = OrderManager.CreateOrder(tableNumber, customerNote);

                            var option1 = "";
                            while (option1 != "0")
                            {
                                Console.WriteLine("*****");
                                Console.WriteLine("0. Done adding");
                                Console.WriteLine("1. Create new order item");
                                Console.WriteLine("*****");
                                Console.Write("Select your option: ");
                                option1 = Console.ReadLine();

                                switch (option1)
                                {
                                    case "0":
                                        {
                                            Console.WriteLine("Done adding");
                                            break;
                                        }
                                    case "1":
                                        {
                                            Console.WriteLine("--- List Menu Items ---");
                                            for (var i = 0; i < menuItems.Count; i++)
                                            {
                                                var item = menuItems[i];
                                                Console.WriteLine($"{i}. {item.Name}: {item.Price:C}");
                                            }
                                            Console.Write("Select menu item: ");
                                            var index = Convert.ToInt32(Console.ReadLine());
                                            var menuItem = menuItems[index];

                                            Console.Write("Enter quantity: ");
                                            var quantity = Convert.ToInt32(Console.ReadLine());

                                            var orderItem = OrderManager.CreateOrderItem(menuItem, quantity);
                                            order.OrderItems.Add(orderItem);

                                            Console.WriteLine("-----");
                                            Console.WriteLine("Successfully created new order item");
                                            Console.WriteLine($"OrderItemID: {orderItem.ID}, MenuItem: {orderItem.MenuItem.Name}, Quantity: {orderItem.Quantity}, MenuItem Price: {orderItem.MenuItem.Price:C}, Total Price: {orderItem.Price:C}");
                                            break;

                                        }
                                    default:
                                        {
                                            Console.WriteLine("Invalid option. Try again!");
                                            break;
                                        }
                                }
                            }
                            
                            order.GetOrderPrice();

                            Console.WriteLine("-----");
                            Console.WriteLine("Successfully created new order!");
                            Console.WriteLine($"---OrderID: {order.ID}, Table Number: {order.TableNumber}, Customer Note: {order.CustomerNote}, Total Price: {order.Price:C}, Status: {order.Status}, Date: {order.CreatedDateTime}");
                            var orderItems = order.OrderItems;
                            for (var i = 0; i < orderItems.Count; i++)
                            {
                                var item = orderItems[i];
                                Console.WriteLine($"-----Item ID: {item.ID}, MenuItem Name: {item.MenuItem.Name}, Quantity: {item.Quantity}, MenuItem Price: {item.MenuItem.Price:C}, Total Price: {item.Price:C}, Status: {item.Status}");
                            }

                            orders.Add(order);
                            break;
                        }
                    case "4":
                        {
                            break;
                        }
                    case "5":
                        {
                            break;
                        }
                    case "6":
                        {
                            Console.WriteLine("--- List Orders ---");
                            var ordersSelect = orders.Where<Order>(o => o.Status == OrderStatus.New || o.Status == OrderStatus.InProgress).ToList();
                            for (int i = 0; i < ordersSelect.Count; i++)
                            {
                                var o = ordersSelect[i];
                                Console.WriteLine($"{i}. OrderID: {o.ID}, Table Number: {o.TableNumber}, Total Price: {o.Price:C}, Status: {o.Status}");
                            }

                            Console.Write("Select your order: ");
                            var index = Convert.ToInt32(Console.ReadLine());
                            var order = ordersSelect[index];

                            var receipt = ReceiptManager.CreateReceipt(order);

                            Console.WriteLine("-----");
                            Console.WriteLine("Successfully created receipt");
                            Console.WriteLine($"ReceiptID: {receipt.ID}, Subtotal: {receipt.SubTotal:C}, Tax: {receipt.Tax:P}, Total Price: {receipt.Total:C}, Status: {receipt.Status}, Date: {receipt.CreatedDateTime}, Restaurant Name: {receipt.Restaurant.Name}");

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
    }
}

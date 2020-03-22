using System;
using System.Collections.Generic;
using System.Linq;

namespace RestaurantApp
{
    class Program
    {
        static void Main(string[] args) { }

        //static void Main(string[] args)
        //{
        //    RestaurantContext db = new RestaurantContext();
        //    MenuManager._context = db;
        //    OrderManager.DB = db;
        //    ReceiptManager.DB = db;

        //    while (true)
        //    {

        //        const string exit = "0";
        //        const string createMenu = "1";
        //        const string editMenu = "2";
        //        const string viewAllMenus = "3";
        //        const string createOrder = "4";
        //        const string editOrder = "5";
        //        const string createReceipt = "6";
                
        //        Console.WriteLine("**********");
        //        Console.WriteLine($"{exit}. Exit");
        //        Console.WriteLine($"{createMenu}. Create new menu");
        //        Console.WriteLine($"{editMenu}. Create category/Create menu item");
        //        Console.WriteLine($"{viewAllMenus}. View menu");
        //        Console.WriteLine($"{createOrder}. Create new order");
        //        Console.WriteLine($"{editOrder}. Edit existing order");
        //        Console.WriteLine($"{createReceipt}. Create new receipt");
        //        Console.WriteLine("**********");

        //        Console.Write("Select your option: ");

        //        var input = Console.ReadLine();

        //        switch (input)
        //        {
        //            case exit:
        //                {
        //                    return;
        //                }
        //            case createMenu:
        //                {
        //                    Console.Write("Enter menu name: ");
        //                    var menuName = Console.ReadLine();
        //                    var menu = MenuManager.CreateMenu(menuName);
        //                    Console.WriteLine($"{menu.ID}. {menu.Name}");
                            
        //                    break;
        //                }
        //            case editMenu:
        //                {
        //                    EditMenu();
        //                    break;
        //                }
        //            case viewAllMenus:
        //                {
        //                    ViewAllMenus();

        //                    break;
        //                }
        //            case createOrder:
        //                {
        //                    Console.Write("Enter table number: ");
        //                    var tableNumber = Console.ReadLine();

        //                    Console.Write("Enter customer note: ");
        //                    var customerNote = Console.ReadLine();

        //                    var order = OrderManager.CreateOrder(tableNumber, customerNote);
        //                    AddOrderItemToNewOrder(order);

        //                    OrderManager.CalculateTotalOrderPrice(order);

        //                    Console.WriteLine("-----");
        //                    Console.WriteLine("Successfully created new order!");
        //                    PrintOrderWithOrderItems(order);
        //                    break;
        //                }
        //            case editOrder:
        //                {
        //                    EditOrder();
        //                    break;
        //                }
        //            case createReceipt:
        //                {
        //                    try
        //                    {
        //                        Console.WriteLine("List of orders pending for receipts:");
        //                        PrintOrdersPendingReceipt();
        //                        Console.Write("Enter table number: ");
        //                        var tableNumber = Console.ReadLine();
        //                        var order = OrderManager.GetPendingOrderByTableNumber(tableNumber);

        //                        var receipt = ReceiptManager.CreateReceipt(order);

        //                        Console.WriteLine("-----");
        //                        Console.WriteLine("Successfully created receipt");
        //                        Console.WriteLine($"ReceiptID: {receipt.ID}, Subtotal: {receipt.SubTotal:C}, Tax: {receipt.Tax:P}, Total Price: {receipt.Total:C}, Status: {receipt.Status}, Date: {receipt.CreatedDateTime}");
        //                        PrintOrderWithOrderItems(order);
        //                    }
        //                    catch (ArgumentException ax)
        //                    {
        //                        Console.WriteLine(ax.Message);
        //                    }
        //                    break;
        //                }

        //            default:
        //                {
        //                    Console.WriteLine("Invalid option. Try again!");
        //                    break;
        //                }
        //        }
        //    }
        //}

        //private static void ViewAllMenus()
        //{
        //    Console.WriteLine("View All Menus");

        //    var menus = MenuManager.GetAllMenus();
        //    foreach (var menu in menus)
        //    {
        //        Console.WriteLine($"Menu {menu.ID}. {menu.Name}");
        //        var categories = MenuManager.GetAllCategoriesByMenu(menu);
        //        foreach (var category in categories)
        //        {
        //            Console.WriteLine($"--Category {category.ID}. {category.Name}");
        //            var menuItems = MenuManager.GetAllMenuItemsByCategoryInMenu(category, menu);
        //            foreach (var menuItem in menuItems)
        //            {
        //                Console.Write("----");
        //                PrintMenuItem(menuItem);
        //            }
        //        }
        //    }
        //}

        //private static void EditOrder()
        //{
        //    try
        //    {
        //        Console.Write("Table number: ");
        //        var tableNumber = Console.ReadLine();
        //        var order = OrderManager.GetPendingOrderByTableNumber(tableNumber);
        //        PrintOrderWithOrderItems(order);

        //        Console.WriteLine("Select option to edit");

        //        const string back = "0";
        //        const string editCustomerNote = "1";
        //        const string editOrderStatus = "2";
        //        const string addOrderItem = "3";

        //        var input = "";
        //        while (input != back)
        //        {
        //            Console.WriteLine("*****");
        //            Console.WriteLine($"{back}. Back");
        //            Console.WriteLine($"{editCustomerNote}. Edit customer note");
        //            Console.WriteLine($"{editOrderStatus}. Edit order status");
        //            Console.WriteLine($"{addOrderItem}. Add new order item");
        //            Console.WriteLine("*****");

        //            Console.Write("Enter your option: ");
        //            input = Console.ReadLine();

        //            switch (input)
        //            {
        //                case back:
        //                    {
        //                        Console.WriteLine("Done editing!");

        //                        break;
        //                    }
        //                case editCustomerNote:
        //                    {
        //                        Console.Write("Enter new customer note: ");
        //                        var note = Console.ReadLine();
        //                        order.CustomerNote = note;
        //                        OrderManager.SaveChangesOnOrder(order);
        //                        break;
        //                    }
        //                case editOrderStatus:
        //                    {
        //                        Console.WriteLine("---List Order Status ---");
        //                        var listStatus = Enum.GetNames(typeof(OrderStatus));
        //                        for (var i = 0; i < listStatus.Length; i++)
        //                        {
        //                            Console.WriteLine($"{i}. {listStatus[i]}");
        //                        }
        //                        Console.Write("Select your option: ");
        //                        var status = Enum.Parse<OrderStatus>(Console.ReadLine());
        //                        order.Status = status;
        //                        OrderManager.SaveChangesOnOrder(order);
        //                        break;
        //                    }
        //                case addOrderItem:
        //                    {
        //                        OrderItem orderItem = AddOrderItem(order);
                                
        //                        if (orderItem != null)
        //                        {
        //                            PrintOrderItem(orderItem);
        //                        }

        //                        break;
        //                    }
        //                default:
        //                    {
        //                        Console.WriteLine("Invalid option. Try again!");
        //                        break;
        //                    }
        //            }
        //        }
        //    }
        //    catch (ArgumentException ax)
        //    {
        //        Console.WriteLine(ax.Message);
        //    }
        //}
        
        //private static void EditMenu()
        //{
        //    const string back = "0";
        //    const string createCategory = "1";
        //    const string createMenuItem = "2";

        //    var input = "";
        //    while (input != back)
        //    {
        //        Console.WriteLine("*****");
        //        Console.WriteLine($"{back}. Back");
        //        Console.WriteLine($"{createCategory}. Create new category");
        //        Console.WriteLine($"{createMenuItem}. Create new menu item");
        //        Console.WriteLine("*****");
        //        Console.Write("Select option: ");
        //        input = Console.ReadLine();

        //        switch (input)
        //        {
        //            case back:
        //                {
        //                    Console.WriteLine("Done adding new item to menu");
        //                    break;
        //                }
        //            case createCategory:
        //                {
        //                    CreateCategory();
        //                    break;
        //                }
        //            case createMenuItem:
        //                {  
        //                    var flag = 0;
        //                    try
        //                    {
        //                        Console.Write("Enter item name: ");
        //                        var itemName = Console.ReadLine();

        //                        Console.Write("Enter item description: ");
        //                        var itemDescription = Console.ReadLine();

        //                        Console.Write("Enter item price: ");
        //                        var itemPrice = Convert.ToDecimal(Console.ReadLine());
        //                        flag = 1;

        //                        Console.WriteLine("Select category");
        //                        Console.WriteLine("-----List of menus-----");

        //                        var menus = MenuManager.GetAllMenus();

        //                        foreach (var m in menus)
        //                        {
        //                            Console.WriteLine($"{m.ID}. {m.Name}");
        //                        }

        //                        Console.Write("Enter menu id: ");
        //                        var menuId = Convert.ToInt32(Console.ReadLine());
        //                        var menu = MenuManager.GetMenuByID(menuId);

        //                        var categories = MenuManager.GetAllCategoriesByMenu(menu);

        //                        Console.WriteLine("Select category");
        //                        Console.WriteLine("--- List Categories ---");
        //                        foreach (var category in categories)
        //                        {
        //                            Console.WriteLine($"{category.ID}. {category.Name}");
        //                        }

        //                        Console.Write("Enter your option: ");

        //                        var id = Convert.ToInt32(Console.ReadLine());
        //                        var itemCategory = MenuManager.GetCategoryByID(id);

        //                        var menuItem = MenuManager.CreateMenuItem(itemName, itemDescription, itemPrice, itemCategory, menu);

        //                        if (menuItem != null)
        //                        {
        //                            Console.WriteLine("-----");
        //                            Console.WriteLine("Successfully created new menu item");
        //                            PrintMenuItem(menuItem);
        //                        }
        //                    }
        //                    catch (FormatException)
        //                    {
        //                        if (flag == 1)
        //                        {
        //                            Console.WriteLine("Invalid category id format!");
        //                        }
        //                        else
        //                        {
        //                            Console.WriteLine("Invalid item price format!");
        //                        }
        //                    }
        //                    catch (OverflowException)
        //                    {
        //                        if (flag == 1)
        //                        {
        //                            Console.WriteLine("Category id is out of range!");
        //                        }
        //                        else
        //                        {
        //                            Console.WriteLine("Item price is out of range!");
        //                        }
        //                    }
        //                    catch (ArgumentOutOfRangeException ax)
        //                    {
        //                        Console.WriteLine(ax.Message);
        //                    }
        //                    catch (ArgumentException ax)
        //                    {
        //                        Console.WriteLine(ax.Message);
        //                    }
        //                    break;
        //                }

        //            default:
        //                {
        //                    Console.WriteLine("Invalid option. Try again!");
        //                    break;
        //                }
        //        }
        //    }
        //}

        //private static void CreateCategory()
        //{
        //    try
        //    {
        //        Console.Write("Enter category name: ");
        //        var categoryName = Console.ReadLine();

        //        Console.WriteLine("-----List of menus-----");

        //        var menus = MenuManager.GetAllMenus();

        //        foreach (var m in menus)
        //        {
        //            Console.WriteLine($"{m.ID}. {m.Name}");
        //        }

        //        Console.Write("Enter menu id: ");
        //        var id = Convert.ToInt32(Console.ReadLine());
        //        var menu = MenuManager.GetMenuByID(id);
        //        var category = MenuManager.CreateCategory(categoryName, menu);

        //        Console.WriteLine("-----");
        //        Console.WriteLine("Successfully created new category");
        //        Console.WriteLine($"Category ID: {category.ID}, Category Name: {category.Name}, Menu: {category.Menu.Name}");
        //    }
        //    catch (FormatException)
        //    {
        //        Console.WriteLine("Menu id has invalid format");
        //    }
        //    catch (OverflowException)
        //    {
        //        Console.WriteLine("Menu id is out of range");
        //    }
        //    catch (ArgumentException ax)
        //    {
        //        Console.WriteLine(ax.Message);
        //    }
        //}


        //private static void PrintMenuItem(MenuItem menuItem)
        //{
        //    if (menuItem.Category == null)
        //    {
        //        MenuManager.GetAllCategory();
        //    }
        //    Console.WriteLine($"{menuItem.ID}. Item name: {menuItem.Name}, Price: {menuItem.Price:C}, Description: {menuItem.Description}, Category: {menuItem.Category.Name}");
        //}
        
        //private static OrderItem AddOrderItem(Order order)
        //{
        //    ViewAllMenus();
        //    var flag = 0;
        //    try
        //    {
        //        Console.Write("Select menu item: ");
        //        var id = Convert.ToInt32(Console.ReadLine());
        //        flag = 1;
        //        var menuItem = MenuManager.GetMenuItemByID(id);

        //        Console.Write("Enter quantity: ");
        //        var quantity = Convert.ToInt32(Console.ReadLine());

        //        var orderItem = OrderManager.CreateOrderItem(menuItem, order, quantity);
        //        OrderManager.CalculateTotalOrderPrice(order);

        //        Console.WriteLine("-----");
        //        Console.WriteLine("Successfully created new order item");
        //        return orderItem;
        //    }
        //    catch (FormatException)
        //    {
        //        if (flag == 1)
        //        {
        //            Console.WriteLine("Invalid quantity format!");
        //        }
        //        else
        //        {
        //            Console.WriteLine("Invalid menu item id format!");
        //        }
        //    }
        //    catch (OverflowException)
        //    {
        //        if (flag == 1)
        //        {
        //            Console.WriteLine("Quantity out of range");
        //        }
        //        else
        //        {
        //            Console.WriteLine("Menu item id out of range");
        //        }
        //    }
        //    catch (ArgumentOutOfRangeException ax)
        //    {
        //        Console.WriteLine(ax.Message);
        //    }
        //    catch (ArgumentException ax)
        //    {
        //        Console.WriteLine(ax.Message);
        //    }
        //    return null;
        //}
        //private static void AddOrderItemToNewOrder(Order order)
        //{
        //    const string back = "0";
        //    const string addOrderItem = "1";

        //    var input = "";
        //    while (input != back)
        //    {
        //        Console.WriteLine("*****");
        //        Console.WriteLine($"{back}. Back");
        //        Console.WriteLine($"{addOrderItem}. Add new order item");
        //        Console.WriteLine("*****");
        //        Console.Write("Select your option: ");
        //        input = Console.ReadLine();

        //        switch (input)
        //        {
        //            case back:
        //                {
        //                    Console.WriteLine("Done adding!");
        //                    break;
        //                }
        //            case addOrderItem:
        //                {
        //                    OrderItem orderItem = AddOrderItem(order);
        //                    if (orderItem != null)
        //                    {
        //                        PrintOrderItem(orderItem);
        //                    }
                            
        //                    break;

        //                }
        //            default:
        //                {
        //                    Console.WriteLine("Invalid option. Try again!");
        //                    break;
        //                }
        //        }
        //    }
        //}
        
        //private static void PrintOrderItem(OrderItem orderItem)
        //{
        //    if (orderItem.MenuItem == null)
        //    {
        //        MenuManager.GetAllMenuItems();
        //    }

        //    if (orderItem != null && orderItem.MenuItem != null)
        //    {
        //        Console.WriteLine($"OrderItemID: {orderItem.ID}, Quantity: {orderItem.Quantity}, Menu Item: {orderItem.MenuItem.Name}, MenuItem: {orderItem.MenuItem.Price:C}, Total Price: {orderItem.Price:C}");

        //    }
        //}

        //private static void PrintOrderWithOrderItems(Order order)
        //{
        //    if (order != null)
        //    {
        //        Console.WriteLine($"---OrderID: {order.ID}, Table Number: {order.TableNumber}, Customer Note: {order.CustomerNote}, Total Price: {order.Price:C}, Status: {order.Status}, Date: {order.CreatedDateTime}");
        //        var orderItems = OrderManager.GetAllOrderItemsInOrder(order);
        //        if (orderItems != null)
        //        {
        //            foreach (var item in orderItems)
        //            {
        //                PrintOrderItem(item);
        //            }
        //        }
        //    }
        //}

        //private static void PrintOrdersPendingReceipt()
        //{
        //    var orders = OrderManager.GetOrdersPendingReceipts();

        //    if (orders != null)
        //    {
        //        foreach (var order in orders)
        //        {
        //            Console.WriteLine($"{order.ID}. Table number: {order.TableNumber}, Price: {order.Price}, Status: {order.Status}");
        //        }
        //    } 
        //    else
        //    {
        //        Console.WriteLine("No order pending for receipt");
        //    }
            
        //}

    }
}

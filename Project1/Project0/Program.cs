using BusinessLogic;
using DataAccess;
using DataAccess.Entities;
using DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using Serilog;

namespace Project0
{
    public class Program
    {
        
        public static readonly ILoggerFactory MyLoggerFactory = LoggerFactory.Create(builder =>
        {
            builder.AddSerilog();
        });
        static void Main(string[] args)
        {
            string connectionString = SecretConfiguration.ConnectionString;

            Log.Logger = new LoggerConfiguration().WriteTo.File("D:\\Revature\\girard-project0\\Project0\\log.txt").CreateLogger();

            DbContextOptions<BlockBusterContext> options = new DbContextOptionsBuilder<BlockBusterContext>()
                .UseSqlServer(connectionString)
                .UseLoggerFactory(MyLoggerFactory.AddSerilog(Log.Logger))
                .Options;

            using var context = new BlockBusterContext(options);

            var repository = new BlockBusterRepository(context);



            string select;

            do
            {
                PrintMenu();


                select = Console.ReadLine();

                switch (Int32.Parse(select))
                {
                    case 1:
                        Console.Clear();
                        List<BlockBuster> locations = new List<BlockBuster>();
                        repository.GetStores(locations);
                        DisplayStores(locations);
                        Console.WriteLine("\nPlease enter the ID of your desired location?");
                        string id = Console.ReadLine();
                        BlockBuster choice = SelectStore(locations, Int32.Parse(id));
                        Console.WriteLine("\nEnter Customer's First Name");
                        string fname = Console.ReadLine();
                        Console.WriteLine("Enter Customer's Last Name");
                        string lname = Console.ReadLine();
                        try
                        {
                            Customer c = new Customer(fname, lname);
                            if (!repository.SearchForCustomer(c))
                            {
                                repository.AddNewCustomer(c);
                            }
                            else
                            {
                                var retCust = repository.GetCustomerByName(c.FirstName, c.LastName);
                                c.CustomerId = retCust.CustomerId;
                            }
                            string response;
                            do
                            {
                                Console.WriteLine("\nThis is our current selection of movies and video games");
                                repository.DisplayInventory(choice.LocationId);
                                Console.WriteLine("\nPlease enter the title of your selection");
                                string selection = Console.ReadLine();
                                Product p = repository.GetProductByTitle(selection, choice);
                                Order o = new Order();
                                repository.MakeOrder(c.CustomerId, choice.LocationId, o);
                                p.ReduceInventory();
                                repository.EditInventory(choice, p);
                                repository.AddProductToOrder(o, p);
                                Console.WriteLine("\nWould you like to add another product to the order?");
                                Console.WriteLine("Please enter Yes or No");
                                response = Console.ReadLine();
                            } while (response == "Yes" || response == "yes");
                        }
                        catch (Exception ex)
                        {
                            Console.Clear();
                            Console.WriteLine($"\n{ex.Message}");
                        }
                        break;
                    case 2:
                        try
                        {
                            Console.Clear();
                            Console.WriteLine("\nEnter Customer's First Name");
                            string firstname = Console.ReadLine();
                            Console.WriteLine("Enter Customer's Last Name");
                            string lastname = Console.ReadLine();
                            Customer custHistory = new Customer(firstname, lastname);
                            Console.Clear();
                            var query = from customerS in context.Customers
                                        join order in context.Orders on customerS.CustomerId equals order.CustomerId
                                        join orderD in context.OrderDetails on order.OrderId equals orderD.OrderId
                                        join inv in context.Inventory on orderD.InventoryId equals inv.InventoryId
                                        join prod in context.Products on inv.ProductId equals prod.ProductId
                                        where customerS.FirstName == custHistory.FirstName && customerS.LastName == custHistory.LastName
                                        select new { order.OrderId, customerS.FirstName, customerS.LastName, order.Date, prod.Title, prod.Price };
                            foreach (var item in query)
                            {
                                Console.WriteLine($"OrderID: {item.OrderId} Name: {item.FirstName} {item.LastName} Date: {item.Date} \nTitle: {item.Title} Price: ${item.Price}");
                            }
                        } catch (Exception e)
                        {
                            Console.Clear();
                            Console.WriteLine($"\n{e.Message}");
                        }
                        break;
                    case 3:
                        Console.Clear();
                        List<BlockBuster> locationLookup = new List<BlockBuster>();
                        repository.GetStores(locationLookup);
                        DisplayStores(locationLookup);
                        Console.WriteLine("\nPlease enter the ID of your desired location?");
                        string pick = Console.ReadLine();
                        Console.Clear();
                        try
                        {
                            var storeQuery = from store in context.Stores
                                             join order in context.Orders on store.LocationId equals order.LocationId
                                             join storeCust in context.Customers on order.CustomerId equals storeCust.CustomerId
                                             join orderD in context.OrderDetails on order.OrderId equals orderD.OrderId
                                             join inv in context.Inventory on orderD.InventoryId equals inv.InventoryId
                                             join prod in context.Products on inv.ProductId equals prod.ProductId
                                             where store.LocationId == Int32.Parse(pick)
                                             select new { store.City, store.State, storeCust.FirstName, storeCust.LastName, order.Date, prod.Title, prod.Price, inv.InventoryAmount };
                            foreach (var item in storeQuery)
                            {
                                Console.WriteLine($"Store Location: {item.City}, {item.State}\nName: {item.FirstName} {item.LastName} Date: {item.Date} \nTitle: {item.Title} Price: ${item.Price}\n In Stock: {item.InventoryAmount}\n");
                            }
                        } catch (Exception x)
                        {
                            Console.Clear();
                            Console.WriteLine($"\nYou forgot to enter a valid ID.");
                        }
                        break;
                }
            } while (Int32.Parse(select) <= 3);
        }



        public static void DisplayStores(List<BlockBuster> print)
        {
            foreach (BlockBuster store in print)
            {
                Console.WriteLine($"\nID: {store.LocationId} Location: {store.Location}");
            }
        }

        public static BlockBuster SelectStore(List<BlockBuster> stores, int id)
        {
            var q = stores.Select(b => b).Where(n => n.LocationId == id);
            return q.First();
        }


        public static void PrintMenu()
        {
            Console.WriteLine("\n\tMENU\n=======================================");
            Console.WriteLine("\n1. Make an order ");
            Console.WriteLine("\n2. Display customer order history");
            Console.WriteLine("\n3. Display store order history");
            Console.WriteLine("\n4. Exit");
            Console.WriteLine("\n=======================================");
            Console.WriteLine("\n Enter your choice (from 1 to 4 ): ");
        }
    }
}
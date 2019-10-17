using DataAccess.Entities;
using BusinessLogic;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Text;

namespace DataAccess.Repositories
{
    public class BlockBusterRepository
    {
        private readonly BlockBusterContext _dbContext;

        public BlockBusterRepository(BlockBusterContext context)
        {
            _dbContext = context ?? throw new ArgumentNullException(nameof(context));
        }


        public List<BlockBuster> GetStores(List<BlockBuster> locations)
        {
            if (_dbContext.Stores.Any())
            {
                foreach (Stores store in _dbContext.Stores)
                {
                    BlockBuster b = new BlockBuster(store.LocationId, $"{store.City}, {store.State}");
                    locations.Add(b);
                }
            } else
            {
                Console.WriteLine("There are no stores open");
            }
            return locations;
        }

        public bool SearchForCustomer(Customer customer1)
        {
            if (_dbContext.Customers.Any(c => (c.FirstName == customer1.FirstName) && (c.LastName == customer1.LastName)))
            {
                return true;
            } else
            {
                return false;
            }
        }


        public void AddNewCustomer(Customer customer1)
        {

            var customer = new DataAccess.Entities.Customers
            {
                FirstName = customer1.FirstName,
                LastName = customer1.LastName
            };

            _dbContext.Customers.Add(customer);
            _dbContext.SaveChanges();
            customer1.CustomerId = customer.CustomerId;
        }

        public Product GetProductByTitle(string search, BlockBuster b)
        {
            var sqlProduct = _dbContext.Products.First(p => p.Title == search);
            var sqlInventory = _dbContext.Inventory.First(i => 
            (i.ProductId == sqlProduct.ProductId) && (b.LocationId == i.LocationId));
            Product product;
            return product = new Product(sqlProduct.ProductId, sqlProduct.Title, sqlProduct.Details, 
                sqlProduct.Price, sqlProduct.Rating, sqlInventory.InventoryAmount);
        }   

        public void AddProductToOrder(Order o, Product p)
        {
            var track = _dbContext.Inventory.Include(p => p.Product).Select(z => z).Where(l => (l.ProductId == p.ProductId));
            var product = new DataAccess.Entities.OrderDetails
            {
                OrderId = o.OrderId,
                InventoryId = track.First().InventoryId
            };

            _dbContext.OrderDetails.Add(product);
            _dbContext.SaveChanges();
        }

        public List<Customers> GetCustomers(string firstname, string lastname)
        {
            return _dbContext.Customers.Select(c => c).ToList();
        }

        public Customers GetCustomerByName(string firstname, string lastname)
        {
            return _dbContext.Customers.First(c => (c.FirstName == firstname) && (c.LastName == lastname));
        }

        public void MakeOrder(int customerId, int storeId, Order blogic)
        {
            var order = new DataAccess.Entities.Orders
            {
                CustomerId = customerId,
                LocationId = storeId,
                Date = blogic.OrderDate
            };

            _dbContext.Orders.Add(order);
            _dbContext.SaveChanges();
            blogic.OrderId = order.OrderId;
        }

        public void DisplayInventory(int storeId)
        {
            foreach (Inventory s in _dbContext.Inventory.Include(p => p.Product).Where(l => l.LocationId == storeId))
            {
                Console.WriteLine($"\nTitle: {s.Product.Title} " +
                    $"Rated: {s.Product.Rating}\nDetails: {s.Product.Details}\nPrice: {s.Product.Price}");
            }
        }

        public void DeleteInventory(BlockBuster b, Product p)
        {
            _dbContext.Inventory.Remove(_dbContext.Inventory.
                First(i => (i.LocationId == b.LocationId) && (i.ProductId == p.ProductId)));
        }

        public void EditInventory(BlockBuster b, Product p)
        {
            var Inventory = _dbContext.Inventory.First(i => (i.LocationId == b.LocationId) && (i.ProductId == p.ProductId));

            Inventory.InventoryAmount = p.InventoryAmount;
            _dbContext.SaveChanges();
            p.ProductId = Inventory.ProductId;
        }


    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic
{
    public class BlockBuster
    {
        public string Location { get; set; }

        public int LocationId { get; set; }

        public List<Product> Inventory = new List<Product>();

        public List<Order> OrderHistory = new List<Order>();

        public List<Customer> Customers = new List<Customer>();

        public BlockBuster (int id, string location)
        {
            this.Location = location;
            this.LocationId = id;
        }


        public void LogOrder(Order order)
        {
            OrderHistory.Add(order);
        }

        public void AddCustomer(Customer c)
        {
            Customers.Add(c);
        }

    }
}

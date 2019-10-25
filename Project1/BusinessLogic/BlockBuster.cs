using System.Collections.Generic;

namespace BusinessLogic
{
    public class BlockBuster
    {
        public string Location { get; set; }

        public int LocationId { get; set; }

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

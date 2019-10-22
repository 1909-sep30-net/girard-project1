using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic
{
    public class CustomerOrderHistory
    {

        public int OrderID { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }

    }
}

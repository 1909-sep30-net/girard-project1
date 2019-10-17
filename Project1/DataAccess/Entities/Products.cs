using System;
using System.Collections.Generic;

namespace DataAccess.Entities
{
    public partial class Products
    {
        public Products()
        {
            Inventory = new HashSet<Inventory>();
        }

        public int ProductId { get; set; }
        public string Title { get; set; }
        public string Rating { get; set; }
        public string Details { get; set; }
        public decimal Price { get; set; }

        public virtual ICollection<Inventory> Inventory { get; set; }
    }
}

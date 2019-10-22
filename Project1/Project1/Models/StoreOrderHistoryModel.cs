using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Project1.Models
{
    public class StoreOrderHistoryModel
    {
        [Required]
        public int OrderID { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public int InventoryAmount { get; set; }
    }
}

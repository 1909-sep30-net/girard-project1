using System;
using System.ComponentModel.DataAnnotations;

namespace Project1.Models
{
    public class CustomerOrderHistoryModel
    {
        [Required]
        public int OrderID { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
    }
}

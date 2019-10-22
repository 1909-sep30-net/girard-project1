using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Project1.Models
{
    public class ProductModel
    {
        public int ID { get; set; }
        [Required]
        public string Title { get; set; }

        [Required]
        public string Rating { get; set; }

        [Required]
        public string Details { get; set; }
        [Required]
        public decimal Price { get; set; }

        public int StoreID { get; set; }


    }
}

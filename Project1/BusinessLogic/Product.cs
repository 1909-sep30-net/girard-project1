using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic
{
    public class Product
    {
        private int _InventoryAmount;
        public int InventoryAmount
        {
            get => _InventoryAmount;
            set
            {
                if (value == 0)
                {
                    throw new ArgumentException("The inventory amount can't be set to zero", nameof(value));
                }
                else if (value < 0)
                {
                    throw new ArgumentException("The inventory amount can't be set to a negative number\n", nameof(value));
                }
                _InventoryAmount = value;
            }
        }

        private string _Title;

        public string Title
        {
            get => _Title;
            set
            {
                if (value.Length == 0)
                {
                    throw new ArgumentException("You forgot to enter the Title.", nameof(value));
                }
                _Title = value;
            }
        }

        public decimal Price { get; set; }
        public string Rating { get; set; }

        public string Details { get; set; }

        public int ProductId { get; set; }

        public Product(int id, string title, string details, decimal price, string rating, int count)
        {
            this.ProductId = id;
            this.Title = title;
            this.Details = details;
            this.Price = price;
            this.Rating = rating;
            this.InventoryAmount = count;
        }

        public void ReduceInventory ()
        {
            if (this.InventoryAmount > 0)
            {
                this.InventoryAmount--;
            }
            else
            {
                Console.WriteLine("That item is out of stock");
            }
        }
    }
}

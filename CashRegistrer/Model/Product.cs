using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CashRegistrer.Model
{
    public class Product
    {
        public int Id { get; set; }

        [MinLength(5)]
        public string Name { get; set; }
        public double Price { get; set; }
        public string? Category { get; set; }
        public string BarCode { get; set; }
        public int QuantityInStock { get; set; }

        public Product(string name, double price, string category, int quantity, string barcode = "")
        {
            Name = name;
            Price = price;
            Category = category;
            QuantityInStock = quantity;
            BarCode = barcode;
        }
        public override string ToString()
        {
            return $"[+] {Category} - {Name} - {Price}£";
        }
    }
}
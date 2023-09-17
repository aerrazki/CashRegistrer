using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CashRegistrer.Model
{
    public class ProductCatalog
    {
        private List<Product> products = new List<Product>();

        public void AddProduct(Product product)
        {
            products.Add(product);
        }

        public List<Product> GetProducts()
        {
            return products;
        }

        public Product GetProductById(int id)
        {
            var product = products.FirstOrDefault(p => p.Id == id);
            if (product == null)
                Console.WriteLine($"Product Id '{id}' not found in the catalog.");

            return product;
        }

        public Product GetProductByName(string name)
        {
            var product = products.FirstOrDefault(p => p.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
            if (product == null)
                Console.WriteLine($"Product '{name}' not found in the catalog.");

            return product;
        }

        public Product GetProductByBarcode(string barcode)
        {
            var product = products.FirstOrDefault(p => p.BarCode.Equals(barcode, StringComparison.OrdinalIgnoreCase));
            if (product == null)
                Console.WriteLine($"Product barcode'{barcode}' not found in the catalog.");
            return product;
        }
    }
}
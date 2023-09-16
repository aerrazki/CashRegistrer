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

        public Product GetProductByName(string name)
        {
            var product = products.FirstOrDefault(p => p.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
            if (product == null)
            {
                throw new InvalidOperationException($"Product '{name}' not found in the catalog.");
            }
            return product;
        }

        public Product GetProductByBarcode(string barcode)
        {
            var product = products.FirstOrDefault(p => p.BarCode.Equals(barcode, StringComparison.OrdinalIgnoreCase));
            if (product == null)
            {
                throw new InvalidOperationException($"Product Barcode '{barcode}' not found in the catalog.");
            }
            return product;
        }
    }
}
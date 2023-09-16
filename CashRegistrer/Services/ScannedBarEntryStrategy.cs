using CashRegistrer.Interfaces;
using CashRegistrer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashRegistrer.Services
{
    public class ScannedBarEntryStrategy : IAddToCartStrategy
    {
        private readonly ProductCatalog catalog;
        private ShoppingCart cart;

        public ScannedBarEntryStrategy(ProductCatalog catalog, ShoppingCart cart)
        {
            this.catalog = catalog;
            this.cart = cart;
        }

        public void AddToCart(string barcode, int quantity)
        {
            var productEntity = catalog.GetProductByBarcode(barcode);
            if (productEntity == null)
                Console.WriteLine($"Product with barcode '{barcode}' not found in the catalog.");
            else
            {
                Console.WriteLine($"Adding product by scanning barcode: ");
                cart.AddToCart(productEntity, quantity);
            }
        }
    }
}


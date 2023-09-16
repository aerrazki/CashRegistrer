using CashRegistrer.Interfaces;
using CashRegistrer.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashRegistrer.Services
{
    public class ManualEntryStrategy : IAddToCartStrategy
    {
        private readonly ProductCatalog catalog;
        private readonly ShoppingCart cart;


        public ManualEntryStrategy(ProductCatalog catalog, ShoppingCart cart)
        {
            this.catalog = catalog;
            this.cart = cart;
        }

        public void AddToCart(string product, int quantity)
        {
            var productEntity = catalog.GetProductByName(product);
            if (productEntity == null)
                Console.WriteLine($"Product '{product}' not found in the catalog.");
            else
                cart.AddToCart(productEntity, quantity);

        }
    }
}
using CashRegistrer.Interfaces;
using CashRegistrer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace CashRegistrer.Services
{
    public class ScannedBarEntryStrategy : IAddToCartStrategy
    {
        private readonly ProductCatalog catalog;
        private readonly ShoppingCart cart;

        public ScannedBarEntryStrategy(ProductCatalog catalog, ShoppingCart cart)
        {
            this.catalog = catalog ?? throw new ArgumentNullException(nameof(catalog));
            this.cart = cart ?? throw new ArgumentNullException(nameof(cart));
        }

        public string ScanBarCode()
        {
            // Simulated barcode input for testing purposes
            var simulatedBarCodeInput = "1234567890";

            // Simulate scanning process (in a real scenario, this would interact with the barcode scanner SDK)
            var messages = new List<string>
            {
                "Please place the scanner device on your product barcode",
                "Scanning...",
            };

            var product = catalog.GetProductByBarcode(simulatedBarCodeInput);

            if (product != null)
                messages.Add($"Successful, product identified : {product}");

            Console.ForegroundColor = ConsoleColor.Blue;

            foreach (string message in messages)
            {
                Console.WriteLine(message);
                Thread.Sleep(1000);
            }

            Console.ResetColor();
            return simulatedBarCodeInput;
        }

        public void AddToCart(string barcode, int quantity = 1)
        {
            var productEntity = catalog.GetProductByBarcode(barcode);

            if (productEntity == null)
                Console.WriteLine($"Product with barcode '{barcode}' not found in the catalog.");
            else
                cart.AddToCart(productEntity, quantity);

        }
    }
}

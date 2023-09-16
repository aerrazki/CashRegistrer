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
        public string ScanBarCode()
        {
            var simulatedBarCodeInput = "1234567890";

            /*
             * - This code will contain the API wiring with the barcode reading device SDK
             * - After scanning, it should get the barcode digits
             * - TO DO : WIRE Barcode scanner SDK here.                     
             */

            var messages = new List<string>{
            "Please place the scanner device on your product barcode",
            "Scanning...",
            };
            var product = catalog.GetProductByBarcode(simulatedBarCodeInput);

            if (product != null)
                messages.Add($"Successful, product identified : {product}");


            foreach (string message in messages)
            {
                Console.WriteLine(message);
                Thread.Sleep(1000);
            }
            return simulatedBarCodeInput;
        }
        public void AddToCart(string barcode, int quantity = 1)
        {
            var productEntity = catalog.GetProductByBarcode(barcode);
            if (productEntity == null)
                Console.WriteLine($"Product with barcode '{barcode}' not found in the catalog.");
            else
            {
                Console.WriteLine($"Added product : {productEntity}");
                cart.AddToCart(productEntity, quantity);
            }
        }
    }
}


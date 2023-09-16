using CashRegistrer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CashRegistrer.Model
{
    public class ShoppingCart
    {
        private Dictionary<Product, int> items = new Dictionary<Product, int>();
        private ProductCatalog productCatalog;
        private List<IDiscountManager> discountManager;
        public ShoppingCart(ProductCatalog catalog, List<IDiscountManager> discountStrategy)
        {
            productCatalog = catalog;
            discountManager = discountStrategy;
        }

        public void AddToCart(Product product, int quantity)
        {
            var catalogProduct = productCatalog.GetProductByName(product.Name);

            if (catalogProduct == null)
            {
                Console.WriteLine($"Product '{product.Name}' not found in the catalog.");
                return;
            }

            if (quantity <= 0)
            {
                Console.WriteLine("Invalid quantity.");
                return;
            }

            if (quantity > catalogProduct.QuantityInStock)
            {
                throw new InvalidOperationException($"Not enough '{product.Name}' in stock.");
            }

            catalogProduct.QuantityInStock -= quantity;

            if (items.ContainsKey(product))
            {
                items[product] += quantity;
            }
            else
            {
                items[product] = quantity;
            }
        }

        public void DisplayReceipt(double initalTotalPrice, double discount)
        {
            Console.ForegroundColor= ConsoleColor.Blue;
            Console.WriteLine("==================Receipt===================");

            Console.WriteLine("Items in the Cart:");
            Console.ResetColor();

            foreach (var item in GetItems())
            {
                Console.WriteLine($"[+] {item.Key.Name} ({item.Key.Category}): {item.Value} x {item.Key.Price}£ ");
            }
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("--------------------------------------------");

            Console.WriteLine($"Total Amount To Pay : {initalTotalPrice}£");

            var totalPrice = initalTotalPrice - discount;

            if (discount > 0)
                Console.WriteLine($"Discount : - {discount}£");
            Console.WriteLine("============================================");
            Console.WriteLine($"Left to pay : {totalPrice}£");
            Console.WriteLine("==================Receipt===================");
            Console.ResetColor();

        }

        public double GetDiscount()
        {
            var discount = 0.0;
            foreach (var discountStrategy in discountManager)
                discount += discountStrategy.ApplyDiscount(this);

            return discount;
        }
        public double CalculateTotalPrice()
        {
            double totalPrice = 0.0;

            foreach (var item in items)
                totalPrice += item.Key.Price * item.Value;

            return totalPrice;
        }

        public Dictionary<Product, int> GetItems()
        {
            return items;
        }
        public void UpdateCart(Dictionary<Product, int> updatedItems)
        {
            items = updatedItems;
        }
    }
}
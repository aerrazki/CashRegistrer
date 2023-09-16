using CashRegistrer.Interfaces;
using CashRegistrer.Model;
using CashRegistrer.Services;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using System;
using System.Collections.Generic;

namespace CashRegistrer.Tests
{
    [TestFixture]
    public class ShoppingCartTests
    {
        private ProductCatalog productCatalog;
        private ShoppingCart cart;
        private ManualEntryStrategy manualEntryStrategy;
        private ScannedBarEntryStrategy scannedBarcode;

        [SetUp]
        public void SetUp()
        {
            productCatalog = new ProductCatalog();
            var discountsActivated = new List<IDiscountManager>() {
                new BuyOneGetOneDiscount(),
                new Buy10ItemsProductGetOneEuro()
            };

            cart = new ShoppingCart(productCatalog, discountsActivated);
            manualEntryStrategy = new ManualEntryStrategy(productCatalog, cart);
            scannedBarcode = new ScannedBarEntryStrategy(productCatalog, cart);

            // Add some test products to the catalog
            productCatalog.AddProduct(new Product("Apple", 1.0, "Fruits", 100));
            productCatalog.AddProduct(new Product("Banana", 0.5, "Fruits", 50));
            productCatalog.AddProduct(new Product("Laptop", 1000.0, "Electronics",quantity: 10, barcode:"1234567890"));
        }

        [Test]
        public void AddProductManually_ValidProduct_AddsToCart()
        {
            // Arrange
            var productName = "Apple";
            var quantity = 2;

            // Act
            manualEntryStrategy.AddToCart(productName, quantity);

            // Assert
            var items = cart.GetItems();
            Assert.IsTrue(items.ContainsKey(productCatalog.GetProductByName(productName)));
            Assert.AreEqual(items[productCatalog.GetProductByName(productName)], quantity);
        }

        [Test]
        public void AddProductManually_InvalidProduct_DisplayErrorMessage()
        {
            // Arrange
            var productName = "InvalidProduct";
            var quantity = 2;

            // Act and Assert
            Assert.Throws<InvalidOperationException>(() =>
            {
                manualEntryStrategy.AddToCart(productName, quantity); // Cannot Add a not found product in Catalog to Cart
            });

        }

        [Test]
        public void AddProductByScanningBarcode_ValidProduct_AddsToCart()
        {
            // Arrange
            var barcode = "1234567890";
            var quantity = 1;

            // Act
            scannedBarcode.AddToCart(barcode, quantity);

            // Assert
            var items = cart.GetItems();
            Assert.IsTrue(items.ContainsKey(productCatalog.GetProductByBarcode(barcode)));
            Assert.AreEqual(items[productCatalog.GetProductByBarcode(barcode)], quantity);
        }

        [Test]
        public void AddProductByScanningBarcode_InvalidProduct_DisplayErrorMessage()
        {
            // Arrange
            var barcode = "111111111";
            var quantity = 1;

            // Act
            Assert.Throws<InvalidOperationException>(() =>
            {
                scannedBarcode.AddToCart(barcode, quantity);
            });
        }

        [Test]
        public void CalculateTotalPrice_EmptyCart_ReturnsZero()
        {
            // Act
            var totalPrice = cart.CalculateTotalPrice();

            // Assert
            Assert.AreEqual(0.0, totalPrice);
        }

        [Test]
        public void CalculateTotalPrice_ItemsInCart_CalculatesTotalPriceWithoutDiscount()
        {
            // Arrange
            manualEntryStrategy.AddToCart("Apple", 2);
            manualEntryStrategy.AddToCart("Banana", 3);

            // Act
            var totalPrice = cart.CalculateTotalPrice();

            // Assert
            Assert.AreEqual(3.5, totalPrice); // 2 Apples at 1.0 each + 3 Bananas at 0.5 each
        }

        [Test]
        public void CalculateTotalPrice_ItemsInCart_CalculatesTotalPriceWithDiscount()
        {
            // Arrange
            manualEntryStrategy.AddToCart("Apple", 2);
            manualEntryStrategy.AddToCart("Banana", 3);

            // Act
            var initalPrice = cart.CalculateTotalPrice();
            var discount = cart.GetDiscount();
            var items = cart.GetItems();
            var totalPrice = initalPrice - discount;
            // Assert
            Assert.AreEqual(items[productCatalog.GetProductByName("Banana")], 4); // After [Buy One Get One discount] for the first 2 banans one will be already paid by the discount, and for the 3rd bananas the customer will have another free banana in his shopping cart makes it 4 in total
            Assert.AreEqual(3.5, initalPrice); // 2 Apples at 1.0 each + 3 Bananas at 0.5 each
            Assert.AreEqual(2, totalPrice); // 2 Apples at 1.0 each + 3 Bananas at 0.5 each
        }

        [Test]
        public void GetDiscount_NoDiscounts_ReturnsZero()
        {
            // Act
            var discount = cart.GetDiscount();

            // Assert
            Assert.AreEqual(0.0, discount);
        }

        [Test]
        public void GetDiscount_ApplyDiscounts_CalculatesDiscount()
        {
            // Arrange
            manualEntryStrategy.AddToCart("Apple", 4); // Buy One Get One discount
            manualEntryStrategy.AddToCart("Banana", 10); // [Buy One Get One discount] & [Buy10ItemsProductGetOneEuro discount]

            // Act
            var discount = cart.GetDiscount();
            Console.WriteLine(discount);

            // Assert
            Assert.AreEqual(5.5, discount); // 2 euros discount for apple + 2.5 euros for bananas for [Buy One Get One offer] + 1 euro (bananas) for [Buy10ItemsProductGetOneEuro]
        }
    }
}

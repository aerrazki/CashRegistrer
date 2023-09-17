using CashRegistrer.Interfaces;
using CashRegistrer.Model;
using CashRegistrer.Services;
using System;
using System.Collections.Generic;

class Program
{
    static ProductCatalog productCatalog;
    static ShoppingCart cart;
    static ManualEntryStrategy manualEntryStategy;
    static ScannedBarEntryStrategy scannedBarCode;
    static List<IDiscountManager> discountsActivated;

    static void Main()
    {
        Initialize();

        bool exit = false;

        while (!exit)
        {
            Console.Clear();
            Console.WriteLine("Main Menu");
            Console.WriteLine("1. Add Product Manually");
            Console.WriteLine("2. Add Product by Scanning Barcode");
            Console.WriteLine("3. View Cart");
            Console.WriteLine("4. Pay - Display Receipt");
            Console.WriteLine("5. Exit");

            string choice = Utilities.ReadStringFromConsole("Enter your choice: ");

            switch (choice)
            {
                case "1":
                    AddProductManually();
                    WaitForKeyPress();
                    break;
                case "2":
                    AddProductByScanningBarcode();
                    WaitForKeyPress();
                    break;
                case "3":
                    ViewCart();
                    WaitForKeyPress();
                    break;
                case "4":
                    Pay();
                    WaitForKeyPress();
                    exit = true;
                    break;
                case "5":
                    exit = true;
                    WaitForKeyPress();
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
    }

    static void Initialize()
    {

        productCatalog = new ProductCatalog();
        discountsActivated = new List<IDiscountManager>() {
            new BuyOneGetOneDiscount(),
            new Buy10ItemsProductGetOneEuro()
        };

        // Add products to the catalog
        cart = new ShoppingCart(productCatalog, discountsActivated);
        manualEntryStategy = new ManualEntryStrategy(productCatalog, cart);
        scannedBarCode = new ScannedBarEntryStrategy(productCatalog, cart);
        var products = new List<Product>
        {
            new Product(1, "Apple", 1.0, "Fruits", 100),
            new Product(2, "Banana", 0.5, "Fruits", 50),
            new Product(3, "Laptop", 1000.0, "Electronics", 10, barcode:"1234567890"),
            new Product(4, "Orange", 0.75, "Fruits", 75),
            new Product(5, "Strawberries", 2.0, "Fruits", 30),
            new Product(6, "Milk", 2.5, "Dairy", 40),
            new Product(7, "Bread", 1.0, "Bakery", 60),
            new Product(8, "Toothpaste", 3.0, "Personal Care", 20),
            new Product(9, "Shampoo", 4.0, "Personal Care", 15),
            new Product(10,"T-shirt", 15.0, "Apparel", 25),
            new Product(11, "Jeans", 30.0, "Apparel", 15)
        };

        foreach (var product in products)
        {
            productCatalog.AddProduct(product);
        }
    }

    static void DisplayCatalog()
    {
        foreach (var item in productCatalog.GetProducts().OrderBy(x => x.Id).ThenBy(x => x.Category))
            if (item.Id > 9)
                Console.WriteLine($"{item.Id} - [+] {item.Category} - {item.Name} - {item.Price}£");
            else
                Console.WriteLine($"{item.Id}  - [+] {item.Category} - {item.Name} - {item.Price}£");

        Console.WriteLine();
    }
    static void AddProductManually()
    {
        DisplayCatalog();
        var inputId = Utilities.ReadIntegerFromConsole("Enter the product number: ");
        var product = productCatalog.GetProductById(inputId);
        var quantity = Utilities.ReadIntegerFromConsole("Enter the product quantity: ");
        if (product != null)
            manualEntryStategy.AddToCart(product.Name, quantity);
    }

    static void AddProductByScanningBarcode()
    {
        // Initiate barcode scanning and get the barcode to identify the product and add to cart
        var barcode = scannedBarCode.ScanBarCode();

        scannedBarCode.AddToCart(barcode);
    }

    static void ViewCart()
    {
        Console.WriteLine("Items in the Cart:");
        Console.ForegroundColor = ConsoleColor.Blue;
        var items = cart.GetItems();
        if(items.Count > 0)
        foreach (var item in cart.GetItems())
            Console.WriteLine($"[+] {item.Key.Name} ({item.Key.Category}): {item.Value} x {item.Key.Price}£ ");
        else
            Console.WriteLine($"[!] Your shopping cart is empty");

        Console.ResetColor();

    }

    static void Pay()
    {
        double totalToPay = cart.CalculateTotalPrice();
        double totalDiscount = cart.GetDiscount();

        cart.DisplayReceipt(totalToPay, totalDiscount);
    }
    static void WaitForKeyPress()
    {
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }

}

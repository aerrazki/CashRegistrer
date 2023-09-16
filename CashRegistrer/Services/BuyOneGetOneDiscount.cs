using CashRegistrer.Interfaces;
using CashRegistrer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashRegistrer.Services
{
    public class BuyOneGetOneDiscount : IDiscountManager
    {
        public double ApplyDiscount(ShoppingCart cart)
        {
            var offerActivationMessage = "[Buy one Get One] Offer Activated: ";
            double discount = 0;
            foreach (var productCart in cart.GetItems())
            {
                var totalItemPrice = productCart.Key.Price * productCart.Value;
                if (productCart.Value > 1)
                {
                    if (productCart.Value % 2 == 0)
                    {
                        discount += totalItemPrice / 2;
                        Console.WriteLine($"{offerActivationMessage} {totalItemPrice / 2}£ discount on {productCart.Key.Name}");
                    }
                    else
                    {
                        //Calculating discount for pair number of articles (QUANTITY-1), rewarding the article remained by adding a free one in the Shopping cart
                        var totalItemPriceOdd = productCart.Key.Price * (productCart.Value - 1);

                        discount += totalItemPriceOdd / 2;

                        //Adding the free item
                        cart.AddToCart(productCart.Key, 1);
                        Console.Write($"{offerActivationMessage} ");
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write($"1 more free '{productCart.Key.Name}' to add to cart");
                        Console.ResetColor();

                        Console.Write($"and {totalItemPriceOdd / 2}£ discount on {productCart.Key.Name}");
                    }
                }
                else
                {
                    cart.AddToCart(productCart.Key, 1);
                    Console.Write($"{offerActivationMessage} ");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write($"1 more free '{productCart.Key.Name}' to add to cart");
                    Console.ResetColor();


                }
            }
            Console.ResetColor();
            Console.WriteLine($"\nTotal discount Applied [Buy one Get One] Offer  : {discount}£");
            return discount;
        }
    }
}

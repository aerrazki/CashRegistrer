using CashRegistrer.Interfaces;
using CashRegistrer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashRegistrer.Services
{
    public class Buy10ItemsProductGetOneEuro : IDiscountManager
    {
        public double ApplyDiscount(ShoppingCart cart)
        {
            int discount = 0;
            foreach (var cartProduct in cart.GetItems())
            {
                discount += ((cartProduct.Value) / 10);
            }
            Console.WriteLine($"Total discount Applied for [Buy X10 same product Get one] offer : {discount}£\n");
            return discount;
        }
    }
}



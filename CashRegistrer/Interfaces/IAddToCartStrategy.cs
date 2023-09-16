using CashRegistrer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashRegistrer.Interfaces
{
    public interface IAddToCartStrategy
    {
        public void AddToCart(string product, int quantity);
    }
}

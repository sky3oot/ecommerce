using Basket.Abstraction.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basket.Abstraction.Service
{
  public  interface IBasketApplication
    {
        Task<ShoppingCart> GetBasket(string userName);
        Task<List<string>> UpdateBasket(ShoppingCart basket);
        Task<List<string>> DeleteBasket(string userName);
    }
}

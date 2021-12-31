
using Basket.Abstraction.Entities;
using Basket.Abstraction.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basket.Abstraction.Repositories
{
    public interface IBasketRepository : IRepository<ShoppingCartEntity, string>
    {
    }
}

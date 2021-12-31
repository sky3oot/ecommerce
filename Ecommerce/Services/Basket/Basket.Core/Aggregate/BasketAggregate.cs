using Basket.Abstraction.Entities;
using Basket.Abstraction.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basket.Core.Aggregate
{
    public class BasketAggregate : BaseAggregate<ShoppingCartEntity>
    {
        public BasketAggregate(ShoppingCartEntity entity) : base(entity)
        {

        }

       
        public void SaveBasket(ShoppingCart cart)
        {
            PopulateEntity(cart);
        }

    
        public void DeleteBasket()
        {
            Entity.IsDeleted = true;
        }


       
        public void ValidateBasket(ShoppingCart cart)
        {
            
        }

   
        private void PopulateEntity(ShoppingCart cart)
        {
            Entity.UserName = cart.UserName;
            Entity.Items = cart.Items;
            
            
        }
    }
}

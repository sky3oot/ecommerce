using Basket.Abstraction.Models;
using Basket.Abstraction.Repositories;
using Basket.Abstraction.Service;
using Basket.Core.Aggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basket.Core.Service
{
   public class BasketApplication : IBasketApplication
    {
        private readonly IBasketRepository _repository;

        public BasketApplication(IBasketRepository repository)
        {

            _repository = repository;
        }


        public async Task<List<string>> DeleteBasket(string userName)
        {
       
            var entity = await _repository.GetOne(userName);
            var result = new List<string>();

            if (entity == null)
            {
                result.Add("Person not found");
                return result;
            }

            var aggregate = new BasketAggregate(entity);
            if (aggregate.ResultMessages.Count < 1)
            {
                
                
                aggregate.DeleteBasket();
                await _repository.Save(aggregate.Entity);
            }
            else
            {
                result = aggregate.ResultMessages;
            }
            return result;
        }

        public async Task<ShoppingCart> GetBasket(string userName)
        {
            var entity = await _repository.GetOne(userName);
            var product = new ShoppingCart(entity);
            return product;
          
        }

        public async Task<List<string>> UpdateBasket(ShoppingCart basket)
        {
           
            var entity = await _repository.GetOne(basket.UserName);
            var result = new List<string>();

            if (entity == null)
            {
                result.Add("Basket not found");
                return result;
            }

            var aggregate = new BasketAggregate(entity);
            aggregate.ValidateBasket(basket);
            if (aggregate.ResultMessages.Count < 1)
            {
              
                aggregate.SaveBasket(basket);
                await _repository.Save(aggregate.Entity);
            }
            else
            {
                result = aggregate.ResultMessages;
            }
            return result;
        }
    }
}

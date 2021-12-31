using Discount.Abstraction.Model;
using Discount.Abstraction.Repositories;
using Discount.Abstraction.Services;
using Discount.Core.Aggregate;
using Discount.Infrastracture.Data.Mongo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discount.Core.Services
{
 public   class DiscountApplication : IDiscountApplication
    {
        private readonly IDscountRepository _repository;

        public DiscountApplication(IDscountRepository repository)
        {

            _repository = repository;
        }


        public async Task CreateDiscount(Coupon coupon)
        {
            var aggregate = new DiscountAggregate(new Coupon());
            var result = new List<string>();
            aggregate.ValidateDiscount(coupon);
            if (aggregate.ResultMessages.Count < 1)
            {


                aggregate.SaveDiscount(coupon);
                await _repository.Save(aggregate.Entity);
            }
            else
            {
                result = aggregate.ResultMessages;
            }
           
        }


        public async Task<List<string>> DeleteDiscount(string productName)
        {
            var entity = await _repository.GetOne(productName);
            var result = new List<string>();

            if (entity == null)
            {
                result.Add("Product not found");
                return result;
            }

            var aggregate = new DiscountAggregate(entity);
            if (aggregate.ResultMessages.Count < 1)
            {

                aggregate.DeleteDiscount();
                await _repository.Save(aggregate.Entity);
            }
            else
            {
                result = aggregate.ResultMessages;
            }
            return result;
        }

        public async Task<IEnumerable<Coupon>> GetDiscount(string productName)
        {
            var products = await _repository.GetDiscountByProduct(productName);
            return products.ToList();
        }

        //public async Task UpdateDiscount(Coupon coupon)
        //{
        //    var entity = await _repository.GetOne(coupon.ProductName);
        //    var result = new List<string>();

        //    if (entity == null)
        //    {
        //        result.Add("Product not found");
          
        //    }

        //    var aggregate = new DiscountAggregate(entity);
        //    aggregate.ValidateDiscount(coupon);
        //    if (aggregate.ResultMessages.Count < 1)
        //    {

        //        aggregate.SaveDiscount(coupon);
        //        await _repository.Save(aggregate.Entity);
        //    }
        //    else
        //    {
        //        result = aggregate.ResultMessages;
        //    }
            
        //}

        public async Task<List<string>> UpdateDiscount(Coupon coupon)
        {
            var entity = await _repository.GetOne(coupon.ProductName);
            var result = new List<string>();

            if (entity == null)
            {
                result.Add("Product not found");
                return result;

            }

            var aggregate = new DiscountAggregate(entity);
            aggregate.ValidateDiscount(coupon);
            if (aggregate.ResultMessages.Count < 1)
            {

                aggregate.SaveDiscount(coupon);
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

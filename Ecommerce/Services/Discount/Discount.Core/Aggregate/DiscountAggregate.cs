using Discount.Abstraction.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discount.Core.Aggregate
{
    public class DiscountAggregate : BaseAggregate<Coupon>
    {
        public DiscountAggregate(Coupon entity) : base(entity)
        {

        }


        public void SaveDiscount(Coupon coupon)
        {
            PopulateEntity(coupon);
        }


        public void DeleteDiscount()
        {
            Entity.IsDeleted = true;
        }



        public void ValidateDiscount(Coupon coupon)
        {
         
        }


        private void PopulateEntity(Coupon coupon)
        {
            Entity.Id = coupon.Id;
            Entity.ProductName = coupon.ProductName;
            Entity.Description = coupon.Description;
            Entity.Amount = coupon.Amount;
           
         

        }
    }
}

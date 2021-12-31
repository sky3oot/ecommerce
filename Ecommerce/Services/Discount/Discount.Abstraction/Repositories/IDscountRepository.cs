using Discount.Abstraction.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discount.Abstraction.Repositories
{
  public  interface IDscountRepository:IRepository<Coupon,string>
    {
        Task<IEnumerable<Coupon>> GetDiscountByProduct(string productName);
    }
}

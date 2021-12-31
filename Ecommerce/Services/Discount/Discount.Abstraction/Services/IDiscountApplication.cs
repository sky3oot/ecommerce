using Discount.Abstraction.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discount.Abstraction.Services
{
   public interface IDiscountApplication
    {
        Task<IEnumerable<Coupon>> GetDiscount(string productName);

        Task CreateDiscount(Coupon coupon);
        Task<List<string>> UpdateDiscount(Coupon coupon);
        Task<List<string>> DeleteDiscount(string productName);

       
    }
}

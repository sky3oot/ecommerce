using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discount.Abstraction.Model
{
    public class Coupon:IEntity
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public int Amount { get; set; }
        public bool IsDeleted { get ; set ; }
      
    }
}

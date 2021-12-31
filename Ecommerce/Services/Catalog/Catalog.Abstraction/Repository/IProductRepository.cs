
using Catalog.Abstraction.Entities;
using Catalog.Abstraction.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Abstraction.Repository
{
   public interface IProductRepository:IRepository<ProductEntity,string>
    {
   
            
            Task<IEnumerable<ProductEntity>> GetProductByName(string name);
            Task<IEnumerable<ProductEntity>> GetProductByCategory(string categoryName);

         
        
    }
}

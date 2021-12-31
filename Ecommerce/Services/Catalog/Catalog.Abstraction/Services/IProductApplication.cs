using Catalog.Abstraction.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Abstraction.Services
{
    public interface IProductApplication
    {
        Task<IEnumerable<Product>> GetProducts();
        Task<Product> GetProduct(string id);
        Task<IEnumerable<Product>> GetProductByName(string name);
        Task<IEnumerable<Product>> GetProductByCategory(string categoryName);

        Task CreateProduct(Product product);
        Task<List<string>> UpdateProduct(Product product);
        Task<List<string>> DeleteProduct(string id);
    }
}

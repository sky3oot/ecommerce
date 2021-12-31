using Catalog.Abstraction.Entities;
using Catalog.Abstraction.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Core.Aggregates
{
    public class ProductAggregate : BaseAggregate<ProductEntity>
    {
        public ProductAggregate(ProductEntity entity) : base(entity)
        {

        }

     
        public void SaveProduct(Product product)
        {
            PopulateEntity(product);
        }


        public void DeleteProduct()
        {
            Entity.IsDeleted = true;
        }


    
        public void ValidateProduct(Product product)
        {
            if (string.IsNullOrEmpty(product.Name))
            {
                AddMessage("Product name is required");
            }
        }

       
        private void PopulateEntity(Product product)
        {
            Entity.Id = product.Id;
            Entity.Name = product.Name;
            Entity.Description = product.Description;
            Entity.Category = product.Category;
            //Entity.ImageFile = product.ImageFile;
            Entity.Price = product.Price;
            Entity.Summary = product.Summary;
            
        }
    }
}

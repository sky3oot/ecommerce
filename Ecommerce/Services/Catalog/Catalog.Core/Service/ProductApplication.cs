using Catalog.Abstraction.Entities;
using Catalog.Abstraction.Models;
using Catalog.Abstraction.Repository;
using Catalog.Abstraction.Services;
using Catalog.Core.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Core.Service
{
    public class ProductApplication : IProductApplication
    {
      
        private readonly IProductRepository _repository;

        public ProductApplication( IProductRepository repository)
        {
           
            _repository = repository;
        }



        public async Task CreateProduct(Product product)
        {
         
            var aggregate = new ProductAggregate(new ProductEntity());
            var result = new List<string>();
            aggregate.ValidateProduct(product);
            if (aggregate.ResultMessages.Count < 1)
            {

             
                aggregate.SaveProduct(product);
                await _repository.Save(aggregate.Entity);
            }
            else
            {
                result = aggregate.ResultMessages;
            }
          
        }

        public async Task<List<string>> DeleteProduct(string id)
        {
        
            var entity = await _repository.GetOne(id);
            var result = new List<string>();

            if (entity == null)
            {
                result.Add("Product not found");
                return result;
            }

            var aggregate = new ProductAggregate(entity);
            if (aggregate.ResultMessages.Count < 1)
            {
               
                aggregate.DeleteProduct();
                await _repository.Save(aggregate.Entity);
            }
            else
            {
                result = aggregate.ResultMessages;
            }
            return result;
        }

        public async Task<Product> GetProduct(string id)
        {
            var entity = await _repository.GetOne(id);
            var product = new Product(entity);
            return product;
        }

        public async Task<IEnumerable<Product>> GetProductByCategory(string categoryName)
        {
            var entities = await _repository.GetProductByCategory(categoryName);
            var products= new List<Product>();
            foreach (var entity in entities)
            {
                var product = new Product(entity);
                products.Add(product);
            }
            return products;
        }

        public async Task<IEnumerable<Product>> GetProductByName(string name)
        {
            var entities = await _repository.GetProductByName(name);
            var products = new List<Product>();
            foreach (var entity in entities)
            {
                var product = new Product(entity);
                products.Add(product);
            }
            return products;
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            var entities = await _repository.GetAll();

            var products = new List<Product>();
            foreach (var entity in entities)
            {
                var product = new Product(entity);
                products.Add(product);
            }
            return products;
        }

        public async Task<List<string>> UpdateProduct(Product product)
        {
        
            var entity = await _repository.GetOne(product.Id);
            var result = new List<string>();

            if (entity == null)
            {
                result.Add("Product not found");
                return result;
            }

            var aggregate = new ProductAggregate(entity);
            aggregate.ValidateProduct(product);
            if (aggregate.ResultMessages.Count < 1)
            {
            
                aggregate.SaveProduct(product);
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

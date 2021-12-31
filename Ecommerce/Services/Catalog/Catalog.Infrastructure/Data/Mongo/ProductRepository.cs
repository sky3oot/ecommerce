using Catalog.Abstraction;
using Catalog.Abstraction.Entities;
using Catalog.Abstraction.Models;
using Catalog.Abstraction.Repository;

using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Infrastructure.Data.Mongo
{
    public class ProductRepository : IProductRepository
    {
        private readonly MongoContext _context;

        public ProductRepository(IOptions<AppSettings> config)
        {
            _context = new MongoContext(config);
        }
        public async Task<IEnumerable<ProductEntity>> GetAll()
        {
            FilterDefinition<ProductEntity> filter = Builders<ProductEntity>.Filter.Ne(s => s.IsDeleted, true);
            List<ProductEntity> product = await _context.Products.Find(filter).ToListAsync();
            return product;
        }

        public async Task<ProductEntity> GetOne(string id)
        {
            var filter = Builders<ProductEntity>.Filter.Eq("_id", id);
            var product = (await _context.Products.FindAsync(filter)).FirstOrDefault();
            return product;
        }

        public async Task<IEnumerable<ProductEntity>> GetProductByCategory(string categoryName)
        {
            FilterDefinition<ProductEntity> filter = Builders<ProductEntity>.Filter.Eq(p => p.Category, categoryName);

            var categories =  await _context.Products.Find(filter).ToListAsync();
            return categories;
        }

        public async Task<IEnumerable<ProductEntity>> GetProductByName(string name)
        {
            FilterDefinition<ProductEntity> filter = Builders<ProductEntity>.Filter.Eq(p => p.Name, name);

            var products =  await _context
                            .Products
                            .Find(filter)
                            .ToListAsync();
            return products;
        }

        public async Task<string> Save(ProductEntity entity)
        {
            FilterDefinition<ProductEntity> filter = Builders<ProductEntity>.Filter.Eq("_id", entity.Id);
            var result = await _context.Products.FindAsync(filter);

            if (result.Any())
            {
                await _context.Products.ReplaceOneAsync(filter, entity);
            }
            else
            {
                await _context.Products.InsertOneAsync(entity);
            }

            return entity.Id;
        }
    }
}

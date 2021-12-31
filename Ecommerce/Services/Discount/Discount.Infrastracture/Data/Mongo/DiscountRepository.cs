using Discount.Abstraction;
using Discount.Abstraction.Model;
using Discount.Abstraction.Repositories;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discount.Infrastracture.Data.Mongo
{
    public class DiscountRepository : IDscountRepository
    {
        private readonly MongoContext _context;

        public DiscountRepository(IOptions<AppSettings> config)
        {
            _context = new MongoContext(config);
        }
        public async Task<IEnumerable<Coupon>> GetAll()
        {
            FilterDefinition<Coupon> filter = Builders<Coupon>.Filter.Ne(s => s.IsDeleted, true);
            List<Coupon> discount = await _context.Discounts.Find(filter).ToListAsync();
            return discount;
        }

        public async Task<IEnumerable<Coupon>> GetDiscountByProduct(string productName)
        {
            FilterDefinition<Coupon> filter = Builders<Coupon>.Filter.Eq(p => p.ProductName, productName);

            var discount = await _context.Discounts.Find(filter).ToListAsync();
            return discount;
        }

        public async Task<Coupon> GetOne(string id)
        {
            var filter = Builders<Coupon>.Filter.Eq("_id", id);
            var discount = (await _context.Discounts.FindAsync(filter)).FirstOrDefault();
            return discount;
        }

        public async Task<string> Save(Coupon entity)
        {
            FilterDefinition<Coupon> filter = Builders<Coupon>.Filter.Eq("_id", entity.ProductName);
            var result = await _context.Discounts.FindAsync(filter);

            if (result.Any())
            {
                await _context.Discounts.ReplaceOneAsync(filter, entity);
            }
            else
            {
                await _context.Discounts.InsertOneAsync(entity);
            }

            return entity.ProductName;
        }
    }
}

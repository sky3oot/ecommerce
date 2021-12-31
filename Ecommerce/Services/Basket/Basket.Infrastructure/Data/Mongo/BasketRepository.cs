using Basket.Abstraction;
using Basket.Abstraction.Entities;
using Basket.Abstraction.Models;
using Basket.Abstraction.Repositories;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basket.Infrastructure.Data.Mongo
{
  public  class BasketRepository : IBasketRepository
    {
        private readonly MongoContext _context;

        public BasketRepository(IOptions<AppSettings> config)
        {
            _context = new MongoContext(config);
        }
        public async Task<IEnumerable<ShoppingCartEntity>> GetAll()
        {
            FilterDefinition<ShoppingCartEntity> filter = Builders<ShoppingCartEntity>.Filter.Ne(s => s.IsDeleted, true);
            List<ShoppingCartEntity> basket = await _context.Baskets.Find(filter).ToListAsync();
            return basket;
        }

        public async Task<ShoppingCartEntity> GetOne(string userName)
        {
            var filter = Builders<ShoppingCartEntity>.Filter.Eq("userName", userName);
            var basket = (await _context.Baskets.FindAsync(filter)).FirstOrDefault();
            return basket;
        }

        public async Task<string> Save(ShoppingCartEntity entity)
        {
            FilterDefinition<ShoppingCartEntity> filter = Builders<ShoppingCartEntity>.Filter.Eq("_id", entity.UserName);
            var result = await _context.Baskets.FindAsync(filter);

            if (result.Any())
            {
                await _context.Baskets.ReplaceOneAsync(filter, entity);
            }
            else
            {
                await _context.Baskets.InsertOneAsync(entity);
            }

            return entity.UserName;
        }
    }
    }


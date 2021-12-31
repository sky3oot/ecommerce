using Microsoft.EntityFrameworkCore;
using Ordering.Application.Contracts.Persistence;
using Ordering.Domain.Common;
using Ordering.Domain.Entities;
using Ordering.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace Ordering.Infrastructure.Repositories
{
    public class RepositoryBase<T> : IAsyncRepository<Order> where T : EntityBase
    {
        protected readonly OrderContext _dbContext;

        public RepositoryBase(OrderContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }        

        public async Task<IReadOnlyList<Order>> GetAllAsync()
        {
            var orders = await _dbContext.Orders.FindAsync(Order => true);
            return orders.ToList();
        }

        public async Task<IReadOnlyList<Order>> GetAsync(Expression<Func<Order, bool>> predicate)
        {
            //if (predicate != null)
                return await _dbContext.Orders.AsQueryable().Where(predicate).ToListAsync();
            //var orders = await _dbContext.Orders.FindAsync(order => true);

            //return orders.ToList();
        }

        public async Task<IReadOnlyList<Order>> GetAsync(Expression<Func<Order, bool>> predicate = null, Func<IQueryable<Order>, IOrderedQueryable<Order>> orderBy = null, string includeString = null, bool disableTracking = true)
        {
            IQueryable<Order> query = _dbContext.Orders.AsQueryable();
            if (disableTracking) query = query.AsNoTracking();

            if (!string.IsNullOrWhiteSpace(includeString)) query = query.Include(includeString);

            if (predicate != null) query = query.Where(predicate);

            if (orderBy != null)
                return await orderBy(query).ToListAsync();
            return await query.ToListAsync();
        }

        public async Task<IReadOnlyList<Order>> GetAsync(Expression<Func<Order, bool>> predicate = null, Func<IQueryable<Order>, IOrderedQueryable<Order>> orderBy = null, List<Expression<Func<Order, object>>> includes = null, bool disableTracking = true)
        {
            IQueryable<Order> query = _dbContext.Orders.AsQueryable();
            if (disableTracking) query = query.AsNoTracking();

            if (includes != null) query = includes.Aggregate(query, (current, include) => current.Include(include));

            if (predicate != null) query = query.Where(predicate);

            if (orderBy != null)
                return await orderBy(query).ToListAsync();
            return await query.ToListAsync();
        }

        public virtual async Task<Order> GetByIdAsync(int id)
        {
            var filter = Builders<Order>.Filter.Eq("_id", id);
            return (await _dbContext.Orders.FindAsync(filter)).FirstOrDefault();
        }

        public async Task<Order> AddAsync(Order entity)
        {
           await _dbContext.Orders.InsertOneAsync(entity);
          
            return entity;
        }

        public async Task UpdateAsync(Order entity)
        {
            FilterDefinition<Order> filter = Builders<Order>.Filter.Eq("_id", entity.Id);
           await  _dbContext.Orders.ReplaceOneAsync(filter, entity);
            
        }

        public async Task DeleteAsync(Order entity)
        {
            FilterDefinition<Order> filter = Builders<Order>.Filter.Eq("_id", entity.Id);
         await   _dbContext.Orders.DeleteOneAsync(filter);
           
        }
    }
}

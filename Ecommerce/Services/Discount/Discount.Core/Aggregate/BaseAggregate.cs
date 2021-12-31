using Discount.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discount.Core.Aggregate
{
    public class BaseAggregate<T> where T : IEntity
    {
        public T Entity;
        public List<string> ResultMessages { get; }
        public BaseAggregate(T entity)
        {
            this.Entity = entity;
            ResultMessages = new List<string>();
        }

        public void AddMessage(string msg)
        {
            this.ResultMessages.Add(msg);
        }

    }
}

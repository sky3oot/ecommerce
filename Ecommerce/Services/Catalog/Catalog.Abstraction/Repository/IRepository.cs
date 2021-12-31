using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Abstraction.Repository
{
    public interface IRepository<T, TId> where T : IEntity
    {
        Task<TId> Save(T entity);

        Task<IEnumerable<T>> GetAll();

        Task<T> GetOne(TId id);



    }
}

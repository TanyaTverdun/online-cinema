using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace onlineCinema.Application.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T?> GetByIdAsync(int id);

        Task<IEnumerable<T>> GetAllAsync(
            Expression<Func<T, bool>>? filter = null,
            string? includeProperties = null);

        Task AddAsync(T entity);

        void Remove(T entity);

        void RemoveRange(IEnumerable<T> entities);

        void Update(T entity);
    }
}

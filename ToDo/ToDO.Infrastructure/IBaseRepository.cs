using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ToDo.Infrastructure
{
    public interface IBaseRepository<T> where T : class
    {
        #region Methods

        //Task<List<T>> GetAllAsync(CancellationToken token);

        Task<T> GetAsync(CancellationToken token, Expression<Func<T, bool>> predicate);

        Task<T> AddAsync(CancellationToken token, T entity);

        Task DeleteAsync(CancellationToken token, int id);

        Task<T> UpdateAsync(CancellationToken token, T entity);

        Task<bool> AnyAsync(CancellationToken token, Expression<Func<T, bool>> predicate);

        //void Attach(T entity);
        //void Detach(T entity);

        //void SetModifiedStateWithNested(T entity);

        #endregion

        //IQueryable<T> Table { get; }
        //IQueryable<T> TableNoTracking { get; }
    }
}

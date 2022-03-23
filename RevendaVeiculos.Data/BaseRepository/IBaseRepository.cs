using RevendaVeiculos.Data.BaseRepository;
using RevendaVeiculos.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RevendaVeiculos.Data.BaseRepository
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        Task<T> GetByIdAsync(int? id);
        Task<bool> ExistsByIdAsync(int? id);
        Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> expression);
        Task<T> FirstOrDefaultAsNoTrackingAsync(Expression<Func<T, bool>> expression, string include);
        Task<T> FirstOrDefaultAsNoTrackingAsync(Expression<Func<T, bool>> expression);
        Task<bool> Any(Expression<Func<T, bool>> expression);
        Task<List<T>> ToListAsync(Expression<Func<T, bool>> expression);
        Task<IEnumerable<T>> ListAsync(Expression<Func<T, bool>> expression, string include);
        Task<IEnumerable<T>> ListAsync(Expression<Func<T, bool>> expression);
        Task<PagedQuery<T>> ListPagedAsync<TKey>(Expression<Func<T, bool>> expression,
                                                  Expression<Func<T, TKey>> orderByExpression,
                                                  int page, int pageSize);
        Task<PagedQuery<T>> ListPagedAsync<TKey>(Expression<Func<T, TKey>> orderByExpression,
                                                    int page, int pageSize);
        Task<IEnumerable<T>> ListAsync();
        Task<IQueryable<T>> Where(Expression<Func<T, bool>> expression);
        Task<T> AddAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task<IEnumerable<T>> AddAllAsync(IEnumerable<T> entities);
        Task<IEnumerable<T>> UpdateAllAsync(IEnumerable<T> entities);
        Task DeleteAsync(T entity);
        Task DeleteAsync(IEnumerable<T> entity);
    }
}

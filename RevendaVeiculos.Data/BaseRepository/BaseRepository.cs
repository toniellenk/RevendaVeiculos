using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using RevendaVeiculos.Data.Entities;
using RevendaVeiculos.Data.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RevendaVeiculos.Data.BaseRepository
{
    public class BaseRepository<T> : UnitOfWork, IBaseRepository<T> where T : BaseEntity
    {


        public BaseRepository(RevendaVeiculosContext context) : base(context)
        {

        }

        public async Task<T> GetByIdAsync(int? id)
            => await _context.Set<T>().SingleOrDefaultAsync(e => e.Id == id);

        public async Task<bool> ExistsByIdAsync(int? id) => await _context.Set<T>().AnyAsync(e => e.Id == id);

        public async Task<PagedQuery<T>> ListPagedAsync<TKey>(Expression<Func<T, bool>> expression, Expression<Func<T, TKey>> orderByExpression, int page, int pageSize)
        {
            var result = await Task.Run(() => _context.Set<T>().AsNoTracking()
                                                               .Where(expression)
                                                               .OrderBy(orderByExpression)
                                                               .ToPagedQuery(page, pageSize));

            return result;
        }

        public async Task<PagedQuery<T>> ListPagedAsync<TKey>(Expression<Func<T, TKey>> orderByExpression, int page, int pageSize)
        {
            var result = await Task.Run(() => _context.Set<T>().AsNoTracking()
                                                               .OrderBy(orderByExpression)
                                                               .ToPagedQuery(page, pageSize));

            return result;
        }

        public async Task<IEnumerable<T>> ListAsync()
            => await _context.Set<T>().AsNoTracking().ToListAsync();

        public async Task<T> AddAsync(T entity)
        {
            entity.DataCriacao = DateTime.Now;
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<T> UpdateAsync(T entity)
        {
            _context.Set<T>().Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
        public async Task<IEnumerable<T>> UpdateAllAsync(IEnumerable<T> entities)
        {
            entities.ToList().ForEach(x => x.DataAtualizacao = DateTime.Now);
            await Task.Run(() => _context.UpdateRange(entities));
            return entities;
        }

        public async Task DeleteAsync(T entity)
        {
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> expression)
            => await _context.Set<T>().FirstOrDefaultAsync(expression);

        public async Task<T> FirstOrDefaultAsNoTrackingAsync(Expression<Func<T, bool>> expression)
           => await _context.Set<T>().AsNoTracking().FirstOrDefaultAsync(expression);

        public async Task<T> FirstOrDefaultAsNoTrackingAsync(Expression<Func<T, bool>> expression, string include)
            => await _context.Set<T>().AsNoTracking().Include(include).FirstOrDefaultAsync(expression);

        public async Task<IEnumerable<T>> ListAsync(Expression<Func<T, bool>> expression, string include)
            => await Task.Run(() => _context.Set<T>().Include(include).Where(expression));

        public async Task<IEnumerable<T>> ListAsync(Expression<Func<T, bool>> expression)
            => await Task.Run(() => _context.Set<T>().Where(expression));

        public async Task<List<T>> ToListAsync(Expression<Func<T, bool>> expression)
            => await _context.Set<T>().Where(expression).ToListAsync();

        public async Task<IQueryable<T>> Where(Expression<Func<T, bool>> expression)
            => await Task.Run(() => _context.Set<T>().Where(expression));

        public async Task<IEnumerable<T>> AddAllAsync(IEnumerable<T> entities)
        {
            await _context.Set<T>().AddRangeAsync(entities);
            return entities;
        }

        public async Task<bool> Any(Expression<Func<T, bool>> expression)
            => await _context.Set<T>().AnyAsync(expression);

        public async Task DeleteAsync(IEnumerable<T> entity)
        {
            await Task.Run(() => _context.Set<T>().RemoveRange(entity));
        }
    }
}

using Microsoft.EntityFrameworkCore;
using RevendaVeiculos.Data.BaseRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevendaVeiculos.Data.Extensions
{
    public static class QueryableExtesions
    {
        public static PagedQuery<T> ToPagedQuery<T>(this IOrderedQueryable<T> query, int page, int pageSize)
            where T : class
        {
            var result = new PagedQuery<T>
            {
                CurrentPage = page,
                PageSize = pageSize,
                Total = query.CountAsync().Result
            };


            var skip = (page - 1) * pageSize;
            result.Data = query.Skip(skip).Take(pageSize).ToListAsync().Result;
            result.LineCounts = result.Data.Count();

            return result;
        }
    }

    public static class EnumerableExtesions
    {
        public static PagedQuery<T> ToPagedQuery<T>(this IEnumerable<T> query, int page, int pageSize)
            where T : class
        {
            var result = new PagedQuery<T>
            {
                CurrentPage = page,
                PageSize = pageSize,
                Total = query.Count()
            };


            var skip = (page - 1) * pageSize;
            result.Data = query.Skip(skip).Take(pageSize).ToList();
            result.LineCounts = result.Data.Count();

            return result;
        }
    }
}

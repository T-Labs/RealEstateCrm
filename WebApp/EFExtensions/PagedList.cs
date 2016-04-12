using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace WebApp
{
    public static class PagedList
    {
        public static IQueryable<T> PagedResult<T, TResult>(this IQueryable<T> query, int pageNum, int pageSize,
                Expression<Func<T, TResult>> orderByProperty, bool isAscendingOrder, out int rowsCount, out int totalPages)
        {
            if (pageSize <= 0) pageSize = 20;

            rowsCount = query.Count();
            totalPages = (int)Math.Ceiling(rowsCount / (double)pageSize * 1.0);
            if (rowsCount <= pageSize || pageNum <= 0) pageNum = 1;

            int excludedRows = (pageNum - 1) * pageSize;

            query = isAscendingOrder ? query.OrderBy(orderByProperty) : query.OrderByDescending(orderByProperty);

            return query.Skip(excludedRows).Take(pageSize);
        }
    }
}

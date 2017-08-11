using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApp.Data;

namespace WebApp
{
    public static class PagedList
    {
        [Obsolete]
        public static IQueryable<T> PagedResult<T, TResult>(this IQueryable<T> query, int pageNum, int pageSize,
            Expression<Func<T, TResult>> orderByProperty, bool isAscendingOrder, out int rowsCount, out int totalPages)
        {
            if (pageSize <= 0) pageSize = 20;

            rowsCount = query.Count();
            totalPages = (int)Math.Ceiling(rowsCount / (double)pageSize * 1.0);
            if (rowsCount <= pageSize || pageNum <= 0) pageNum = 1;

            int excludedRows = (pageNum - 1) * pageSize;

            // query = isAscendingOrder ? query.OrderBy(orderByProperty) : query.OrderByDescending(orderByProperty);

            return query.Skip(excludedRows).Take(pageSize);
        }
        
        public static async Task<PagedResults<T>> PagedResultAsync<T>(this IQueryable<T> query, int pageNum, int pageSize)//Expression<Func<T, TResult>> orderByProperty, bool isAscendingOrder,
        {
            int totalItems;
            var items = query.PagedResult<T>(pageNum, pageSize, out totalItems);

            var pageInfo = new PageInfo
            {
                PageNumber = pageNum,
                PageSize = pageSize,
                TotalItems = totalItems
            };
            
            return new PagedResults<T>
            {
                PageInfo = pageInfo,
                Items = await items.ToListAsync()
            };
        }

        private static IQueryable<T> PagedResult<T>(this IQueryable<T> query, int pageNum, int pageSize, out int rowsCount)//Expression<Func<T, TResult>> orderByProperty, bool isAscendingOrder,
        {
            if (pageSize <= 0) pageSize = 20;

            rowsCount = query.Count();
            if (rowsCount <= pageSize || pageNum <= 0) pageNum = 1;

            int excludedRows = (pageNum - 1) * pageSize;
            return query.Skip(excludedRows).Take(pageSize);
        }

      /*  public static IEnumerable<T> PagedResult<T>(this IEnumerable<T> query, int pageNum, int pageSize, out PageInfo pageInfo)//Expression<Func<T, TResult>> orderByProperty, bool isAscendingOrder,
        {

            int totalItems;
            int totalPages;
            var items = query.PagedResult<T>(pageNum, pageSize, out totalItems, out totalPages);

            pageInfo = new PageInfo
            {
                PageNumber = pageNum,
                PageSize = pageSize,
                TotalItems = totalItems
            };
            return items;
        }

        public static IEnumerable<T> PagedResult<T>(this IEnumerable<T> query, int pageNum, int pageSize, out int rowsCount, out int totalPages)//Expression<Func<T, TResult>> orderByProperty, bool isAscendingOrder,
        {
            if (pageSize <= 0) pageSize = 20;

            rowsCount = query.Count();
            totalPages = (int)Math.Ceiling(rowsCount / (double)pageSize * 1.0);
            if (rowsCount <= pageSize || pageNum <= 0) pageNum = 1;

            int excludedRows = (pageNum - 1) * pageSize;
            return query.Skip(excludedRows).Take(pageSize);
        }*/
    }
}

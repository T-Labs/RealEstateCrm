using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.Entity;
using WebApp.Entities;
using System.Linq.Expressions;

namespace WebApp
{
    public static class HousingExtensions
    {
        public class FilterData
        {
            public int? Page { get; set; }
            public int[] HouseTypeId { get; set; } = new int[] { };
            public int? CityId { get; set; }
            public int? PriceFrom { get; set; }
            public int? PriceTo { get; set; }

            public bool? IsArchived { get; set; }
        }

        public static Housing GetFullById(this DbSet<Housing> housings, int id)
        {
            return housings.Include(x => x.Phones)
                .Include(x => x.City)
                .Include(x => x.District)
                .Include(x => x.Calls)
                .Single(m => m.Id == id);
        }

        public static IQueryable<Housing> IncludeAll(this DbSet<Housing> housing)
        {
            return housing.Include(x => x.City)
                .Include(x => x.Street)
                .Include(x => x.District)
                .Include(x => x.Phones)
                .Include(x => x.TypesHousing)
                .Include(x => x.User);
        }


        public static Func<Housing, bool> Filter(FilterData filter)
        {
            Func<Housing, bool> func = (x) =>
            {
                bool result = true;
                if (filter.CityId.HasValue)
                {
                    result &= x.CityId == filter.CityId.Value;
                }

                if (filter.HouseTypeId.Length > 0)
                {
                    result &= filter.HouseTypeId.Contains(x.TypesHousingId);
                }

                if (filter.PriceFrom.HasValue && filter.PriceTo.HasValue)
                {
                    result &= x.Sum >= filter.PriceFrom.Value && x.Sum <= filter.PriceTo.Value;
                }
                else if (filter.PriceFrom.HasValue)
                {
                    result &= x.Sum >= filter.PriceFrom.Value;
                }
                else if (filter.PriceTo.HasValue)
                {
                    result &= x.Sum <= filter.PriceTo.Value;
                }

                if (filter.IsArchived.HasValue)
                {
                    result &= x.IsArchive == filter.IsArchived.Value;
                }
                return result;
            };

            return func;
        }
        
        public static List<Housing> GetPage(this IQueryable<Housing> query, int page, out int totalItems, out int totalPages)
        {
            const int pageSize = 10;
            totalItems = query.Count();
            totalPages = (int)Math.Ceiling(totalItems / (double)pageSize * 1.0);
            if (totalItems == 0)
            {
                return new List<Housing>();
            }

            if (page > totalPages)
            {
                throw new ArgumentOutOfRangeException(nameof(page));
            }
            int start = (int)((page - 1) * pageSize);
            query = query.Skip(start).Take(pageSize);

            var result = query
                .Include(x => x.City)
                .Include(x => x.Street)
                .Include(x => x.District)
                .Include(x => x.Phones)
                .Include(x => x.TypesHousing)
                .Include(x => x.User)
                .Include(x => x.Calls);//.ThenInclude(x => x.Select(c => c.User));


            return result.ToList();
        }
        
    }
}

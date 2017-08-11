using System;
using System.Collections.Generic;
using System.Linq;

using WebApp.Entities;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace WebApp
{
    public static class HousingExtensions
    {
        [Obsolete]
        public class FilterParams
        {
            public int? Page { get; set; }
            public int[] HouseTypeId { get; set; } = new int[] { };
            public int? CityId { get; set; }
            public int? PriceFrom { get; set; }
            public int? PriceTo { get; set; }
            public bool? IsArchived { get; set; }
            public int[] DistrictId { get; set; } = new int[] { };
        }

        public static Housing GetFullById(this IQueryable<Housing> housings, int id)
        {
            return housings.IncludeAll().Single(m => m.Id == id);
        }

        public static IQueryable<Housing> IncludeAll(this IQueryable<Housing> housing)
        {
            return housing.Include(x => x.City)
                .Include(x => x.Street)
                .Include(x => x.District)
                .Include(x => x.Phones)
                .Include(x => x.TypesHousing)
                .Include(x => x.Calls)
                .Include(x => x.User);
        }

        [Obsolete]
        public static Func<Housing, bool> Filter(FilterParams filter)
        {
            Func<Housing, bool> func = (x) =>
            {
                bool result = true;
                if (filter.CityId.HasValue)
                {
                    result &= x.CityId == filter.CityId.Value;
                }

                if (filter.DistrictId.Length > 0)
                {
                    result &= filter.DistrictId.Contains(x.DistrictId);
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
                else
                {
                    result &= x.IsArchive == false;
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

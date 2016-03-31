using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.Entity;
using WebApp.Entities;

namespace WebApp
{
    public static class HousingExtensions
    {
        public static Housing GetById(this DbSet<Housing> housings, int id)
        {
            return housings.Include(x => x.Phones)
                .Include(x => x.City)
                .Include(x => x.District)
                .Include(x => x.Calls)
                .Single(m => m.Id == id);
        }

        public static IQueryable<Housing> AddCityFilter(this IQueryable<Housing> query, int? cityId)
        {
            if (cityId.HasValue)
            {
                query = query.Where(x => x.CityId == cityId.Value);
            }
            return query;
        }

        public static IQueryable<Housing> AddDistrictFilter(this IQueryable<Housing> query, int? districtId)
        {
            if (districtId.HasValue)
            {
                query = query.Where(x => x.DistrictId == districtId.Value);
            }
            return query;
        }

        public static IQueryable<Housing> AddHousingTypeFilter(this IQueryable<Housing> query, int? houseTypeId)
        {
            if (houseTypeId.HasValue)
            {
                query = query.Where(x => x.TypesHousing.Id == houseTypeId.Value);
            }
            return query;
        }

        public static IQueryable<Housing> AddHousingTypeFilter(this IQueryable<Housing> query, int[] houseTypeId)
        {
            if (houseTypeId.Length > 0)
            {
                query = query.Where(x => houseTypeId.Contains(x.TypesHousing.Id));
            }
            return query;
        }


        public static IQueryable<Housing> AddCostFilter(this IQueryable<Housing> query, int? priceFrom, int? priceTo)
        {
            if (priceFrom.HasValue)
            {
                query = query.Where(x => x.Sum >= priceFrom.Value);
            }

            if (priceTo.HasValue)
            {
                query = query.Where(x => x.Sum <= priceTo.Value);
            }
            return query;
        }


        public static IQueryable<Housing> AddIsArchiveFilter(this IQueryable<Housing> query, bool? isArchive)
        {
            if (isArchive.HasValue)
            {
                query = query.Where(x => x.IsArchive == isArchive.Value);
            }
            return query;
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
                .Include(x => x.Calls);

            return result.ToList();
        }

        /*public static List<Housing> GetByFilters(this DbSet<Housing> housings, int page, int?[] houseTypeId, int? cityId, int? districtId, int? priceFrom, int? priceTo, bool? isArchive, int? objectId)
        {
            IQueryable<Housing> query = housings
                                .AddIsArchiveFilter(isArchive)
                                .AddCityFilter(cityId)
                                .AddDistrictFilter(districtId)
                                .AddHousingTypeFilter(houseTypeId)
                                .AddCostFilter(priceFrom, priceTo);

            return query.GetPage(page);
        }*/
    }
}

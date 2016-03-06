using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using WebApp.Entities;
using WebApp.Models;
using Microsoft.Data.Entity;

namespace WebApp.DAL
{
    public class BuildingRepository : BaseRepository
    {

        public BuildingRepository([FromServices] ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public List<Housing> GetBuildings(int? page, int[] houseTypeId, int? cityId, int? priceFrom, int? priceTo)
        {
            var housings = DbContext.Objects;
            IQueryable<Housing> query = housings;
            
            if (houseTypeId.Length > 0)
            {
                query = query.Where(x => houseTypeId.Contains(x.TypesHousing.Id));
            }

            if (cityId.HasValue)
            {
                query = query.Where(x => x.CityId == cityId.Value);
            }

            if (priceFrom.HasValue)
            {
                query = query.Where(x => x.Sum >= priceFrom.Value);
            }

            if (priceTo.HasValue)
            {
                query = query.Where(x => x.Sum <= priceTo.Value);
            }

            const int pageSize = 10;
            int totalPages = query.Count() / pageSize;
            if (page.HasValue)
            {
                if (page > totalPages)
                {
                    throw new ArgumentOutOfRangeException(nameof(page));
                }
                int start = page.Value * pageSize;
                query = query.Skip(start).Take(pageSize);
            }

            var result = query
                .Include(x => x.City)
                .Include(x => x.Street)
                .Include(x => x.District)
                .Include(x => x.Phones)
                .Include(x => x.TypesHousing);

            return result.ToList();
        }
    }
}

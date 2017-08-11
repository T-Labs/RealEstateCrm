using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using WebApp.Entities;
using WebApp.Models;

namespace WebApp.Services
{
    //https://docs.asp.net/en/latest/performance/caching/memory.html

    public class AddressService
    {
        private readonly ApplicationDbContext DbContext;
        private readonly IMemoryCache Cache;
        private readonly ILogger<AddressService> _logger;

        public AddressService([FromServices]ApplicationDbContext dbContext, IMemoryCache cache, ILogger<AddressService> logger)
        {
            DbContext = dbContext;
            Cache = cache;
            _logger = logger;
        }

        public List<City> CachedCityList()
        {
            List<City> list;
            if (!Cache.TryGetValue(CacheKeys.City, out list))
            {
                list = DbContext.Cities.OrderBy(x => x.Name).ToList();

                Cache.Set(CacheKeys.City, list, new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(5)));
                _logger.LogInformation($"{CacheKeys.City} updated from source.");
            }

            return list;
        }

        public List<District> CachedDistrictList(int? cityId=null)
        {
            List<District> list;
            if (!Cache.TryGetValue(CacheKeys.District, out list))
            {
                IQueryable<District> query = DbContext.Districts;
                if (cityId.HasValue)
                {
                    query = query.Where(x => x.CityId == cityId.Value);
                }
                list = query.OrderBy(x => x.Name).ToList();

                Cache.Set(CacheKeys.District, list, new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(5)));
                _logger.LogInformation($"{CacheKeys.District} updated from source.");
            }

            return list;
        }


        public List<TypesHousing> CachedHousingTypeList(int? cityId = null)
        {
            List<TypesHousing> list;
            if (!Cache.TryGetValue(CacheKeys.HousingType, out list))
            {
                IQueryable<TypesHousing> query = DbContext.TypesHousing;
                
                list = query.OrderBy(x => x.Name).ToList();

                Cache.Set(CacheKeys.HousingType, list, new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(5)));
                _logger.LogInformation($"{CacheKeys.HousingType} updated from source.");
            }

            return list;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Entities;

namespace WebApp.DAL
{
    public class BuildingRepository
    {
        public List<Housing> GetBuildings(int? page, int[] houseTypeId, int? cityId, int? priceFrom, int? priceTo)
        {
            var list = new List<Housing>();
            for (int i = 0; i < 50; i++)
            {
                list.Add(CreateRadnom());
            }
            
            if (houseTypeId.Length > 0)
            {
                list = list.Where(x => houseTypeId.Contains(x.TypesHousing.Id)).ToList();
            }

            if (cityId.HasValue)
            {
                list = list.Where(x => x.CityId == cityId.Value).ToList();
            }

            if (priceFrom.HasValue)
            {
                list = list.Where(x => x.Sum >= priceFrom.Value).ToList();
            }

            if (priceTo.HasValue)
            {
                list = list.Where(x => x.Sum <= priceTo.Value).ToList();
            }

            const int pageSize = 10;
            int totalPages = list.Count / pageSize;
            if (page.HasValue)
            {
                if (page > totalPages)
                {
                    throw new ArgumentOutOfRangeException(nameof(page));
                }
                int start = page.Value * pageSize;
                list = list.GetRange(start, pageSize);
            }

            return list;
        }

        static Random random = new Random();
        private Housing CreateRadnom()
        {
            
           
            var houseType = MockData.GetRandomHouseType();
            var city = MockData.GetRandomCity();
            var district = MockData.GetRandomDistrict();
            var street = MockData.GetRandomStreet();

            var sum = random.Next(5, 30) * 1000;
            Housing item = new Housing()
            {
                Id = random.Next(10000),
                CityId = city.Id,
                City = city,
                Street = street,
                StreetId = street.Id,
                District = district,
                DistrictId = district.Id,
                TypesHousing = houseType,
                Comment = MockData.GetRandomDescription(),
                Sum = sum,
                Phones = new List<HousingPhone> { new HousingPhone { Number = "+123456789" } }
            };
            return item;
        }
    }
}

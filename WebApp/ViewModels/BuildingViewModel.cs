using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Entities;

namespace WebApp.ViewModels
{
    public class BuildingViewModel
    {
        public int RentId { get; private set; }

        public string HouseType { get; private set; }
        public int HouseTypeId { get; private set; }

        public int Price { get; private set; }

        public string District { get; private set; }

        public string Street { get; private set; }

        public string Description { get; private set; }

        public string Phone { get; private set; }

        public string CityName { get; private set; }

        public int CityId { get; private set; }

        public int DistrictId { get; private set; }

        private BuildingViewModel()
        {
        }

        public static BuildingViewModel Create()
        {
            var random = new Random();
            var houseType = MockData.GetRandomHouseType();
            var city = MockData.GetRandomCity();

            var district = MockData.GetRandomDistrict();
            var street = MockData.GetRandomStreet();
            var model = new BuildingViewModel()
            {
                RentId = random.Next(1000, 100000),
                Description = MockData.GetRandomDescription(),
                District = district.Name,
                DistrictId = district.Id,
                HouseType = houseType.Name,
                HouseTypeId = houseType.Id,
                Price = random.Next(5000, 30000),
                Street = street.Name,
                Phone = "+123456789",
                CityId = city.Id,
                CityName = city.Name
            };

            return model;
        }

        public static BuildingViewModel Create(Housing building)
        {
            var model = new BuildingViewModel()
            {
                Street = building.Street.Name,
                District = building.District.Name,
                DistrictId = building.DistrictId,
                CityId = building.CityId,
                Phone = building.Phones[0].Number,
                HouseTypeId = building.TypesHousing.Id,
                HouseType = building.TypesHousing.Name,
                Price = (int)building.Sum,
                Description = building.Comment,
                RentId = building.Id,
                CityName = building.City.Name
            };

            return model;
        }
    }
}

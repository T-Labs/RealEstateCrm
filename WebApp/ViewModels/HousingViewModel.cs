using System.Linq;
using RealEstateCrm.Entities;

namespace RealEstateCrm.ViewModels
{
    public class HousingViewModel
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

        private HousingViewModel()
        {
        }

        public static HousingViewModel Create(Housing building, bool isAuth)
        {
            var model = new HousingViewModel()
            {
                Street = building.Street.Name,
                District = building.District.Name,
                DistrictId = building.DistrictId,
                CityId = building.CityId,
                Phone = isAuth ? building.Phones.FirstOrDefault()?.Number ?? string.Empty : string.Empty,
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

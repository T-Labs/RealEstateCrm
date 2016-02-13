using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.ViewModels
{
    public class RentViewModel
    {
        public int RentId { get; private set; }

        public string HouseType { get; private set; }
        public int HouseTypeId { get; private set; }

        public int Price { get; private set; }

        public string District { get; private set; }

        public string Street { get; private set; }

        public string Description { get; private set; }

        public string Phone { get; private set; }

        private RentViewModel()
        {
        }

        public static RentViewModel Create()
        {
            var random = new Random();
            var houseType = MockData.GetRandomHouseType();
            var model = new RentViewModel()
            {
                RentId = random.Next(1000, 100000),
                Description = MockData.GetRandomDescription(),
                District = MockData.GetRandomDistrict(),
                HouseType = houseType.Name,
                HouseTypeId = houseType.Id,
                Price = random.Next(5000, 30000),
                Street = MockData.GetRandomStreet(),
                Phone = "+123456789"
            };

            return model;
        }
    }
}

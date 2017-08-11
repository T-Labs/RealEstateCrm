using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Entities;
using WebApp.ViewModels;

namespace Web.Helpers
{
    public class HousindViewModelBuilder: IModelBuilder<HousingViewModel>
    {
        public HousingViewModel Model { get; } = new HousingViewModel();

        public Task<HousindViewModelBuilder> BuildAsync(int id)
        {
            throw new NotImplementedException();
        }

        public HousindViewModelBuilder Build(Housing building)
        {
            Model.Street = building.Street.Name;
            Model.District = building.District.Name;
            Model.DistrictId = building.DistrictId;
            Model.CityId = building.CityId;
            Model.Phone = building.Phones.FirstOrDefault()?.Number;
            Model.HouseTypeId = building.TypesHousing.Id;
            Model.HouseType = building.TypesHousing.Name;
            Model.Price = (int) building.Sum;
            Model.Description = building.Comment;
            Model.RentId = building.Id;
            Model.CityName = building.City.Name;
            
            return this;
        }

        public HousindViewModelBuilder ShowMobilePhones(bool show)
        {
            Model.ShowMobilePhone = show;
            return this;
        }
    }
}

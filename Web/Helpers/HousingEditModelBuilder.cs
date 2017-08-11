using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data;
using Data.Query;
using WebApp.Entities;
using WebApp.ViewModels;

namespace Web.Helpers
{
    public class HousingEditModelBuilder: IModelBuilder<Housing, HousingEditModel>
    {
        private ReadOnlyDataContext ReadOnlyDataContext { get; }
        private IQueryDispatcher QueryDispatcher { get; }
        public HousingEditModel Model { get; }
        
        public HousingEditModelBuilder(ReadOnlyDataContext readOnlyDataContext, IQueryDispatcher queryDispatcher)
        {
            ReadOnlyDataContext = readOnlyDataContext;
            QueryDispatcher = queryDispatcher;
        }

        public HousingEditModel Build(Housing housing)
        {
            var model = Create(housing);
            return model;
        }
        
        public async Task<HousingEditModel> BuildAsync(int id)
        {
            var housing = await QueryDispatcher.ExecuteAsync<HousingFullByIdQuery, Housing>(new HousingFullByIdQuery(id));
            var model = Create(housing);
            return model;
        }
        
        private static HousingEditModel Create(Housing housing)
        {
            var item = new HousingEditModel
            {
                EditId = housing.Id,
                Comment = housing.Comment,
                FirstName = housing.FirstName,
                LastName = housing.LastName,
                MidleName = housing.MidleName,
                Cost = housing.Sum,
                EndDate = housing.EndDate,
                Phone1 = housing.Phones.SingleOrDefault(x => x.Order == 0)?.Number,
                Phone2 = housing.Phones.SingleOrDefault(x => x.Order == 1)?.Number,
                Phone3 = housing.Phones.SingleOrDefault(x => x.Order == 2)?.Number,
                HouseNumber = housing?.House,
                HouseBuilding = housing?.Building,
                Room = housing?.Room,
                IsArchived = housing.IsArchive,
                IsPartnerShip = housing.PartherShip,
                HouseType = housing.TypesHousing?.Name ?? "Не указано",
                HouseTypeId = housing.TypesHousingId,
                CityId = housing.CityId,
                DistrictId = housing.DistrictId,
                StreetId = housing.StreetId,
                Calls = housing.Calls.Select(HousingCallViewModel.Create).ToList()
            };

            var addressParts = new List<string>();
            if (housing.City != null)
            {
                addressParts.Add(housing.City.Name);
            }

            if (housing.District != null)
            {
                addressParts.Add(housing.District.Name);
            }

            if (housing.Street != null)
            {
                addressParts.Add(housing.Street.Name);
            }

            addressParts.Add(housing.House);
            addressParts.Add(housing.Building);
            addressParts.Add(housing.Room);

            item.FullAddress = addressParts.Where(x => !string.IsNullOrEmpty(x)).Aggregate("", (x, y) => x + ", " + y).Trim(',');

            return item;
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc.Rendering;
using WebApp.Entities;

namespace WebApp.ViewModels
{
    public class HousingEditModel
    {
        public int EditId { get; private set; }
        
        [Display(Name = "Фамилия")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Имя")]
        public string MidleName { get; set; }
        
        [Display(Name = "Отчество")]
        public string LastName { get; set; }

        [Display(Name = "Описание")]
        public string Comment { get; set; }

        [Display(Name = "Цена")]
        public double Cost { get; set; }
        
        public AddressSelectionModel Address { get; set; }

        [Display(Name = "Телефон для связи")]
        public string[] Phones { get; set; }

        [Display(Name =  "Тип жилья")]
        public DropDownViewModel HouseType { get; set; }

        public HousingEditModel()
        {
            Phones = new string[3];
        }

        public static HousingEditModel Create(Housing housing, List<City> allCities, List<Street> allStreets, List<TypesHousing> typesHousings)
        {
            var item = new HousingEditModel
            {
                EditId = housing.Id,
                Comment = housing.Comment,
                FirstName = housing.FirstName,
                LastName = housing.LastName,
                MidleName = housing.MidleName,
                Cost = housing.Sum,
                Address = new AddressSelectionModel(allCities, allStreets, housing),
                HouseType = new DropDownViewModel
                {
                    Id = housing.TypesHousingId,
                    Items = typesHousings.Select(x => new SelectListItem {Value = x.Id.ToString(), Text = x.Name })
                }
            };

            return item;
        }


        public void UpdateEntity(Housing item)
        {
            item.FirstName = FirstName;
            item.MidleName = MidleName;
            item.LastName = LastName;
            item.Sum = Cost;
            item.Comment = Comment;
            item.CityId = Address.City.Id;
            item.DistrictId = Address.District.Id;
            item.StreetId = Address.Street.Id;
            item.House = Address.HouseNumber;
            item.Building = Address.HouseBuilding;
            item.Room = Address.Room;
            item.TypesHousingId = HouseType.Id;

        }
    }
}

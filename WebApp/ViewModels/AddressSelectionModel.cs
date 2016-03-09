using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc.Rendering;
using WebApp.Entities;

namespace WebApp.ViewModels
{
    public class AddressSelectionModel
    {
        [Required]
        [Display(Name="Город")]
        public DropDownViewModel City { get; set; }

        [Required]
        [Display(Name = "Район")]
        public DropDownViewModel District { get; set; }

        [Required]
        [Display(Name = "Улица")]
        public DropDownViewModel Street { get; set; }

        [Required]
        [Display(Name = "Номер дома")]
        public string HouseNumber { get; set; }

        [Display(Name="Строение")]
        public string HouseBuilding { get; set; }

        [Display(Name = "Номер квартиры")]
        public string Room { get; set; }


        public AddressSelectionModel()
        {
        }

        public AddressSelectionModel(List<City> allCities, List<Street> allStreets, Housing housing)
        {
            City = new DropDownViewModel()
            {
                Id = housing.CityId,
                Items = allCities.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name })
            };

            District = new DropDownViewModel()
            {
                Id = housing.DistrictId,
                Items = housing.City?.Districts?.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name }) ?? new List<SelectListItem>()
            };

            Street = new DropDownViewModel()
            {
                Id = housing.StreetId,
                Items = allStreets.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name })
            };

            HouseNumber = housing.House;
            HouseBuilding = housing.Building;
            Room = housing.Room;
        }
    }
}

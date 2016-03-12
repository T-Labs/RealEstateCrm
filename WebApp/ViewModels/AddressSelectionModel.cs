using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.AspNet.Mvc.Rendering;
using RealEstateCrm.Entities;
using WebApp.ViewModels;

namespace RealEstateCrm.ViewModels
{
    public class AddressSelectionModel
    {
        [UIHint("dropdown")]
        [Required]
        [Display(Name="Город")]
        public DropDownViewModel City { get; set; }

        [UIHint("dropdown")]
        [Required]
        [Display(Name = "Район")]
        public DropDownViewModel District { get; set; }

        [UIHint("dropdown")]
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

        public AddressSelectionModel(Housing housing, List<SelectListItem> allCities, List<SelectListItem> allStreets)
        {
            City = new DropDownViewModel()
            {
                Id = housing?.CityId ?? 0,
                Items = allCities
            };

            District = new DropDownViewModel()
            {
                Id = housing?.DistrictId ?? 0,
                Items = housing?.City?.Districts?.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name }) ?? new List<SelectListItem>()
            };

            Street = new DropDownViewModel()
            {
                Id = housing?.StreetId ?? 0,
                Items = allStreets
            };

            HouseNumber = housing?.House;
            HouseBuilding = housing?.Building;
            Room = housing?.Room;
        }
    }
}

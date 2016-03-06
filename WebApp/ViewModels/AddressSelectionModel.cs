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
        public IEnumerable<SelectListItem> AllCities { get; set; }
        public IEnumerable<SelectListItem> Districts { get; set; }

        [Display(Name = "Город")]
        public int CityId { get; set; }

        [Display(Name = "Район")]
        public int DistrictId{get; set; }

        [Display(Name="Улица")]
        public int StreetId { get; set; }
        
        public AddressSelectionModel()
        {
        }

        public AddressSelectionModel(List<City> allCities, City selectedCity)
        {
            AllCities = allCities.Select(x => new SelectListItem{ Value = x.Id.ToString(), Text = x.Name });
            Districts = selectedCity.Districts?.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name });
            CityId = selectedCity.Id;
        }
    }
}

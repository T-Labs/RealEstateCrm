using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc.Rendering;
using WebApp.Entities;

namespace WebApp.ViewModels
{
    public class StreetViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Название улицы")]
        [UIHint("string")]
        public string Name { get; set; }

        [Display(Name = "Город")]
        [UIHint("dropdown")]

        public DropDownViewModel City { get; set; }


        public static StreetViewModel Create(Street street, IEnumerable<SelectListItem> cityList)
        {
            var item = new StreetViewModel
            {
                Id = street.Id,
                Name = street.Name,
                City = new DropDownViewModel(street.CityId, cityList)
            };

            return item;
        }
    }
}

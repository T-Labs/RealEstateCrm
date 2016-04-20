using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc.Rendering;
using WebApp.Entities;

namespace WebApp.ViewModels
{
    public class StreetItemViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Название улицы")]
        [UIHint("string")]
        public string Name { get; set; }

        [Display(Name = "Город")]
        [UIHint("city-selector")]
        public int CityId { get; set; }
        
        public static StreetItemViewModel Create(Street street)
        {
            var item = new StreetItemViewModel
            {
                Id = street.Id,
                Name = street.Name,
                CityId = street.CityId
            };

            return item;
        }
    }
}

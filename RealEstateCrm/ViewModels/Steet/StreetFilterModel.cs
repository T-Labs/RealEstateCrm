using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.ViewModels
{
    public class StreetFilterModel
    {
        [Display(Name = "Название улицы")]
        [UIHint("string")]
        public string Name { get; set; }

        [Display(Name = "Город")]
        [UIHint("city-selector")]
        public int CityId { get; set; }
    }
}

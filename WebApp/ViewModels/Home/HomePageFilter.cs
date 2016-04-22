using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc.Rendering;
using Newtonsoft.Json;

namespace WebApp.ViewModels.Home
{
    public class HomePageFilter
    {
        [UIHint("dropdown-multiple")]
        [Display(Name = "Вид жилья")]
        public List<SelectListItem> HousingTypeList { get; set; }

       
        [Display(Name = "Город")]
        public int CityId { get; set; }

        [UIHint("dropdown")]
        [Display(Name = "Район")]
        public int DistrictId { get; set; }

        [Display(Name = "Цена от")]
        public int? MinCost { get; set; }

        [Display(Name = "Цена до")]
        public int? MaxCost { get; set; }

    }
}

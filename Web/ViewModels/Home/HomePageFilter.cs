using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Data.Query;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace WebApp.ViewModels.Home
{
    public class HomePageFilter
    {        
        public List<int> HousingTypeListIds { get; set; } = new List<int>();

        public List<int> DistrictListIds { get; set; } = new List<int>();

        [Display(Name = "Город")]
        public int CityId { get; set; }
              

        [Display(Name = "Цена от")]
        public int? MinCost { get; set; }

        [Display(Name = "Цена до")]
        public int? MaxCost { get; set; }

        public HomePageFilter()
        {
        }

        public HomePageFilter(HousingPagedQuery param)
        {
            HousingTypeListIds = param.HouseTypeId.ToList();
            DistrictListIds = param.DistrictId.ToList();
            CityId = param.CityId ?? 0;
            MinCost = param.PriceFrom;
            MaxCost = param.PriceTo;
        }
    }
}

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;

using Newtonsoft.Json;
using WebApp.Models;

namespace WebApp.ViewModels
{
    public class HousingIndexModel
    {
        public List<HousingEditModel> Items { get; set; }

        public HousingIndexFilterModel Filters { get; set; }
        public PageInfo PageInfo { get; set; } = new PageInfo();
    }

    public class HousingIndexFilterModel
    {
        [UIHint("housiong-type-selector")]
        [Display(Name="Вид жилья")]
        public int HousingTypeId { get; set; }

        [UIHint("city-selector")]
        [Display(Name = "Город")]
        public int CityId { get; set; }


        [Display(Name = "Район")]
        public int DistrictId { get; set; }

        [Display(Name = "Цена от")]
        public int MinCost { get; set; }

        [Display(Name = "Цена до")]
        public int MaxCost { get; set; }

        [Display(Name = "ID объекта")]
        public int SelectedObjectId { get; set; }

        [Display(Name="Архивные записи")]
        public bool IsArchived { get; set; }

        public HousingIndexFilterModel()
        {
        }

    }
}

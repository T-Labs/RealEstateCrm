using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.Data.Entity;
using Newtonsoft.Json;
using WebApp.Models;

namespace WebApp.ViewModels
{
    public class HousingIndexModel
    {
        public IEnumerable<HousingEditModel> Items { get; set; }

        public HousingIndexFilterModel Filters { get; set; }

        public int TotalPages { get; set; }
        public int CurrentPage { get; set; }
    }

    public class HousingIndexFilterModel
    {
        [JsonProperty("ht")]
        [UIHint("dropdown")]
        [Display(Name="Вид жилья")]
        public DropDownViewModel HousingTypeList { get; set; }

        [JsonProperty("c")]
        [UIHint("dropdown")]
        [Display(Name = "Город")]
        public DropDownViewModel City { get; set; }

        [JsonProperty("d")]
        [UIHint("dropdown")]
        [Display(Name = "Район")]
        public DropDownViewModel District { get; set; }

        [JsonProperty("minc")]
        [Display(Name = "Цена от")]
        public int MinCost { get; set; }

        [JsonProperty("maxc")]
        [Display(Name = "Цена до")]
        public int MaxCost { get; set; }

        [JsonProperty("oid")]
        [Display(Name = "ID объекта")]
        public int SelectedObjectId { get; set; }

        [JsonProperty("a")]
        [Display(Name="Архивные записи")]
        public bool IsArchived { get; set; }

        public HousingIndexFilterModel()
        {
        }

    }
}

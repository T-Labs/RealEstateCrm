using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.ViewModels
{
    public class CustomerIndexModel
    {
        public List<CustomerEditModel> Items { get; set; } = new List<CustomerEditModel>();
        public CustomerIndexFilterModel Filters { get; set; } = new CustomerIndexFilterModel();

        public int TotalPages { get; set; }
        public int CurrentPage { get; set; }
    }


    public class CustomerIndexFilterModel
    {
        [JsonProperty(PropertyName = "h")]
        [UIHint("housiong-type-selector")]
        [Display(Name = "Вид жилья")]
        public int? HousingTypeId { get; set; }

        [JsonProperty(PropertyName = "c")]
        [UIHint("city-selector")]
        [Display(Name = "Город")]
        public int? CityId { get; set; }

        [JsonProperty(PropertyName = "d")]
        [Display(Name = "Район")]
        public int? DistrictId { get; set; }

        [JsonProperty(PropertyName = "min")]
        [Display(Name = "Цена от")]
        public int? MinCost { get; set; }
        
        [JsonProperty(PropertyName = "max")]
        [Display(Name = "Цена до")]
        public int? MaxCost { get; set; }

        [JsonProperty(PropertyName = "id")]
        [Display(Name = "ID объекта")]
        public int? SelectedObjectId { get; set; }

        [JsonProperty(PropertyName = "a")]
        [Display(Name = "Только с доступом на сайт")]
        public bool? IsSiteAccessOnly { get; set; }

        public CustomerIndexFilterModel()
        {
        }

    }

}

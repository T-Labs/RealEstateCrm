using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace WebApp.ViewModels.Home
{
    public class HomePageFilter
    {
        [UIHint("dropdown")]
        [Display(Name = "Вид жилья")]
        public DropDownViewModel HousingTypeList { get; set; }

        [UIHint("dropdown")]
        [Display(Name = "Город")]
        public DropDownViewModel City { get; set; }

        [UIHint("dropdown")]
        [Display(Name = "Район")]
        public DropDownViewModel District { get; set; }

        [Display(Name = "Цена от")]
        public int MinCost { get; set; }

        [Display(Name = "Цена до")]
        public int MaxCost { get; set; }
    }
}

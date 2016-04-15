using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Entities;

namespace WebApp.ViewModels.Home
{
    public class HomePageViewModel
    {
        public List<HousingViewModel> Items { get; set; }

        public HomePageFilter Filter { get; set; } = new HomePageFilter();

        public int TotalPages { get; set; }
        public int CurrentPage { get; set; }
    }
}

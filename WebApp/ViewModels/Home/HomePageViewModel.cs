using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.ViewModels.Home
{
    public class HomePageViewModel
    {
       public List<RentViewModel> Rents { get; }

        public HomePageViewModel()
        {
            Rents = new List<RentViewModel>();
            for (int i = 0; i < 50; i++)
            {
                Rents.Add(RentViewModel.Create());
            }
        }
    }
}

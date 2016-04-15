using System.Linq;
using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Mvc;
using WebApp.Entities;
using WebApp.Models;
using WebApp.ViewModels;
using WebApp.ViewModels.Home;

namespace WebApp.Controllers
{
    [AllowAnonymous]
    public class HomeController : BaseController
    {
        public HomeController(ApplicationDbContext dbContext, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager) 
            : base(dbContext, null)
        {
            if (!InitTestData.DatabaseInit)
            {
                InitTestData.RoleSync(roleManager);
                InitTestData.DatabaseInitData(dbContext, userManager, roleManager);
            }
        }

        public IActionResult Index(int? page, int[] houseTypeId, int? cityId, int? priceFrom, int? priceTo)
        {

          
            IQueryable<Housing> query = _context.Housing
                                                  .AddCityFilter(cityId)
                                                  .AddHousingTypeFilter(houseTypeId)
                                                  .AddCostFilter(priceFrom, priceTo);

            int totalItems;
            int totalPages;
            var items = query.GetPage(page ?? 1, out totalItems, out totalPages);
            //Thread.Sleep(2000);
            bool isAuth = User.Identity.IsAuthenticated;

            var model = new HomePageViewModel
            {
                Items = items.Select(x => HousingViewModel.Create(x, isAuth)).ToList(),
                CurrentPage = page ?? 1,
                TotalPages = totalPages,
                Filter = new HomePageFilter()
                {
                    //HousingTypeList = DropDownViewModel(houseType ?? 0, _context.TypesHousing.ToSelectList(true))
                }
            };

            return View(model);
        }
        
    }
}

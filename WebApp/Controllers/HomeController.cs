using System;
using System.Linq;
using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using WebApp.Entities;
using WebApp.Models;
using WebApp.ViewModels;
using WebApp.ViewModels.Home;
using Microsoft.AspNet.Http.Internal;
using Microsoft.Data.Entity;

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

            Func<Housing, bool> func = (x) =>
            {
                bool result = true;
                if (cityId.HasValue)
                {
                    result &= x.CityId == cityId.Value;
                }

                if (houseTypeId.Length > 0)
                {
                    result &= houseTypeId.Contains(x.TypesHousingId);
                }

                if (priceFrom.HasValue && priceTo.HasValue)
                {
                    result &= x.Sum >= priceFrom.Value && x.Sum <= priceTo.Value;
                }
                else if (priceFrom.HasValue)
                {
                    result &= x.Sum >= priceFrom.Value;
                }
                else if (priceTo.HasValue)
                {
                    result &= x.Sum <= priceTo.Value;
                }
                return result;
            };

            IQueryable<Housing> query = _context.Housing.Include(x => x.City)
                .Include(x => x.Street)
                .Include(x => x.District)
                .Include(x => x.Phones)
                .Include(x => x.TypesHousing)
                .Include(x => x.User).AsQueryable().Where(x => func(x));

            int totalItems;
            int totalPages;
            var items = query.PagedResult(page ?? 1, 20, x => x.CreatedAt, false, out totalItems, out totalPages).ToList();
          
            bool isAuth = User.Identity.IsAuthenticated;

            var model = new HomePageViewModel
            {
                Items = items.Select(x => HousingViewModel.Create(x, isAuth)).ToList(),
                CurrentPage = page ?? 1,
                TotalPages = totalPages,
                Filter = new HomePageFilter()
                {
                    HousingTypeList = new MultiSelectList(_context.TypesHousing, "Id", "Name", houseTypeId)
                    //new DropDownViewModel(houseType ?? 0, _context.TypesHousing.ToSelectList(true))
                }
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Filter(HomePageViewModel model, int? page, int[] houseTypeId, int? cityId, int? priceFrom, int? priceTo)
        {
            var housingTypeString = string.Empty;            
            foreach (var item in houseTypeId)
            {
                housingTypeString += $"houseTypeId={item}&";
            }

            return RedirectToAction(nameof(Index),
                new
                {
                    page, houseTypeId = housingTypeString.TrimEnd('&'), cityId, priceFrom, priceTo
                });
        }

    }
}

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

namespace WebApp.Controllers
{
    [AllowAnonymous]
    public class HomeController : BaseController
    {
        public HomeController(ApplicationDbContext dbContext, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager) 
            : base(dbContext)
        {
            if (!InitTestData.DatabaseInit)
            {
                InitTestData.RoleSync(roleManager);
                InitTestData.DatabaseInitData(dbContext, userManager, roleManager);
            }
        }

        public IActionResult Index(int? page, string houseTypeId, int? cityId, int? priceFrom, int? priceTo, int? districtId)
        {
            var houseTypeIdArray = string.IsNullOrEmpty(houseTypeId) ? new int[] {} : houseTypeId.Split(',').Select(x => Convert.ToInt32(x)).ToArray();
            var filterData = new HousingExtensions.FilterData
            {
                CityId = cityId,
                Page = page,
                PriceFrom = priceFrom,
                PriceTo = priceTo,
                HouseTypeId = houseTypeIdArray,
                DistrictId = districtId
            };

            IQueryable<Housing> query = _context.Housing
                .IncludeAll()
                .Where(x => HousingExtensions.Filter(filterData)(x));

            int totalItems;
            int totalPages;
            var items = query.PagedResult(page ?? 1, 20, x => x.CreatedAt, false, out totalItems, out totalPages).ToList();
          
            bool isAuth = User.Identity.IsAuthenticated;

            var model = new HomePageViewModel
            {
                Items = items.Select(x => HousingViewModel.Create(x, isAuth)).ToList(),
                CurrentPage = page ?? 1,
                TotalPages = totalPages,
                Filter = new HomePageFilter
                {
                    CityId = cityId ?? 0,
                    MinCost = priceFrom,
                    MaxCost = priceTo,
                    DistrictId = districtId ?? 0,
                    HousingTypeListIds = houseTypeIdArray.ToList(),
                    HousingTypeList = _context.TypesHousing.ToList().Select(x => new SelectListItem()
                    {
                        Value = x.Id.ToString(),
                        Text = x.Name,
                        Selected = houseTypeIdArray.Contains(x.Id)
                    }).ToList()
                }
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Filter(HomePageFilter model, int? page, int[] houseTypeId, int? cityId, int? priceFrom, int? priceTo, int? districtId)
        {
            var housingTypeString = houseTypeId.Aggregate(string.Empty, (current, item) => current + $"{item},");

            return RedirectToAction(nameof(Index),
                new
                {
                    page,
                    houseTypeId = housingTypeString.TrimEnd(','),
                    cityId,
                    priceFrom,
                    priceTo,
                    districtId = districtId.HasValue && districtId.Value > 0 ? districtId : null
                });
        }

    }
}

using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using WebApp.Entities;
using WebApp.Models;
using WebApp.ViewModels;
using WebApp.ViewModels.Home;
using Microsoft.EntityFrameworkCore;

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
                InitTestData.DatabaseInit = true;
                var testData = new InitTestData();
                testData.RoleSync(roleManager);
                testData.DatabaseInitData(_context, userManager, roleManager);
            }
        }

        public IActionResult Index(int? page, string houseTypeId, int? cityId, int? priceFrom, int? priceTo, string districtId)
        {
            var houseTypeIdArray = string.IsNullOrEmpty(houseTypeId) ? new int[] { } : houseTypeId.Split(',').Select(x => Convert.ToInt32(x)).ToArray();
            var districtIdArray = string.IsNullOrEmpty(districtId) ? new int[] { } : districtId.Split(',').Select(x => Convert.ToInt32(x)).ToArray();

            var filterParams = new HousingExtensions.FilterParams
            {
                CityId = cityId,
                Page = page,
                PriceFrom = priceFrom,
                PriceTo = priceTo,
                HouseTypeId = houseTypeIdArray,
                DistrictId = districtIdArray
            };

            if (IsCustomer)
            {
                var customer = _context.Clients.Include(x => x.TypesHousingToCustomers).Include(x => x.DistrictToClients).FirstOrDefault(x => x.Id == CustomerUser.CustomerId);
                if (customer != null)
                {
                    filterParams.CityId = customer.CityId;
                    filterParams.HouseTypeId = customer.TypesHousingToCustomers.Select(x => x.TypesHousingId).ToArray();

                    var districtIds = customer.DistrictToClients.Select(x => x.DistrictId).ToArray();
                    filterParams.DistrictId = districtIds;
                }
                else
                {
                    return BadRequest("Customer not found");//HttpNotFoundObjectResult("Customer not found");
                }
            }

            IQueryable<Housing> query = _context.Housing
                .IncludeAll()
                .Where(x => HousingExtensions.Filter(filterParams)(x));

            int totalItems;
            int totalPages;
            var items = query.PagedResult(page ?? 1, 20, x => x.CreatedAt, false, out totalItems, out totalPages).ToList();

            bool isAuth = User.Identity.IsAuthenticated;


            var model = new HomePageViewModel
            {
                Items = items.Select(x => HousingViewModel.Create(x, isAuth)).ToList(),
                CurrentPage = page ?? 1,
                TotalPages = totalPages,
                Filter = new HomePageFilter(filterParams)
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Filter(int? page, int[] houseTypeId, int? cityId, int? priceFrom, int? priceTo, int[] districtId)
        {
            var housingTypeString = houseTypeId.Aggregate(string.Empty, (current, item) => current + $"{item},");
            var districtIdString = districtId.Aggregate(string.Empty, (current, item) => current + $"{item},");

            return RedirectToAction(nameof(Index),
                new
                {
                    page,
                    houseTypeId = housingTypeString.TrimEnd(','),
                    cityId,
                    priceFrom,
                    priceTo,
                    districtId = districtIdString.TrimEnd(',')
                });
        }

    }
}

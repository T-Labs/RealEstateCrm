using System;
using System.Linq;
using System.Threading.Tasks;
using Data;
using Data.Query;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Web;
using Web.Helpers;
using WebApp.Data;
using WebApp.Entities;
using WebApp.Models;
using WebApp.ViewModels;
using WebApp.ViewModels.Home;


namespace WebApp.Controllers
{
    [AllowAnonymous]
    public class HomeController : BaseController
    {
        private ReadOnlyDataContext ReadOnlyDataContext { get; }
        private IQueryDispatcher QueryDispatcher { get; }
        private BuilderFactory BuilderFactory { get; }
        
        public HomeController(ReadOnlyDataContext readOnlyDataContext, IQueryDispatcher queryDispatcher, BuilderFactory builderFactory) : base(null, queryDispatcher)
        {
            ReadOnlyDataContext = readOnlyDataContext;
            QueryDispatcher = queryDispatcher;
            BuilderFactory = builderFactory;
        }

        public async Task<IActionResult> Index(int? page, string houseTypeId, int? cityId, int? priceFrom, int? priceTo, string districtId)
        {
            var houseTypeIdArray = string.IsNullOrEmpty(houseTypeId) ? new int[] {} : houseTypeId.Split(',').Select(x => Convert.ToInt32(x)).Where(x => x > 0).ToArray();
            var districtIdArray = string.IsNullOrEmpty(districtId) ? new int[] { } : districtId.Split(',').Select(x => Convert.ToInt32(x)).Where(x => x > 0).ToArray();
            
            var queryParams = new HousingPagedQuery
            {
                PageNumber = page ?? 1,
                PageSize = 20,
                CityId = cityId,
                Page = page,
                PriceFrom = priceFrom,
                PriceTo = priceTo,
                HouseTypeId = houseTypeIdArray,
                DistrictId = districtIdArray,
                CustomerId = CustomerUser?.CustomerId
            };
            
            var pagedItems = await QueryDispatcher.ExecuteAsync<HousingPagedQuery, PagedResults<Housing>>(queryParams);
            bool isAuth = User.Identity.IsAuthenticated;
            
            var model = new HomePageViewModel
            {
                Items = pagedItems.Items.Select(x =>
                {
                    var buider = BuilderFactory.Create<HousindViewModelBuilder>();
                    buider.Build(x).ShowMobilePhones(isAuth);
                    return buider.Model;
                }).ToList(),
                PageInfo = pagedItems.PageInfo,
                Filter = new HomePageFilter(queryParams)
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

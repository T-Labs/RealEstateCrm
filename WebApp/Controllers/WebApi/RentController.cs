using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.Data.Entity;
using WebApp.ViewModels;
using WebApp;
using WebApp.Entities;
using WebApp.Models;

namespace WebApp.WebApi
{
    public class RentController : Controller
    {
        // GET: api/values
        //[Route("api/rent")]
        //[HttpGet]
        //public IEnumerable<HousingViewModel> Get([FromServices] ApplicationDbContext dbContext, int? page, int[] houseTypeId, int? cityId, int? priceFrom, int? priceTo)
        //{
        //    IQueryable<Housing> query = dbContext.Housing
        //                                           .AddCityFilter(cityId)
        //                                           .AddHousingTypeFilter(houseTypeId)
        //                                           .AddCostFilter(priceFrom, priceTo);

        //    int totalItems;
        //    int totalPages;
        //    var items = query.GetPage(page ?? 1, out totalItems, out totalPages);
        //    //Thread.Sleep(2000);
        //    bool isAuth = User.Identity.IsAuthenticated;
        //    return items.Select(x => HousingViewModel.Create(x, isAuth));
        //}

        [Route("api/rent/houseTypes")]
        [HttpGet]
        public IEnumerable<SelectListItem> GetHouseTypes([FromServices] ApplicationDbContext repo)
        {
            return repo.TypesHousing.Select(x => new SelectListItem() { Value = x.Id.ToString(), Text = x.Name });
        }

        [Route("api/rent/cityList")]
        [HttpGet]
        public IEnumerable<SelectListItem> GetCityList([FromServices] ApplicationDbContext repo)
        {
            var city = repo.Cities.Select(x => new SelectListItem() { Value = x.Id.ToString(), Text = x.Name });
            return city;
        }

        [Route("api/rent/districtByCity")]
        [HttpGet]
        public IEnumerable<SelectListItem> GetDistrictByCity([FromServices] ApplicationDbContext repo, int? cityId)
        {
            IQueryable<District> districts = repo.Districts;

            if (cityId.HasValue)
            {
                districts = districts.Where(x => x.CityId == cityId.Value);
            }
            return districts.OrderBy(x => x.Name).Select(x => new SelectListItem() { Value = x.Id.ToString(), Text = x.Name });
        }

        [Route("api/rent/streetsByCity")]
        [HttpGet]
        public JsonResult GetStreetsByCity([FromServices] ApplicationDbContext repo, int? cityId)
        {
          
            if (cityId.HasValue)
            {
                var city = repo.Cities.Include(x => x.Streets).Single(x => x.Id == cityId);
                var items = city.Streets.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name }).ToList();
                return new JsonResult(new
                {
                    items = items
                });
            }
            return new JsonResult(new
            {
                items = new List<SelectListItem>()
            });
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using WebApp.Entities;
using WebApp.Models;
using WebApp.ViewModels;

namespace WebApp.WebApi
{
    
    public class RentController : Controller
    {
        // GET: api/values
        [Route("api/rent")]
        [HttpGet]
        public IEnumerable<BuildingViewModel> Get([FromServices] ApplicationDbContext dbContext, int? page, int[] houseTypeId, int? cityId, int? priceFrom, int? priceTo)
        {
            var items = dbContext.Housing.GetBuildings(page, houseTypeId, cityId, priceFrom, priceTo);
            //Thread.Sleep(2000);
            return items.Select(BuildingViewModel.Create);
        }

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
        
    }
}

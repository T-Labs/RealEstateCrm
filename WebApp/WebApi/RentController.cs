using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using WebApp.DAL;
using WebApp.ViewModels;

namespace WebApp.WebApi
{
    
    public class RentController : Controller
    {
        // GET: api/values
        [Route("api/rent")]
        [HttpGet]
        public IEnumerable<BuildingViewModel> Get([FromServices] BuildingRepository repo, int? page, int[] houseTypeId, int? cityId, int? priceFrom, int? priceTo)
        {
            var items = repo.GetBuildings(page, houseTypeId, cityId, priceFrom, priceTo);
            //Thread.Sleep(2000);
            return items.Select(BuildingViewModel.Create);
        }

        [Route("api/rent/houseTypes")]
        [HttpGet]
        public IEnumerable<SelectListItem> GetHouseTypes()
        {
            return MockData.HouseTypes.Select(x => new SelectListItem() { Value = x.Id.ToString(), Text = x.Name });
        }

        [Route("api/rent/cityList")]
        [HttpGet]
        public IEnumerable<SelectListItem> GetCityList()
        {
            var city = MockData.CityList.Select(x => new SelectListItem() { Value = x.Id.ToString(), Text = x.Name });
            return city;
        }

        [Route("api/rent/districtByCity")]
        [HttpGet]
        public IEnumerable<SelectListItem> GetDistrictByCity(int? cityId)
        {
            var districts = MockData.Districs;

            if (cityId.HasValue)
            {
                districts = districts.Where(x => x.CityId == cityId.Value).ToArray();
            }

            return districts.Select(x => new SelectListItem() { Value = x.Id.ToString(), Text = x.Name });
        }
        
        // GET api/values/5
        /*  [HttpGet("{id}")]
          public string Get(int id)
          {
              return "value";
          }

          // POST api/values
          [HttpPost]
          public void Post([FromBody]string value)
          {
          }

          // PUT api/values/5
          [HttpPut("{id}")]
          public void Put(int id, [FromBody]string value)
          {
          }

          // DELETE api/values/5
          [HttpDelete("{id}")]
          public void Delete(int id)
          {
          }*/
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using WebApp.ViewModels;
using WebApp.ViewModels.Home;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApp.WebApi
{
    
    public class RentController : Controller
    {
        // GET: api/values
        [Route("api/rent")]
        [HttpGet]
        public IEnumerable<RentViewModel> Get(int[] houseTypeId)
        {
            var model = new HomePageViewModel();
            if (houseTypeId.Length > 0)
            {
                return model.Rents.Where(x => houseTypeId.Contains(x.HouseTypeId));
            }
            return model.Rents;
        }

        [Route("api/rent/houseTypes")]
        [HttpGet]
        public IEnumerable<SelectListItem> GetHouseTypes()
        {
            return MockData.HouseTypes.Select(x => new SelectListItem() { Value = x.Id.ToString(), Text = x.Name });
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

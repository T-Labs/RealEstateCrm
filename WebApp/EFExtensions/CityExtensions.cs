using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.Data.Entity;
using WebApp.Entities;

namespace WebApp
{
    public static class CityExtensions
    {
        public static IEnumerable<SelectListItem> ToSelectList(this IEnumerable<City> cityList)
        {
            var items = cityList.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name }).ToList();
            return items;
        }
    }
}

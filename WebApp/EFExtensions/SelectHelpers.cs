using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc.Rendering;
using WebApp.Entities;

namespace WebApp
{
    public static class SelectHelpers
    {
        public static IEnumerable<SelectListItem> ToSelectList(this IEnumerable<TypesHousing> cityList, bool addedEmpty = false)
        {
            var items = cityList.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name }).ToList();
            if (addedEmpty)
            {
                items.Insert(0, new SelectListItem { Value = "", Selected = true, Text = "Все виды жилья" });
            }
            return items;
        }

        public static IEnumerable<SelectListItem> ToSelectList(this IEnumerable<Street> streets)
        {
            var items = streets.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name }).ToList();
            return items;
        }

        public static IEnumerable<SelectListItem> ToSelectList(this IEnumerable<District> cityList, bool addedEmpty = false)
        {
            var items = cityList.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name }).ToList();
            if (addedEmpty)
            {
                items.Insert(0, new SelectListItem { Value = "", Selected = true, Text = "Все районы" });
            }
            return items;
        }

        public static IEnumerable<SelectListItem> ToSelectList(this IEnumerable<City> cityList, bool addedEmpty = false)
        {
            var items = cityList.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name }).ToList();
            if (addedEmpty)
            {
                items.Insert(0, new SelectListItem { Value = "", Selected = true, Text = "Все города" });
            }
            return items;
        }
    }
}

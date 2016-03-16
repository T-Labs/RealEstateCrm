using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.Data.Entity;
using WebApp.Entities;

namespace WebApp
{
    public static class StreetExtensions
    {
        public static IEnumerable<SelectListItem> ToSelectList(this IEnumerable<Street> streets)
        {
            var items = streets.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name }).ToList();
            return items;
        }
    }
}

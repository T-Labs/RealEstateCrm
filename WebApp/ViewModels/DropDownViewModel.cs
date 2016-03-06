using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc.Rendering;

namespace WebApp.ViewModels
{
    public class DropDownViewModel
    {
        public int Id { get; set; }
        public IEnumerable<SelectListItem> Items { get; set; } 
    }
}

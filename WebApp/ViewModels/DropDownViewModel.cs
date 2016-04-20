using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.Mvc.Rendering;
using Newtonsoft.Json;

namespace WebApp.ViewModels
{
    [Obsolete("Don't use")]
    public class DropDownViewModel
    {
        public int Id { get; set; }

        [JsonIgnore]
        public IEnumerable<SelectListItem> Items { get; set; }

        [JsonIgnore]
        public string SelectedText
        {
            get { return Items?.FirstOrDefault(x => x.Value == Id.ToString())?.Text ?? string.Empty; }
        }

        public bool Disabled { get; set; }

        public bool IsMultiple { get; set; }

        public DropDownViewModel()
        {
        }

        public DropDownViewModel(int id, IEnumerable<SelectListItem> items)
        {
            Id = id;
            Items = items;
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Entities;

namespace WebApp.ViewModels
{
    public class BlackListViewModel
    {
        public int Id { get; private set; }

        [Required]
        [Display(Name = "Описание")]
        [UIHint("string")]
        public string Description { get; private set; } = string.Empty;

        [Required]
        [Display(Name = "Номер телефона")]
        [UIHint("string")]
        public string PhoneNumber { get; private set; } = string.Empty;

        public static BlackListViewModel Create(Blacklist item)
        {
            var model = new BlackListViewModel
            {
                Id = item.Id,
                Description = item.Description,
                PhoneNumber = item.PhoneNumber
            };

            return model;
        }
        
    }
}

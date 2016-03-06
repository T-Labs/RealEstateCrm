using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Entities;

namespace WebApp.ViewModels
{
    public class HousingEditModel
    {
        public int Id { get; private set; }
        
        [Display(Name = "Фамилия")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Имя")]
        public string MidleName { get; set; }
        
        [Display(Name = "Отчество")]
        public string LastName { get; set; }

        [Display(Name = "Описание")]
        public string Comment { get; set; }

        [Display(Name = "Цена")]
        public double Cost { get; set; }

        [UIHint("address")]
        public AddressSelectionModel Address { get; set; }
        
        public HousingEditModel()
        {
        }

        public static HousingEditModel Create(Housing housing, List<City> allCities)
        {
            var item = new HousingEditModel
            {
                Id = housing.Id,
                Comment = housing.Comment,
                FirstName = housing.FirstName,
                LastName = housing.LastName,
                MidleName = housing.MidleName,
                Cost = housing.Sum,
                Address = new AddressSelectionModel(allCities, housing.City)
            };

            return item;
        }
    }
}

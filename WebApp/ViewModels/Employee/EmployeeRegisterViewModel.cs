using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using WebApp.Entities;

namespace WebApp.ViewModels
{
    public class EmployeeRegisterViewModel : EmployeeEditViewModel
    {

        [UIHint("string")]
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [UIHint("string")]
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [UIHint("string")]
        [DataType(DataType.Password)]
        [Display(Name = "Подтвердите пароль")]
        [Compare("Password", ErrorMessage = "Пароли не совпадают.")]
        public string ConfirmPassword { get; set; }

        public EmployeeRegisterViewModel(): base()
        {
        }

        public EmployeeRegisterViewModel(int cityId, List<City> cityList) : base(cityId, cityList)
        {
        }

    }
}

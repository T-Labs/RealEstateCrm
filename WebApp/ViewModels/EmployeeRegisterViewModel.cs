using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.ViewModels
{
    public class EmployeeRegisterViewModel
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

        [UIHint("string")]
        [Display(Name = "Фамилия")]
        public string FirstName { get; set; }

        [UIHint("string")]
        [Required]
        [Display(Name = "Имя")]
        public string MidleName { get; set; }

        [UIHint("string")]
        [Display(Name = "Отчество")]
        public string LastName { get; set; }

        [UIHint("checkbox")]
        [Display(Name = "Создание объектов")]
        public bool IsCreateHousing { get; set; }

        [UIHint("checkbox")]
        [Display(Name = "Редактирование объектов")]
        public bool IsEditHousing { get; set; }

        [UIHint("checkbox")]
        [Display(Name = "Удаление объектов")]
        public bool IsDeleteHousiong { get; set; }

        [UIHint("checkbox")]
        [Display(Name = "Создание карточки клиентов")]
        public bool IsCreateCustomer { get; set; }

        [UIHint("checkbox")]
        [Display(Name = "Редактирование карточки клиентов")]
        public bool IsEditCustomer { get; set; }

        [UIHint("checkbox")]
        [Display(Name = "Удаление карточки клиентов")]
        public bool IsDeleteCustomer { get; set; }

        [UIHint("checkbox")]
        [Display(Name = "Создание и редактирование пользователей")]
        public bool IsManageUsers { get; set; }

        [UIHint("dropdown")]
        [Display(Name = "Город")]
        public DropDownViewModel City { get; set; }

        public List<string> GetSelectedRoles()
        {
            var roles = new List<string>();
            //customer
            if (IsCreateCustomer) { roles.Add(RoleNames.CreateCustomer); }
            if (IsEditCustomer) { roles.Add(RoleNames.EditCustomer); }
            if (IsDeleteCustomer) { roles.Add(RoleNames.DeleteCustomer); }

            //housing
            if (IsCreateHousing) { roles.Add(RoleNames.CreateHousing); }
            if (IsEditHousing) { roles.Add(RoleNames.EditHousing); }
            if (IsDeleteHousiong) { roles.Add(RoleNames.DeleteHousing); }

            //
            if (IsManageUsers) { roles.Add(RoleNames.ManageUser); }

            return roles;
        }


        public static EmployeeRegisterViewModel CreateForEdit(ApplicationUser user)
        {
            var item = new EmployeeRegisterViewModel
            {
                
            };

            return item;
        }
    }
}

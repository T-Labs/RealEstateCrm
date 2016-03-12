using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using WebApp.Models;

namespace WebApp.ViewModels
{
    public class EmployeeRegisterViewModel
    {
        public string EditId { get; set; }

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
        [Display(Name = "Ф.И.О.")]
        public string FIO { get; set; }

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


        public static EmployeeRegisterViewModel CreateForEdit(ApplicationUser user, List<IdentityRole> roles)
        {
            Dictionary<string, IdentityRole> roleMap = roles.ToDictionary(x => x.Id, x => x);
            var item = new EmployeeRegisterViewModel
            {
                EditId = user.Id,
                FIO = user.UserName,
                IsCreateCustomer = user.Roles.SingleOrDefault(x => roleMap[x.RoleId].Name == RoleNames.CreateCustomer) != null,
                IsEditCustomer = user.Roles.SingleOrDefault(x => roleMap[x.RoleId].Name == RoleNames.EditCustomer) != null,
                IsDeleteCustomer = user.Roles.SingleOrDefault(x => roleMap[x.RoleId].Name == RoleNames.DeleteHousing) != null,

                IsCreateHousing= user.Roles.SingleOrDefault(x => roleMap[x.RoleId].Name == RoleNames.CreateHousing) != null,
                IsEditHousing = user.Roles.SingleOrDefault(x => roleMap[x.RoleId].Name == RoleNames.EditHousing) != null,
                IsDeleteHousiong = user.Roles.SingleOrDefault(x => roleMap[x.RoleId].Name == RoleNames.DeleteHousing) != null,

                IsManageUsers = user.Roles.SingleOrDefault(x => roleMap[x.RoleId].Name == RoleNames.ManageUser) != null,
            };

            return item;
        }
    }
}

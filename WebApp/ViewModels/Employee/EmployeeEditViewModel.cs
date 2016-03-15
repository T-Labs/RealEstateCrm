using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.AspNet.Identity.EntityFramework;
using WebApp.Entities;
using WebApp.Models;

namespace WebApp.ViewModels
{
    public class EmployeeEditViewModel
    {
        public string EditId { get; set; }


        [UIHint("string")]
        [Display(Name = "Ф.И.О.")]
        public string FIO { get; set; }

        [Editable(false)]
        [UIHint("string")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [UIHint("string")]
        [Display(Name = "Пароль")]
        public string OpenPassword { get; set; }

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

        public EmployeeEditViewModel()
        {
        }

        public EmployeeEditViewModel(int cityId, List<City> cityList)
        {
            City = new DropDownViewModel(cityId, cityList.ToSelectList());
        }

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


        public static EmployeeEditViewModel CreateForEdit(ApplicationUser user, List<IdentityRole> roles, List<City> cityList)
        {
            Dictionary<string, IdentityRole> roleMap = roles.ToDictionary(x => x.Id, x => x);
            var item = new EmployeeEditViewModel
            {
                EditId = user.Id,
                FIO = user.FIO,
                Email = user.Email,
                OpenPassword = user.OpenPassword,
                City = new DropDownViewModel(user.CityId ?? 0, cityList.ToSelectList()),
                IsCreateCustomer = user.Roles.SingleOrDefault(x => roleMap[x.RoleId].Name == RoleNames.CreateCustomer) != null,
                IsEditCustomer = user.Roles.SingleOrDefault(x => roleMap[x.RoleId].Name == RoleNames.EditCustomer) != null,
                IsDeleteCustomer = user.Roles.SingleOrDefault(x => roleMap[x.RoleId].Name == RoleNames.DeleteCustomer) != null,

                IsCreateHousing= user.Roles.SingleOrDefault(x => roleMap[x.RoleId].Name == RoleNames.CreateHousing) != null,
                IsEditHousing = user.Roles.SingleOrDefault(x => roleMap[x.RoleId].Name == RoleNames.EditHousing) != null,
                IsDeleteHousiong = user.Roles.SingleOrDefault(x => roleMap[x.RoleId].Name == RoleNames.DeleteHousing) != null,

                IsManageUsers = user.Roles.SingleOrDefault(x => roleMap[x.RoleId].Name == RoleNames.ManageUser) != null,
            };

            return item;
        }
    }
}
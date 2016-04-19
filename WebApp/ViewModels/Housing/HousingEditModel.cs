using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.Data.Entity;
using WebApp.Entities;
using WebApp.Models;

namespace WebApp.ViewModels
{
    public class HousingEditModel
    {
        [Display(Name = "ID объекта")]
        public int EditId { get; private set; }
        
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
       
        [UIHint("city-selector")]
        [Required]
        [Display(Name = "Город")]
        public int CityId { get; set; }

        [UIHint("dropdown")]
        [Required]
        [Display(Name = "Район")]
        public DropDownViewModel District { get; set; }

        [UIHint("dropdown")]
        [Required]
        [Display(Name = "Улица")]
        public DropDownViewModel Street { get; set; }

        [Required]
        [Display(Name = "Номер дома")]
        public string HouseNumber { get; set; }

        [Display(Name = "Строение")]
        public string HouseBuilding { get; set; }

        [Display(Name = "Номер квартиры")]
        public string Room { get; set; }

        [UIHint("phone")]
        [Required]
        [Display(Name = "Телефон 1 для связи")]
        public string Phone1 { get; set; }

        [UIHint("phone")]
        [Display(Name = "Телефон 2 для связи")]
        public string Phone2 { get; set; }

        [UIHint("phone")]
        [Display(Name = "Телефон 3 для связи")]
        public string Phone3 { get; set; }

       /* [UIHint("dropdown")]
        [Display(Name =  "Тип жилья")]
        public DropDownViewModel HouseType { get; set; }*/

        [Display(Name = "Тип жилья")]
        public int HouseTypeId { get; set; }

        [Display(Name = "Дата освобождения объекта")]
        public DateTime EndDate { get; set; }

        [Display(Name = "Полный адрес")]
        public string FullAddress { get; set; }

        [UIHint("checkbox")]
        [Display(Name = "В архиве")]
        public bool IsArchived { get; set; }

        [UIHint("checkbox")]
        [Display(Name = "Партнерство")]
        public bool IsPartnerShip { get; set; }

        public List<HousingCallViewModel> Calls { get; set; }

        public HousingEditModel()
        {
        }

        public static HousingEditModel Create(ApplicationDbContext context, Housing housing,  ClaimsPrincipal user)
        {
            var allCities = context.Cities.Include(x => x.Districts).Include(x => x.Streets).ToSelectList().ToList();
            var typesHousings = context.TypesHousing.ToList();
            
            return Create(housing, typesHousings, allCities, user);
        }

        public static HousingEditModel Create(Housing housing, 
            List<TypesHousing> typesHousings, 
            List<SelectListItem> allCities,
            ClaimsPrincipal user)
        {
            var item = new HousingEditModel
            {
                EditId = housing.Id,
                Comment = housing.Comment,
                FirstName = housing.FirstName,
                LastName = housing.LastName,
                MidleName = housing.MidleName,
                Cost = housing.Sum,
                EndDate = housing.EndDate,
                Phone1 = housing.Phones.SingleOrDefault(x => x.Order == 0)?.Number,
                Phone2 = housing.Phones.SingleOrDefault(x => x.Order == 1)?.Number,
                Phone3 = housing.Phones.SingleOrDefault(x => x.Order == 2)?.Number,
                HouseNumber = housing?.House,
                HouseBuilding = housing?.Building,
                Room = housing?.Room,
                IsArchived = housing.IsArchive,
                IsPartnerShip = housing.PartherShip,
               /* HouseType = new DropDownViewModel
                {
                    Id = housing.TypesHousingId,
                    Items = typesHousings.Select(x => new SelectListItem {Value = x.Id.ToString(), Text = x.Name })
                },*/
                HouseTypeId = housing.TypesHousingId,
                CityId = housing.CityId
                //Calls = housing.Calls.Select(HousingCallViewModel.Create).ToList()
            };

           /* item.City = new DropDownViewModel(housing?.CityId ?? 0, allCities)
            {
                Disabled = user.IsInRole(RoleNames.Employee)
            };*/
            item.Street = new DropDownViewModel(housing?.StreetId ?? 0, housing?.City?.Streets?.ToSelectList());

            item.District = new DropDownViewModel()
            {
                Id = housing?.DistrictId ?? 0,
                Items = housing?.City?.Districts?.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name }) ?? new List<SelectListItem>()
            };

            var addressParts = new List<string>();
            if (housing.City != null)
            {
                addressParts.Add(housing.City.Name);
            }

            if (housing.District != null)
            {
                addressParts.Add(housing.District.Name);
            }

            if (housing.Street != null)
            {
                addressParts.Add(housing.Street.Name);
            }

            addressParts.Add(housing.House);
            addressParts.Add(housing.Building);
            addressParts.Add(housing.Room);
            
            item.FullAddress = addressParts.Where(x => !string.IsNullOrEmpty(x)).Aggregate("", (x, y) => x + ", " + y).Trim(',');

            return item;
        }


        public void UpdateEntity(Housing item)
        {
            item.FirstName = FirstName;
            item.MidleName = MidleName;
            item.LastName = LastName;
            item.Sum = Cost;
            item.Comment = Comment;
            item.CityId = CityId;
            item.DistrictId = District.Id;
            item.StreetId = Street.Id;
            item.House = HouseNumber;
            item.Building = HouseBuilding;
            item.Room = Room;
            item.TypesHousingId = HouseTypeId;
            item.EndDate = EndDate;
            item.IsArchive = IsArchived;
            item.PartherShip = IsPartnerShip;

            UpdatePhone(item, 0, Phone1);
            UpdatePhone(item, 1, Phone2);
            UpdatePhone(item, 2, Phone3);
        }

        private static void UpdatePhone(Housing item, int order, string phone)
        {
            var housingPhone = item.Phones.SingleOrDefault(x => x.Order == order);
            if (housingPhone != null)
            {
                housingPhone.Number = phone;
            }
            else if(!string.IsNullOrEmpty(phone))
            {
                housingPhone = new HousingPhone { Number = phone, Order = order };
                item.Phones.Add(housingPhone);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc.Rendering;
using WebApp.Entities;

namespace WebApp.ViewModels
{
    public class CustomerEditModel
    {
        [Display(Name = "ID объекта")]
        public int EditId { get; set; }

        [Required]
        [Display(Name = "Фамилия")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Имя")]
        public string MidleName { get; set; }

        [Display(Name = "Отчество")]
        public string LastName { get; set; }

        [UIHint("city-selector")]
        [Required]
        [Display(Name = "Город")]
        public int CityId { get; set; }

        [Required]
        [Display(Name = "Районы")]
        public List<int> DistrictList { get; set; }
        
        [Required]
        [Display(Name = "Типы жилья")]
        public List<int> HousingTypeListIds { get; set; } = new List<int>();

        [Display(Name = "Статус")]
        public int Status { get; set; }

        [DataType(DataType.Currency)]
        [Display(Name = "Стоимость, от")]
        public int MinSum { get; set; }

        [DataType(DataType.Currency)]
        [Display(Name = "Стоимость, до")]
        public int MaxSum { get; set; }

        [Display(Name = "Дата и время встречи")]
        public DateTime DateMeeting { get; set; }
        
        [Display(Name = "Сумма договора")]
        public int ContractSum { get; set; }
        
        [Display(Name = "Решех")]
        public int ReheshSum { get; set; }

        [Display(Name="Пол")]
        public int Gender { get; set; }

        [UIHint("checkbox")]
        [Display(Name="Доступ на сайт")]
        public bool IsSiteAccess { get; set; }

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

        [UIHint("checkbox")]
        [Display(Name = "В архиве")]
        public bool IsArchive { get; set; }

        public void UpdateEntity(Customer customer)
        {
            customer.FirstName = FirstName;
            customer.MidleName = MidleName;
            customer.LastName = LastName;
            customer.Gender = (Gender)Gender;
            customer.CityId = CityId;
            customer.ContractSum = ContractSum;
            customer.MinSum = MinSum;
            customer.MaxSum = MaxSum;
            customer.ReheshSum = ReheshSum;
            customer.IsSiteAccess = IsSiteAccess;
            customer.DateMeeting = DateMeeting;
            customer.Status = (CustomerStatus)Status;

            UpdatePhone(customer, 0, Phone1);
            UpdatePhone(customer, 1, Phone2);
            UpdatePhone(customer, 2, Phone3);
        }


        public void UpdateDistricts(Customer customer)
        {
            var toRemove = new List<DistrictToСlient>();
            foreach (var district in customer.DistrictToClients)
            {
                if (!DistrictList.Contains(district.DistrictId))
                {
                    toRemove.Add(district);
                }
            }

            toRemove.ForEach(x => customer.DistrictToClients.Remove(x));

            foreach (var districtId in DistrictList)
            {
                var district = customer.DistrictToClients.FirstOrDefault(x => x.DistrictId == districtId);
                if (district == null)
                {
                    customer.DistrictToClients.Add(new DistrictToСlient()
                    {
                        ClientId = customer.Id,
                        DistrictId = districtId
                    });
                }
            }
        }

        public void UpdateHousingTypes(Customer customer)
        {
            var toRemove = new List<TypesHousingToCustomer>();
            foreach (var district in customer.TypesHousingToCustomers)
            {
                if (!HousingTypeListIds.Contains(district.TypesHousingId))
                {
                    toRemove.Add(district);
                }
            }

            toRemove.ForEach(x => customer.TypesHousingToCustomers.Remove(x));

            foreach (var id in HousingTypeListIds)
            {
                var district = customer.TypesHousingToCustomers.FirstOrDefault(x => x.TypesHousingId == id);
                if (district == null)
                {
                    customer.TypesHousingToCustomers.Add(new TypesHousingToCustomer()
                    {
                        ClientId = customer.Id,
                        TypesHousingId = id
                    });
                }
            }
        }


        private static void UpdatePhone(Customer item, int order, string phone)
        {
            var housingPhone = item.Phones.SingleOrDefault(x => x.Order == order);
            if (housingPhone != null)
            {
                housingPhone.Number = phone;
            }
            else if (!string.IsNullOrEmpty(phone))
            {
                housingPhone = new CustomerPhone() { Number = phone, Order = order };
                item.Phones.Add(housingPhone);
            }
        }

        public static CustomerEditModel Create(Customer c)
        {
            var item = new CustomerEditModel()
            {
                EditId = c.Id,
                Gender = c.Gender.HasValue ? (int)c.Gender : (-1),
                CityId = c.CityId,
                ContractSum = c.ContractSum,
                FirstName = c.FirstName,
                MidleName = c.MidleName,
                LastName = c.LastName,
                IsArchive = c.IsArchive,
                IsSiteAccess = c.IsSiteAccess,
                MinSum = c.MinSum,
                MaxSum = c.MaxSum,
                ReheshSum = c.ReheshSum,
                DateMeeting = c.DateMeeting,
                Status = (int)c.Status,
                Phone1 = c.Phones.SingleOrDefault(x => x.Order == 0)?.Number,
                Phone2 = c.Phones.SingleOrDefault(x => x.Order == 1)?.Number,
                Phone3 = c.Phones.SingleOrDefault(x => x.Order == 2)?.Number,
                DistrictList = c.DistrictToClients.Select(x => x.DistrictId).ToList(),
                HousingTypeListIds = c.TypesHousingToCustomers.Select(x => x.TypesHousingId).ToList()
            };
            return item;
        }
    }
}

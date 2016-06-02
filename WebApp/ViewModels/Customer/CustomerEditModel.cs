using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc.Rendering;

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
    }
}

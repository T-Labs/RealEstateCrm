using System.Linq;
using WebApp.Entities;

namespace WebApp.ViewModels
{
    public class HousingViewModel
    {
        public int RentId { get; set; }

        public string HouseType { get;  set; }
        public int HouseTypeId { get;  set; }

        public int Price { get;  set; }

        public string District { get;  set; }

        public string Street { get;  set; }

        public string Description { get;  set; }

        private string _phone;
        public string Phone
        {
            get => ShowMobilePhone ? _phone: string.Empty;
            set => _phone = value;
        }
        
        public bool ShowMobilePhone { get; set; }

        public string CityName { get;  set; }

        public int CityId { get;  set; }

        public int DistrictId { get;  set; }
    }
}

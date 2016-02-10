using System.Collections.Generic;

namespace WebApp.Entities
{
    public class District
    {
        public int Id { get; set; }

        public string Name { get; set; }

        //public int CityId { get; set; }

        //public virtual City Cities { get; set; }

        public List<DistrictСustomer> DistrictСustomers { get; set; }

    }
}
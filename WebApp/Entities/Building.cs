using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.Entities
{
    public class Building
    {
        public int Id { get; set; }

        public string Phone1 { get; set; }

        public string Phone2 { get; set; }

        public string Phone3 { get; set; }

        public int CityId { get; set; }

        public virtual City City { get; set; }

        public int DistrictId { get; set; }

        public virtual District District { get; set; }

        public int StreetId { get; set; }

        public virtual Street Street { get; set; }

        public string House { get; set; }

        public string Housing { get; set; }

        public string Apartment { get; set; }

        public virtual TypesHousing TypesHousing { get; set; }

        public double Sum { get; set; }

        public string Currency { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string MidleName { get; set; }

        public int PartherShip { get; set; }

        public string Comment { get; set; }

        public DateTime InDate { get; set; }

        public DateTime EndDate { get; set; }

        public DateTime RevisionDate { get; set; }

        public string ApplicationUserId { get; set; }

        public ApplicationUser User { get; set; }
    }
}



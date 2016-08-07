using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Entities
{
    public class Housing
    {
        public int Id { get; set; }

        public List<HousingPhone> Phones { get; set; }

        public int CityId { get; set; }

        public virtual City City { get; set; }

        public int DistrictId { get; set; }

        public virtual District District { get; set; }

        public int StreetId { get; set; }

        public virtual Street Street { get; set; }

        public string House { get; set; }

        public string Building { get; set; }

        public string Room { get; set; }

        public int TypesHousingId { get; set; }
        public virtual TypesHousing TypesHousing { get; set; }

        public double Sum { get; set; }

        public string Currency { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string MidleName { get; set; }

        public bool PartherShip { get; set; }

        public string Comment { get; set; }

        public DateTime InDate { get; set; }

        public DateTime EndDate { get; set; }

        public DateTime RevisionDate { get; set; }
     
        public string ApplicationUserId { get; set; }
        
        public ApplicationUser User { get; set; }

        public bool IsArchive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? LastEditedAt { get; set; }

        public List<HousingCall> Calls { get; set; } 
    }
}



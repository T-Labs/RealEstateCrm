using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Entities
{
    public class Street
    {
        public int Id { get; set; }

        public string  Name { get; set; }

        public int DistrictId { get; set; }

        public virtual District District { get; set; }

        public List<Building> Buildings { get; set; }

    }
}

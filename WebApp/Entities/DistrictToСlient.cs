using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Entities
{
    public class DistrictToСlient
    {
        public int ClientId { get; set; }

        public Customer Clients { get; set; }

        public int DistrictId { get; set; }

        public District Districts { get; set; }
    }
}

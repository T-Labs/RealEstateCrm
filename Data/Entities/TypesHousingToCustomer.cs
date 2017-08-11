using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Entities
{
    public class TypesHousingToCustomer
    {
        public int ClientId { get; set; }

        public Customer Clients { get; set; }

        public int TypesHousingId { get; set; }

        public TypesHousing TypesHousing { get; set; }
    }
}

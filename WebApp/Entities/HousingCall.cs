using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealEstateCrm.Entities
{
    public class HousingCall : Call
    {
        public string Status { get; set; }

        public int HousingId { get; set; }

        public Housing Housing { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealEstateCrm.Entities
{
    public class Sms
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public string  Description { get; set; }

        public string Phone { get; set; }

        public int ClientId { get; set; }
        public virtual Customer Client { get; set; }
    }
}

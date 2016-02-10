using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Entities
{
    public class Blacklist
    {
        public int Id { get; set; }

        public int ClientId { get; set; }

        public virtual Client Clients { get; set; }

        public string  Description { get; set; }

        public DateTime  DateAdd { get; set; }

    }
}

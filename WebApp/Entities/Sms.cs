using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Entities
{
    public class Sms
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public string  Description { get; set; }

        public string Phone { get; set; }

        public List<Client> Clients { get; set; }
    }
}

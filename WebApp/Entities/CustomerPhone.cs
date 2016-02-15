using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Entities
{
    public class CustomerPhone
    {
        public int CustomerPhoneId { get; set; }

        public int Order { get; set; }

        public string Number { get; set; }
    }
}

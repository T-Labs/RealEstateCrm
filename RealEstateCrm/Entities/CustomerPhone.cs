using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Entities
{
    [DebuggerDisplay("CustomerPhoneId: {CustomerPhoneId}, Order: {Order}, Number: {Number} ")]
    public class CustomerPhone
    {
        public int CustomerPhoneId { get; set; }

        public int Order { get; set; }

        public string Number { get; set; }
    }
}

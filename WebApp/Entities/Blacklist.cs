using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models;

namespace RealEstateCrm.Entities
{
    public class Blacklist
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public string PhoneNumber { get; set; }

        public DateTime DateAdd { get; set; }

        public string ApplicationUserId { get; set; }

        public ApplicationUser User { get; set; }
    }
}

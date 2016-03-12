using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.Entities
{
    public abstract class Call
    {
        public int Id { get; set; }
        
        public DateTime  Date { get; set; }
        
        public string ApplicationUserId { get; set; }

        public ApplicationUser User { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Entities
{
    public class Street
    {
        public int Id { get; set; }

        public string  Name { get; set; }
/*
        public int CityId { get; set; }
        
        public virtual City City { get; set; }
        */
        public List<Housing> Buildings { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealEstateCrm.Entities
{
    public class Street
    {
        public int Id { get; set; }

        public string  Name { get; set; }

        public List<Housing> Buildings { get; set; }

    }
}

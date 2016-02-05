using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Entities
{
    public class Object
    {
        public int Id { get; set; }

        public virtual  TypesHousing TypesHousing { get; set; }
    }
}

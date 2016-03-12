using System.Collections.Generic;

namespace RealEstateCrm.Entities
{
    public class City
    {
        public int Id { get; set; }

        public string  Name  { get; set; }

        public List<District> Districts { get; set; }

        public List<Customer> Clients { get; set; }

        public List<Housing> Buildings { get; set; }


    }
}
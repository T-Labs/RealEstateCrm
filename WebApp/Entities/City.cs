using System.Collections.Generic;

namespace WebApp.Entities
{
    public class City
    {
        public int Id { get; set; }

        public string  Name  { get; set; }

        public virtual List<District> Districts { get; set; }

        public List<Client> Clients { get; set; }
    }
}
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using WebApp.Models;

namespace WebApp.Entities
{
    [DebuggerDisplay("Id:{Id}, Name: {Name}")]
    public class City
    {
        public int Id { get; set; }

        public string  Name  { get; set; }

        public List<District> Districts { get; set; }

        public List<Customer> Clients { get; set; }

        public List<Housing> Buildings { get; set; }

        public ICollection<Street> Streets { get; set; }

        public List<ApplicationUser> Users { get; set; }


    }
}
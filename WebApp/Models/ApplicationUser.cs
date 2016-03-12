using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using WebApp.Entities;

namespace WebApp.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public bool IsArchieved { get; set; }

        /*[Required]
        public bool OpenPassword { get; set; }
        public int CityId { get; set; }
        public City City { get; set; }*/
    }
}

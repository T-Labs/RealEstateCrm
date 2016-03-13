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

        [MaxLength(255)]
        public string FIO { get; set; }

        [MaxLength(50)]
        public string OpenPassword { get; set; }
/*
        public int CityId { get; set; }

        [ForeignKey("CityId")]
        public virtual City City { get; set; }
        */
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}

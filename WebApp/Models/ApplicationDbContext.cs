using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Data.Entity;
using WebApp.Entities;

namespace WebApp.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Blacklist> BlackLists { get; set; }

        //public DbSet<Call> Calls { get; set; }

        public DbSet<City> Cities { get; set; }

        public DbSet<Client>Clients { get; set; }

        public DbSet<District> Districts { get; set; }

        //public DbSet<Entities.Object> Objects { get; set; }

        //public DbSet<TypesHousing> TypesHousing { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<DistrictСustomer>()
                .HasKey(t => new {t.ClientId, t.DistrictId});

            builder.Entity<DistrictСustomer>()
                .HasOne(pt => pt.Clients)
                .WithMany(p => p.DistrictСustomers)
                .HasForeignKey(p => p.ClientId);

            builder.Entity<DistrictСustomer>()
                .HasOne(pt => pt.Districts)
                .WithMany(p => p.DistrictСustomers)
                .HasForeignKey(p => p.DistrictId);


        }
    }
}

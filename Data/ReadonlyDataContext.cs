using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using WebApp.Entities;
using WebApp.Models;

namespace Data
{
    public class ReadOnlyDataContext
    {
        private ApplicationDbContext DbContext { get; }

        public IQueryable<Housing> Housing => DbContext.Housing.AsNoTracking();
        public IQueryable<Customer> Clients => DbContext.Clients.AsNoTracking();
        public IQueryable<TypesHousing> TypesHousing => DbContext.TypesHousing.AsNoTracking();

        public IQueryable<ApplicationUser> Users => DbContext.Users.AsNoTracking();
        public IQueryable<City> Cities => DbContext.Cities.AsNoTracking();
        
        public ReadOnlyDataContext(ApplicationDbContext dbContext)
        {
            DbContext = dbContext;
        }
        
    }
}

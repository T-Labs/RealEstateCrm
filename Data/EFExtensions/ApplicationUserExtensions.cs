using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApp.Models;

namespace WebApp
{
    public static class ApplicationUserExtensions
    {

        public static ApplicationUser GetById(this DbSet<ApplicationUser> users, string id)
        {
            return users.Include(x => x.Roles).Single(m => m.Id == id);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApp.Models;

namespace Data.Query.Handlers
{
    public class UserQueryHandler: IQueryHandler<UserByIdQuery, ApplicationUser>
    {
        public Task<ApplicationUser> ExecuteAsync(ReadOnlyDataContext context, UserByIdQuery query)
        {
            var user = context.Users.Include(x => x.City).Include(x => x.City.Districts).FirstOrDefaultAsync(x => x.Id == query.Id);
            return user;
        }
    }
}

using System.Linq;
using System.Security.Claims;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Mvc;
using WebApp.Models;
using Microsoft.Data.Entity;

namespace WebApp.Controllers
{
    public abstract class BaseController : Controller
    {
        protected readonly ApplicationDbContext _context;

        protected ApplicationUser CurrentUser
        {
            get { return _context.Users.Include(x => x.City).Include(x => x.City.Districts).FirstOrDefault(x => x.Id == User.GetUserId()); }
        }

        //       protected IdentityRole CurrentRole { get; private set; }

        protected BaseController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            
        }
        
    }
}

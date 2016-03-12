using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Mvc;
using WebApp.Models;

namespace WebApp.Controllers
{
    public abstract class BaseController : Controller
    {
        protected readonly ApplicationDbContext _context;
        protected ApplicationUser CurrentUser { get; private set; }
        protected IdentityRole CurrentRole { get; private set; }

        protected BaseController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
        }
        
    }
}

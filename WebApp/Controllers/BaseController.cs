using System.IO;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using WebApp.Models;
using Microsoft.Data.Entity;
using Microsoft.Extensions.WebEncoders;

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


        protected void SuccessMessage(string message)
        {
            // Microsoft.Extensions.WebEncoders.HtmlEncoder.Default.HtmlEncode();
            
           // using (var tw = new StringWriter())
            {
             //  HtmlEncoder.Default.HtmlEncode(message, tw);
                TempData["CrmSuccessMessage"] = message;
            }
          
        }

    }
}

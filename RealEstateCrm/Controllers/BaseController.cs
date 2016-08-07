using System.IO;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;
using Microsoft.Extensions.WebEncoders;
using Newtonsoft.Json;
using WebApp.Entities;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Controllers
{
    public abstract class BaseController : Controller
    {
        protected readonly ApplicationDbContext _context;

        protected ApplicationUser CurrentUser
        {
            get { return _context.Users.Include(x => x.City).Include(x => x.City.Districts).FirstOrDefault(x => x.Id == User.Identity.Name); }
        }

        protected CustomerUser CustomerUser
        {
            get { return CustomerUser.FromSession(HttpContext.Session); }
            set { CustomerUser.ToSession(value, HttpContext.Session); }
        }

        protected bool IsCustomer => CustomerUser != null;

        protected BaseController(ApplicationDbContext context)
        {
            _context = context;
        }


        protected void SuccessMessage(string message)
        {
            TempData["CrmSuccessMessage"] = message;
        }

        protected void ErrorMessage(string message)
        {
            TempData["CrmErrorMessage"] = message;
        }

    }
}

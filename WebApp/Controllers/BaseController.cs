using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Mvc;
using WebApp.Models;

namespace WebApp.Controllers
{
    public abstract class BaseController : Controller
    {
        protected readonly ApplicationDbContext _context;
        protected ApplicationUser currentUser { get; private set; }
        protected IdentityRole currentRole { get; private set; }

        protected BaseController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
        }
        
    }
}

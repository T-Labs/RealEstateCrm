using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Mvc;
using WebApp.Models;

namespace WebApp.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        public HomeController(ApplicationDbContext dbContext, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            if (!InitTestData.DatabaseInit)
            {
                InitTestData.RoleSync(roleManager);
                InitTestData.DatabaseInitData(dbContext, userManager, roleManager);
            }
        }

        public IActionResult Index(){
           
            return View();
        }
        
    }
}

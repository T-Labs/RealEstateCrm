using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Mvc;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        public HomeController(ApplicationDbContext dbContext, UserManager<ApplicationUser> userManager)
        {
            if (!InitTestData.DatabaseInit)
            {
                InitTestData.DatabaseInitData(dbContext, userManager);
            }
        }

        public IActionResult Index()
        {
           
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}

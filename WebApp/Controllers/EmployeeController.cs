using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.Data.Entity;
using WebApp.Models;
using WebApp.ViewModels;
using WebApp.ViewModels.Account;

namespace WebApp.Controllers
{
    public class EmployeeController : Controller
    {
        private ApplicationDbContext _context;
        private UserManager<ApplicationUser> _userManager;

        public EmployeeController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            this._userManager = userManager;
        }

        // GET: Employee
        public IActionResult Index()
        {
            return View(_context.ApplicationUser.ToList());
        }

        [Authorize]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(EmployeeRegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = model.FIO,
                    Email = model.Email
                };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    var roles = new List<string>();
                    roles.Add(RoleNames.Employee);
                    roles.AddRange(model.GetSelectedRoles());
                    
                    var resultRole = await _userManager.AddToRolesAsync(user, roles);

                    return RedirectToAction(nameof(HomeController.Index), "Home");
                }
                AddErrors(result);
            }
            
            return View(model);
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        // GET: Employee/Details/5
        public IActionResult Details(string id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            ApplicationUser applicationUser = _context.ApplicationUser.Single(m => m.Id == id);
            if (applicationUser == null)
            {
                return HttpNotFound();
            }

            return View(applicationUser);
        }

        // GET: Employee/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Employee/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ApplicationUser applicationUser)
        {
            if (ModelState.IsValid)
            {
                _context.ApplicationUser.Add(applicationUser);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(applicationUser);
        }

        // GET: Employee/Edit/5
        public IActionResult Edit(string id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            
            ApplicationUser applicationUser = _context.ApplicationUser.Include(x => x.Roles).Include(x => x.Claims).Single(m => m.Id == id);
            if (applicationUser == null)
            {
                return HttpNotFound();
            }

            var roles = _context.Roles.ToList();
            return View(EmployeeRegisterViewModel.CreateForEdit(applicationUser, roles));
        }

        // POST: Employee/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(EmployeeRegisterViewModel model, string editId)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser applicationUser = _context.ApplicationUser.Include(x => x.Roles).Include(x => x.Claims).Single(m => m.Id == editId);
                _context.Update(applicationUser);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

        // GET: Employee/Delete/5
        [ActionName("Delete")]
        public IActionResult Delete(string id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            ApplicationUser applicationUser = _context.ApplicationUser.Single(m => m.Id == id);
            if (applicationUser == null)
            {
                return HttpNotFound();
            }

            return View(applicationUser);
        }

        // POST: Employee/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(string id)
        {
            ApplicationUser applicationUser = _context.ApplicationUser.Single(m => m.Id == id);
            _context.ApplicationUser.Remove(applicationUser);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}

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
    [Authorize(AuthPolicy.ManageUser)]
    public class EmployeeController : Controller
    {
        private ApplicationDbContext _context;
        private UserManager<ApplicationUser> _userManager;

        public EmployeeController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            this._userManager = userManager;
        }

        [Authorize(AuthPolicy.ManageUser)]
        // GET: Employee
        public IActionResult Index()
        {
            var identityRoles = _context.Roles.ToList();
            var roleEmployee = _context.Roles.Single(x => x.Name == RoleNames.Employee);
            var model = _context.ApplicationUser.Include(x => x.Roles)
                .Where(x => x.Roles.Any(r => r.RoleId == roleEmployee.Id))
                .ToList()
                .Select(x => EmployeeEditViewModel.CreateForEdit(x, identityRoles, _context.Cities.ToList())).ToList();
            return View(model);
        }

        [Authorize]
        public IActionResult Create()
        {
            var model = new EmployeeRegisterViewModel(0, _context.Cities.ToList());
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EmployeeRegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    FIO = model.FIO,
                    UserName = model.Email,
                    Email = model.Email,
                    OpenPassword = model.Password
                };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    var roles = new List<string>();
                    roles.Add(RoleNames.Employee);
                    roles.AddRange(model.GetSelectedRoles());
                    
                    var resultRole = await _userManager.AddToRolesAsync(user, roles);

                    return RedirectToAction(nameof(Index));
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
        
        // GET: Employee/Edit/5
        public IActionResult Edit(string id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            
            ApplicationUser applicationUser = _context.ApplicationUser.GetById(id);
            if (applicationUser == null)
            {
                return HttpNotFound();
            }

            var roles = _context.Roles.ToList();
            var cityList = _context.Cities.ToList();
            return View(EmployeeEditViewModel.CreateForEdit(applicationUser, roles, cityList));
        }

        // POST: Employee/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EmployeeEditViewModel model, string editId)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = _context.ApplicationUser.GetById(editId);
                user.FIO = model.FIO;
                user.CityId = model.City.Id;

                var selectedRoles = model.GetSelectedRoles();
                foreach (var roleName in RoleNames.PermissionRoles)
                {
                    bool isSelected = selectedRoles.Contains(roleName);
                    bool userInRole = await _userManager.IsInRoleAsync(user, roleName);
                    if (isSelected)
                    {
                        if (!userInRole)
                        {
                            await _userManager.AddToRoleAsync(user, roleName);
                        }
                    }
                    else
                    {
                        if (userInRole)
                        {
                            await _userManager.RemoveFromRoleAsync(user, roleName);
                        }
                    }
                }

                _context.Update(user);
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

            _context.ApplicationUser.Remove(applicationUser);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        
    }
}

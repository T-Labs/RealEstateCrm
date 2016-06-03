using System.Linq;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.Data.Entity;
using WebApp.Entities;
using WebApp.Models;
using Microsoft.AspNet.Authorization;
using WebApp.ViewModels;

namespace WebApp.Controllers
{
    [Authorize(AuthPolicy.Employees)]
    public class CustomersController : BaseController
    {
        public CustomersController(ApplicationDbContext context) : base(context)
        {
        }

        // GET: Customer
        public IActionResult Index()
        {
            var applicationDbContext = _context.Clients.Include(c => c.Cities).Include(c => c.CustomerAccount).Include(c => c.Smses).Include(c => c.User);
            return View(applicationDbContext.ToList());
        }

        // GET: Customer/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Customer customer = _context.Clients.Single(m => m.Id == id);
            if (customer == null)
            {
                return HttpNotFound();
            }

            return View(customer);
        }

        // GET: Customer/Create
        public IActionResult Create()
        {
            var model = new CustomerEditModel();
            return View("Save", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CustomerEditModel model)
        {
            if (ModelState.IsValid)
            {
                var customer = new Customer();
                model.UpdateEntity(customer);
                _context.Clients.Add(customer);
                _context.SaveChanges();

                model.UpdateDistricts(customer);
                model.UpdateHousingTypes(customer);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }
           
            return View("Save",model);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Customer customer = _context.Clients
                .Include(x => x.DistrictToClients)
                .Include(x => x.TypesHousingToCustomers)
                .Include(x => x.Phones)
                .Single(m => m.Id == id);

            if (customer == null)
            {
                return HttpNotFound();
            }

            var model = CustomerEditModel.Create(customer);
            return View("Save", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(CustomerEditModel model)
        {
            if (ModelState.IsValid)
            {
                var customer = _context.Clients
                    .Include(x => x.DistrictToClients)
                    .Include(x => x.TypesHousingToCustomers)
                    .Include(x => x.Phones)
                    .Single(x => x.Id == model.EditId);

                model.UpdateEntity(customer);
                model.UpdateDistricts(customer);
                model.UpdateHousingTypes(customer);
                _context.Update(customer);
                
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View("Save", model);
        }

        // GET: Customer/Delete/5
        [ActionName("Delete")]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Customer customer = _context.Clients.Single(m => m.Id == id);
            if (customer == null)
            {
                return HttpNotFound();
            }

            return View(customer);
        }

        // POST: Customer/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            Customer customer = _context.Clients.Single(m => m.Id == id);
            _context.Clients.Remove(customer);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}

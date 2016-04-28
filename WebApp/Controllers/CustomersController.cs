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

        // POST: Customer/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Customer customer)
        {
            if (ModelState.IsValid)
            {
                _context.Clients.Add(customer);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
           
            return View(customer);
        }

        // GET: Customer/Edit/5
        public IActionResult Edit(int? id)
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
            
            var model = new CustomerEditModel();
            return View("Save", model);
        }

        // POST: Customer/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Customer customer)
        {
            if (ModelState.IsValid)
            {
                _context.Update(customer);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewData["CityId"] = new SelectList(_context.Cities, "Id", "Cities", customer.CityId);
            ViewData["CustomerUserId"] = new SelectList(_context.ApplicationUser, "Id", "CustomerAccount", customer.CustomerUserId);
            ViewData["SmsId"] = new SelectList(_context.Smses, "Id", "Smses", customer.SmsId);
            ViewData["ApplicationUserId"] = new SelectList(_context.ApplicationUser, "Id", "User", customer.ApplicationUserId);
            return View(customer);
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

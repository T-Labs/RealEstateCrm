using System.Linq;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.Data.Entity;
using WebApp.Entities;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class StreetsController : Controller
    {
        private ApplicationDbContext _context;

        public StreetsController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: Streets
        public IActionResult Index()
        {
            return View(_context.Streets.ToList());
        }

        // GET: Streets/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Street street = _context.Streets.Single(m => m.Id == id);
            if (street == null)
            {
                return HttpNotFound();
            }

            return View(street);
        }

        // GET: Streets/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Streets/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Street street)
        {
            if (ModelState.IsValid)
            {
                _context.Streets.Add(street);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(street);
        }

        // GET: Streets/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Street street = _context.Streets.Single(m => m.Id == id);
            if (street == null)
            {
                return HttpNotFound();
            }
            return View(street);
        }

        // POST: Streets/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Street street)
        {
            if (ModelState.IsValid)
            {
                _context.Update(street);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(street);
        }

        // GET: Streets/Delete/5
        [ActionName("Delete")]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Street street = _context.Streets.Single(m => m.Id == id);
            if (street == null)
            {
                return HttpNotFound();
            }

            return View(street);
        }

        // POST: Streets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            Street street = _context.Streets.Single(m => m.Id == id);
            _context.Streets.Remove(street);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
